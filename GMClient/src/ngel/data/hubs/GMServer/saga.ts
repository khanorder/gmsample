import { all, fork, takeLatest, call, put, select, take } from 'redux-saga/effects';
import { EventChannel, eventChannel, Subscribe, Unsubscribe } from 'redux-saga';
import { AnyAction, PayloadAction, Reducer } from '@reduxjs/toolkit';
import { HubConnectionBuilder, HubConnection, HubConnectionState, LogLevel } from '@microsoft/signalr';
import { push, replace, goBack, goForward, go, RouterState } from 'connected-next-router';
import { Helpers } from 'src/helpers/index';
import { Errors } from 'src/ngel/data/autoErrors';
import { RootState } from 'src/store/reducers';
import { LayoutState, startLoading, startLoadingMessage, stopLoading } from 'src/store/reducers/layouts';
import { UserState, signIn, signOut } from 'src/store/reducers/user';
import {
    initConnection,
    startReconnecting,
    setConnection,
    setConnectionState,
    setConnectedAck,
    addCountTryConnect,
    resetCountTryConnect,
    GMServerHubState,
    setSignInAck,
    setSignInLDAPAck,
    setSignInEmailAck,
    setDataTableAck,
    setSendGameMailResultAck,
    setInsertEventMailResultAck,
    setUpdateEventMailResultAck,
    setRemoveEventMailResultAck,
    setSendUserJobAck,
    setSignOutAck,
    setDeniedAck,
    setSendChattingMessageResultAck,
    setReceiveChattingMessageAck,
    setTestAck,
    setNoticeAck,
    setReceiveChattingMessageAllAck,
    signInReq,
    signInLDAPReq,
    signInEmailReq,
    signOutReq,
    sendGameMailReq,
    insertEventMailReq,
    updateEventMailReq,
    removeEventMailReq,
    sendChattingMessageReq,
    testReq,
} from './reducer';
import {
    connectedAck,
    checkAuthenticationAck,
    checkConnectionAck,
    commonNoticeAck,
    signInAck,
    signInLDAPAck,
    signInEmailAck,
    dataTableAck,
    sendGameMailResultAck,
    insertEventMailResultAck,
    updateEventMailResultAck,
    removeEventMailResultAck,
    sendUserJobAck,
    signOutAck,
    deniedAck,
    sendChattingMessageResultAck,
    receiveChattingMessageAck,
    testAck,
    noticeAck,
    receiveChattingMessageAllAck,
} from '@packets/GMServer/ack';
import { GMServerHubModels } from './model';
import { setTables } from 'src/ngel/data/tables/reducer';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';
import isEmpty from 'lodash/isEmpty';

function createConnectionStateChannel (connection: HubConnection): EventChannel<HubConnectionState> {
    return eventChannel<HubConnectionState>(emit => {
        const emitter = () => emit(connection.state);
        const interval = 'production' !== process.env.NODE_ENV ? 1000 : 3000;
        const reconnect = setInterval(emitter, interval);
        const unsubscribe: Unsubscribe = () => clearInterval(reconnect);
        return unsubscribe;
    });
}

function* reconnect (connection: HubConnection) {
    const channel: EventChannel<HubConnectionState> = yield call(createConnectionStateChannel, connection);
    yield put(resetCountTryConnect());
    yield put(setConnectionState(HubConnectionState.Reconnecting));

    try {
        while (true) {
            try {
                const data: HubConnectionState = yield take(channel);
                console.log(`saga - reconnect: ${data}`);

                const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);

                if (hub.denied) {
                    yield put(setConnectionState(HubConnectionState.Disconnected));

                    if (hub.connection)
                        hub.connection.stop();

                    channel.close();
                    break;
                }

                const maxReTryCount = 'production' !== process.env.NODE_ENV ? 5 : 20;

                if (maxReTryCount < hub.countTryConnect) {
                    console.log(`saga - reconnect: stop reconnect.`);
                    yield put(setConnectionState(HubConnectionState.Disconnected));
                    channel.close();
                    continue;
                }

                yield put(addCountTryConnect());

                switch (data) {
                    case HubConnectionState.Disconnected:
                        console.log(`saga - reconnect: SignalR is trying connect.(${dayjs().format("YYYY-MM-DD HH:mm:ss")})`);
                        connection.start()
                            .catch((error) => {
                                console.error(error);
                            });
                        break;

                    case HubConnectionState.Connected:
                        console.log(`saga - reconnect: SignalR connected to the GMServer.(${dayjs().format("YYYY-MM-DD HH:mm:ss")})`);
                        yield put(setConnectionState(HubConnectionState.Connected));
                        yield put(resetCountTryConnect());
                        channel.close();

                        console.log(`the reconnect channel closed.`);
                        break;

                    default:
                        console.log(`SignalR ${connection.state.toString()}.(${dayjs().format("YYYY-MM-DD HH:mm:ss")})`);
                        break;
                }
            } catch (innerError) {
                console.error(innerError);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

function createCheckConnectionChannel (connection: HubConnection): EventChannel<HubConnectionState> {
    return eventChannel<HubConnectionState>(emit => {
        const emitter = () => emit(connection.state);
        const interval = 7000;
        const reconnect = setInterval(emitter, interval);
        const unsubscribe: Unsubscribe = () => clearInterval(reconnect);
        return unsubscribe;
    });
}

export function* checkConnection (connection: HubConnection) {
    const channel: EventChannel<HubConnectionState> = yield call(createCheckConnectionChannel, connection);

    try {
        while (true) {
            try {
                const data: HubConnectionState = yield take(channel);
                console.log(`saga - checkConnection: ${data}`);
                const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);

                if (hub.denied) {
                    yield put(setConnectionState(HubConnectionState.Disconnected));

                    if (hub.connection)
                        hub.connection.stop();

                    channel.close();
                    break;
                }

                switch (data) {
                    case HubConnectionState.Connected:
                        console.log(`saga - checkConnection: SignalR connection checked.(${dayjs().format("YYYY-MM-DD HH:mm:ss")})`);
                        yield setConnectionState(HubConnectionState.Connected);

                        try {
                            const router: RouterState = yield select((state: RootState) => state.router);
                            const packet = new GMServerHubModels.PCheckConnectionReq();

                            if (window)
                                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

                            if (router && router.location.pathname)
                                packet.reqPathName = router.location.pathname;

                            const encodedDataString = Helpers.encodeMessagePack(packet);
                            connection.invoke('CheckConnection', encodedDataString);
                        } catch (hubError) {
                            console.error(hubError);
                        }
                        break;

                    default:
                        console.log(`saga - checkConnection: stop check connection.(${dayjs().format("YYYY-MM-DD HH:mm:ss")})`);
                        channel.close();
                        break;
                }
            } catch (innerError) {
                console.error(innerError);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

function createHubCheckConnectionAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('CheckConnectionAck');
        connection.on('CheckConnectionAck', emitter);
        return unsubscribe;
    });
}

function* onCheckConnectionAck (connection: HubConnection) {
    const channel: EventChannel<string> = yield call(createHubCheckConnectionAckChannel, connection);

    try {
        while (true) {
            try {
                console.log(`saga - onCheckConnectionAck`);
                const data: string = yield take(channel);
                let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PCheckConnectionAck>(data);
                if (null == decodedData)
                    decodedData = new GMServerHubModels.PCheckConnectionAck();

                const result: GMServerHubModels.PCheckConnectionAck = yield call(checkConnectionAck, decodedData);

                const router: RouterState = yield select((state: RootState) => state.router);
                switch (result.error) {
                    case Errors.None:
                        const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');
                        const user: UserState = yield select((state: RootState) => state.user);
                        if (token && isEmpty(user.signinId))
                            yield call(checkAuthenticationReq, connection);
                        break;

                    case Errors.CheckConnection_PasswordExpired:
                        break;

                    case Errors.CheckConnection_ReissueToken:
                        if (window && result.token) {
                            console.log('the token was reissued.');
                            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', result.token);
                            const user: UserState = yield select((state: RootState) => state.user);
                            if (isEmpty(user.signinId))
                                yield call(checkAuthenticationReq, connection);
                        }
                        break;

                    case Errors.CheckConnection_AuthExpired:
                        if (window)
                            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');

                        alert('인증이 만료되었습니다.\n로그인 화면으로 이동합니다.');
                        yield put(signOut());
                        break;

                    default:
                        if (window) {
                            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');
                            console.log('remove the expired token.');
                        }

                        if (router && '/' != router.location.pathname && false == router.location.pathname.startsWith(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth') && false == router.location.pathname.startsWith(process.env.OAUTH_PATH ?? '/oauth'))
                            yield put(push(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth'));
                }
            } catch (innerError) {
                console.error(innerError);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

function createHubCheckAuthenticationAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('CheckAuthenticationAck');
        connection.on('CheckAuthenticationAck', emitter);
        return unsubscribe;
    });
}

function* onCheckAuthenticationAck (connection: HubConnection) {
    const channel: EventChannel<string> = yield call(createHubCheckAuthenticationAckChannel, connection);

    try {
        while (true) {
            try {
                console.log(`saga - onCheckAuthenticationAck`);
                const data: string = yield take(channel);
                let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PCheckAuthenticationAck>(data);
                if (null == decodedData)
                    decodedData = new GMServerHubModels.PCheckAuthenticationAck();

                const result: GMServerHubModels.PCheckAuthenticationAck = yield call(checkAuthenticationAck, decodedData);

                const router: RouterState = yield select((state: RootState) => state.router);
                const user: UserState = yield select((state: RootState) => state.user);
                const layouts: LayoutState = yield select((state: RootState) => state.layouts);
                if (layouts.loadingActive)
                    yield put(stopLoading());

                switch (result.error) {
                    case Errors.None:
                        if (result.user) {
                            yield put(signIn(result.user));
                            yield put(setTables(result.dataTable));
                        }
                        break;

                    case Errors.CheckAuthentication_ReissueToken:
                        if (window && result.token && result.user) {
                            console.log('the token was reissued.');
                            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', result.token);
                            yield put(signIn(result.user));
                            yield put(setTables(result.dataTable));
                        }
                        break;

                    default:
                        if (window) {
                            const token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');
                            if (token) {
                                Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');
                                console.log('remove the expired token.');
                            }
                        }

                        yield put(signOut());

                        if (router && '/' != router.location.pathname && false == router.location.pathname.startsWith(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth') && false == router.location.pathname.startsWith(process.env.OAUTH_PATH ?? '/oauth'))
                            yield put(push(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth'));
                }
            } catch (innerError) {
                console.error(innerError);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

function createHubConnectedAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('ConnectedAck');
        connection.on('ConnectedAck', emitter);
        return unsubscribe;
    });
}

function* onConnectedAck (connection: HubConnection) {
    const channel: EventChannel<string> = yield call(createHubConnectedAckChannel, connection);

    try {
        while (true) {
            try {
                console.log(`saga - onConnectedAck`);
                const data: string = yield take(channel);
                let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PConnectedAck>(data);
                if (null == decodedData)
                    decodedData = new GMServerHubModels.PConnectedAck();

                const result: GMServerHubModels.PConnectedAck = yield call(connectedAck, decodedData);
                yield put(setConnectedAck(result));
                if (window && !Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK')) {
                    const router: RouterState = yield select((state: RootState) => state.router);
                    if (router && '/' != router.location.pathname && false == router.location.pathname.startsWith(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth') && false == router.location.pathname.startsWith(process.env.OAUTH_PATH ?? '/oauth'))
                        yield put(push(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth'));
                    return;
                }
                yield call(checkAuthenticationReq, connection);
            } catch (innerError) {
                console.error(innerError);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

function createHubCommonNoticeAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('CommonNoticeAck');
        connection.on('CommonNoticeAck', emitter);
        return unsubscribe;
    });
}

function* onCommonNoticeAck (connection: HubConnection) {
    const channel: EventChannel<string> = yield call(createHubCommonNoticeAckChannel, connection);

    try {
        while (true) {
            try {
                console.log(`saga - onCommonNoticeAck`);
                const data: string = yield take(channel);
                let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PCommonNoticeAck>(data);
                if (null == decodedData)
                    decodedData = new GMServerHubModels.PCommonNoticeAck();

                const result: GMServerHubModels.PCommonNoticeAck = yield call(commonNoticeAck, decodedData);
                  if (result.message) {
                      alert(result.message);
                  } else {
                      switch (result.error) {
                          case Errors.Common_FailedToDecodeMsgPack:
                                  alert('패킷을 변환하지 못했습니다.');
                              break;

                          default:
                                  alert(Errors[result.error]);
                              break;
                      }
                  }
            } catch (innerError) {
                console.error(innerError);
            }
        }
    } catch (error) {
        console.error(error);
    }
}

function createHubCloseChannel (connection: HubConnection): EventChannel<HubConnectionState> {
    return eventChannel<HubConnectionState>(emit => {
        const emitter = () => emit(connection.state);
        const unsubscribe: Unsubscribe = () => {};
        connection.onclose(emitter);
        return unsubscribe;
    });
}

function* onClose (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<HubConnectionState> = yield call(createHubCloseChannel, connection);

        try {
            while (true) {
                try {
                    const data: HubConnectionState = yield take(channel);
                    console.log(`saga - onClose`);

                    yield call(setConnectionInfo, connection.baseUrl);
                } catch (innerError) {
                    console.error(innerError);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubSignInAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('SignInAck');
        connection.on('SignInAck', emitter);
        return unsubscribe;
    });
}

function* onSignInAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubSignInAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onSignInAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PSignInAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PSignInAck();

                    const result: GMServerHubModels.PSignInAck = yield call(signInAck, decodedData);

                    yield put(setSignInAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubSignInLDAPAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('SignInLDAPAck');
        connection.on('SignInLDAPAck', emitter);
        return unsubscribe;
    });
}

function* onSignInLDAPAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubSignInLDAPAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onSignInLDAPAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PSignInLDAPAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PSignInLDAPAck();

                    const result: GMServerHubModels.PSignInLDAPAck = yield call(signInLDAPAck, decodedData);

                    yield put(setSignInLDAPAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubSignInEmailAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('SignInEmailAck');
        connection.on('SignInEmailAck', emitter);
        return unsubscribe;
    });
}

function* onSignInEmailAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubSignInEmailAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onSignInEmailAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PSignInEmailAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PSignInEmailAck();

                    const result: GMServerHubModels.PSignInEmailAck = yield call(signInEmailAck, decodedData);

                    yield put(setSignInEmailAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubDataTableAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('DataTableAck');
        connection.on('DataTableAck', emitter);
        return unsubscribe;
    });
}

function* onDataTableAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubDataTableAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onDataTableAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PDataTableAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PDataTableAck();

                    const result: GMServerHubModels.PDataTableAck = yield call(dataTableAck, decodedData);

                    yield put(setDataTableAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubSendGameMailResultAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('SendGameMailResultAck');
        connection.on('SendGameMailResultAck', emitter);
        return unsubscribe;
    });
}

function* onSendGameMailResultAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubSendGameMailResultAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onSendGameMailResultAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PSendGameMailResultAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PSendGameMailResultAck();

                    const result: GMServerHubModels.PSendGameMailResultAck = yield call(sendGameMailResultAck, decodedData);

                    yield put(setSendGameMailResultAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubInsertEventMailResultAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('InsertEventMailResultAck');
        connection.on('InsertEventMailResultAck', emitter);
        return unsubscribe;
    });
}

function* onInsertEventMailResultAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubInsertEventMailResultAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onInsertEventMailResultAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PInsertEventMailResultAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PInsertEventMailResultAck();

                    const result: GMServerHubModels.PInsertEventMailResultAck = yield call(insertEventMailResultAck, decodedData);

                    yield put(setInsertEventMailResultAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubUpdateEventMailResultAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('UpdateEventMailResultAck');
        connection.on('UpdateEventMailResultAck', emitter);
        return unsubscribe;
    });
}

function* onUpdateEventMailResultAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubUpdateEventMailResultAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onUpdateEventMailResultAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PUpdateEventMailResultAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PUpdateEventMailResultAck();

                    const result: GMServerHubModels.PUpdateEventMailResultAck = yield call(updateEventMailResultAck, decodedData);

                    yield put(setUpdateEventMailResultAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubRemoveEventMailResultAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('RemoveEventMailResultAck');
        connection.on('RemoveEventMailResultAck', emitter);
        return unsubscribe;
    });
}

function* onRemoveEventMailResultAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubRemoveEventMailResultAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onRemoveEventMailResultAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PRemoveEventMailResultAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PRemoveEventMailResultAck();

                    const result: GMServerHubModels.PRemoveEventMailResultAck = yield call(removeEventMailResultAck, decodedData);

                    yield put(setRemoveEventMailResultAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubSendUserJobAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('SendUserJobAck');
        connection.on('SendUserJobAck', emitter);
        return unsubscribe;
    });
}

function* onSendUserJobAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubSendUserJobAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onSendUserJobAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PSendUserJobAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PSendUserJobAck();

                    const result: GMServerHubModels.PSendUserJobAck = yield call(sendUserJobAck, decodedData);

                    yield put(setSendUserJobAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubSignOutAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('SignOutAck');
        connection.on('SignOutAck', emitter);
        return unsubscribe;
    });
}

function* onSignOutAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubSignOutAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onSignOutAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PSignOutAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PSignOutAck();

                    const result: GMServerHubModels.PSignOutAck = yield call(signOutAck, decodedData);

                    yield put(setSignOutAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubDeniedAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('DeniedAck');
        connection.on('DeniedAck', emitter);
        return unsubscribe;
    });
}

function* onDeniedAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubDeniedAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onDeniedAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PDeniedAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PDeniedAck();

                    const result: GMServerHubModels.PDeniedAck = yield call(deniedAck, decodedData);

                    yield put(setDeniedAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubSendChattingMessageResultAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('SendChattingMessageResultAck');
        connection.on('SendChattingMessageResultAck', emitter);
        return unsubscribe;
    });
}

function* onSendChattingMessageResultAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubSendChattingMessageResultAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onSendChattingMessageResultAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PSendChattingMessageResultAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PSendChattingMessageResultAck();

                    const result: GMServerHubModels.PSendChattingMessageResultAck = yield call(sendChattingMessageResultAck, decodedData);

                    yield put(setSendChattingMessageResultAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubReceiveChattingMessageAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('ReceiveChattingMessageAck');
        connection.on('ReceiveChattingMessageAck', emitter);
        return unsubscribe;
    });
}

function* onReceiveChattingMessageAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubReceiveChattingMessageAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onReceiveChattingMessageAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PReceiveChattingMessageAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PReceiveChattingMessageAck();

                    const result: GMServerHubModels.PReceiveChattingMessageAck = yield call(receiveChattingMessageAck, decodedData);

                    yield put(setReceiveChattingMessageAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubTestAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('TestAck');
        connection.on('TestAck', emitter);
        return unsubscribe;
    });
}

function* onTestAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubTestAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onTestAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PTestAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PTestAck();

                    const result: GMServerHubModels.PTestAck = yield call(testAck, decodedData);

                    yield put(setTestAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubNoticeAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('NoticeAck');
        connection.on('NoticeAck', emitter);
        return unsubscribe;
    });
}

function* onNoticeAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubNoticeAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onNoticeAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PNoticeAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PNoticeAck();

                    const result: GMServerHubModels.PNoticeAck = yield call(noticeAck, decodedData);

                    yield put(setNoticeAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function createHubReceiveChattingMessageAllAckChannel (connection: HubConnection): EventChannel<string> {
    return eventChannel<string>(emit => {
        const emitter = (data: string) => emit(data);
        const unsubscribe: Unsubscribe = () => connection.off('ReceiveChattingMessageAllAck');
        connection.on('ReceiveChattingMessageAllAck', emitter);
        return unsubscribe;
    });
}

function* onReceiveChattingMessageAllAck (connection: HubConnection) {
    if (connection && HubConnectionState.Disconnected === connection.state) {
        const channel: EventChannel<string> = yield call(createHubReceiveChattingMessageAllAckChannel, connection);

        try {
            while (true) {
                try {
                    console.log(`saga - onReceiveChattingMessageAllAck`);
                    const data: string = yield take(channel);
                    let decodedData = Helpers.decodeMessagePack<GMServerHubModels.PReceiveChattingMessageAllAck>(data);
                    if (null == decodedData)
                        decodedData = new GMServerHubModels.PReceiveChattingMessageAllAck();

                    const result: GMServerHubModels.PReceiveChattingMessageAllAck = yield call(receiveChattingMessageAllAck, decodedData);

                    yield put(setReceiveChattingMessageAllAck(result));
                } catch (error) {
                    console.error(error);
                }
            }
        } catch (error) {
            console.error(error);
        }
    }
}

function* setConnectionInfo(url: string): any {
    console.log(`saga - setConnectionInfo`);

    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);

    if (!hub.connection || (hub.connection && HubConnectionState.Disconnected === hub.connection.state)) {
        try {
            let connectionBuilder = (new HubConnectionBuilder()).withUrl(url).configureLogging('production' === process.env.NODE_ENV ? LogLevel.None : LogLevel.Information);
            const connection = connectionBuilder.build();
            connection.serverTimeoutInMilliseconds = 300000;

            yield fork(onCheckAuthenticationAck, connection);
            yield fork(onCheckConnectionAck, connection);
            yield fork(onConnectedAck, connection);
            yield fork(onCommonNoticeAck, connection);
            yield fork(onClose, connection);
            yield fork(onSignInAck, connection);
            yield fork(onSignInLDAPAck, connection);
            yield fork(onSignInEmailAck, connection);
            yield fork(onDataTableAck, connection);
            yield fork(onSendGameMailResultAck, connection);
            yield fork(onInsertEventMailResultAck, connection);
            yield fork(onUpdateEventMailResultAck, connection);
            yield fork(onRemoveEventMailResultAck, connection);
            yield fork(onSendUserJobAck, connection);
            yield fork(onSignOutAck, connection);
            yield fork(onDeniedAck, connection);
            yield fork(onSendChattingMessageResultAck, connection);
            yield fork(onReceiveChattingMessageAck, connection);
            yield fork(onTestAck, connection);
            yield fork(onNoticeAck, connection);
            yield fork(onReceiveChattingMessageAllAck, connection);
            yield fork(checkConnection, connection);
            yield call(reconnect, connection);
            yield put(setConnection(connection));

        } catch (error) {
            console.error(error);
        }
    }
}

function* callInitConnection(action: PayloadAction<string>) {
    console.log(`saga - callInitConnection`);

    yield call(setConnectionInfo, `${action.payload}/GMServer`);
}

function* callStartReconnecting() {
    console.log(`saga - callStartReconnecting`);

    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);

    if (hub.denied) {
        yield put(setConnectionState(HubConnectionState.Disconnected));

        if (hub.connection)
            hub.connection.stop();

        return;
    }

    if (hub.connection) {
        switch (hub.connection.state) {
            case HubConnectionState.Connected:
                yield put(setConnectionState(HubConnectionState.Connected));
                break;

            default:
                try {
                    yield call(reconnect, hub.connection);
                } catch (error) {
                    console.error(error);
                }
        }
    }
}

export function* checkAuthenticationReq(connection: HubConnection) {
    const router: RouterState = yield select((state: RootState) => state.router);
    const packet = new GMServerHubModels.PCheckAuthenticationReq();

    if (window)
        packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

    if (isEmpty(packet.token)) {
        yield put(signOut());
        return;
    }

    if (router && router.location.pathname)
        packet.reqPathName = router.location.pathname;
    console.log(`saga - checkAuthenticationReq: ${JSON.stringify(packet)}`);

    if (connection && HubConnectionState.Connected === connection.state) {
        try {
            const encodedDataString = Helpers.encodeMessagePack(packet);
            connection.invoke('CheckAuthentication', encodedDataString);
        } catch(error) {
                console.error(error);
        }
    } else {
        console.log(`saga - checkAuthenticationReq: ${(null == connection ? 'The GMServerHub connection is null.' : connection.state)}`);
    }
}

export function* callSignInReq(action: PayloadAction<GMServerHubModels.PSignInReq>) {
    console.log(`saga - callSignInReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('SignIn', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - signInReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callSignInLDAPReq(action: PayloadAction<GMServerHubModels.PSignInLDAPReq>) {
    console.log(`saga - callSignInLDAPReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('SignInLDAP', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - signInLDAPReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callSignInEmailReq(action: PayloadAction<GMServerHubModels.PSignInEmailReq>) {
    console.log(`saga - callSignInEmailReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('SignInEmail', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - signInEmailReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callSignOutReq() {
    console.log(`saga - callSignOutReq`);

    const packet = new GMServerHubModels.PSignOutReq();
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('SignOut', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - signOutReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callSendGameMailReq(action: PayloadAction<GMServerHubModels.PSendGameMailReq>) {
    console.log(`saga - callSendGameMailReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('SendGameMail', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - sendGameMailReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callInsertEventMailReq(action: PayloadAction<GMServerHubModels.PInsertEventMailReq>) {
    console.log(`saga - callInsertEventMailReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('InsertEventMail', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - insertEventMailReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callUpdateEventMailReq(action: PayloadAction<GMServerHubModels.PUpdateEventMailReq>) {
    console.log(`saga - callUpdateEventMailReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('UpdateEventMail', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - updateEventMailReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callRemoveEventMailReq(action: PayloadAction<GMServerHubModels.PRemoveEventMailReq>) {
    console.log(`saga - callRemoveEventMailReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('RemoveEventMail', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - removeEventMailReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callSendChattingMessageReq(action: PayloadAction<GMServerHubModels.PSendChattingMessageReq>) {
    console.log(`saga - callSendChattingMessageReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('SendChattingMessage', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - sendChattingMessageReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* callTestReq(action: PayloadAction<GMServerHubModels.PTestReq>) {
    console.log(`saga - callTestReq - data: ${JSON.stringify(action.payload)}`);

    const packet = action.payload;
    const router: RouterState = yield select((state: RootState) => state.router);
    const hub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (hub.connection && HubConnectionState.Connected === hub.connection.state) {
        try {
            if (window)
                packet.token = Helpers.getCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK');

            if (router && router.location.pathname)
                packet.reqPathName = router.location.pathname;

            const encodedDataString = Helpers.encodeMessagePack(packet);
            hub.connection.invoke('Test', encodedDataString);
        } catch(error) {
            console.error(error);
        }
    } else {
        console.log(`saga - testReq: ${(null == hub.connection ? 'The GMServerHub connection is null.' : hub.connection.state)}`);
    }
}

export function* watchGMServerHub() {
    yield takeLatest(initConnection, callInitConnection);
    yield takeLatest(startReconnecting, callStartReconnecting);
    yield takeLatest(signInReq, callSignInReq);
    yield takeLatest(signInLDAPReq, callSignInLDAPReq);
    yield takeLatest(signInEmailReq, callSignInEmailReq);
    yield takeLatest(signOutReq, callSignOutReq);
    yield takeLatest(sendGameMailReq, callSendGameMailReq);
    yield takeLatest(insertEventMailReq, callInsertEventMailReq);
    yield takeLatest(updateEventMailReq, callUpdateEventMailReq);
    yield takeLatest(removeEventMailReq, callRemoveEventMailReq);
    yield takeLatest(sendChattingMessageReq, callSendChattingMessageReq);
    yield takeLatest(testReq, callTestReq);
}
