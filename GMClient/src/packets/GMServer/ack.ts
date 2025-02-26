import { HubConnectionState } from '@microsoft/signalr';
import { GMServerHubState, setDenied } from '@ngel/data/hubs/GMServer/reducer';
import { GMServerHubModels } from '@ngeldata/hubs/GMServer/model';
import { RootState } from 'src/store/reducers';
import { UserState, signIn, signOut, updateJob } from 'src/store/reducers/user';
import { HomepageSettingsState, setExistsUpdate, setNotifyUpdateDone } from 'src/store/reducers/settings/hompageSettings';
import { LayoutState, startLoadingMessage, stopLoading, openNotification } from 'src/store/reducers/layouts';
import { pushChattingMessage } from 'src/store/reducers/chattings';
import { call, put, select } from 'redux-saga/effects';
import { push, replace, goBack, goForward, go, RouterState } from 'connected-next-router';
import { Helpers } from '@helpers/index';
import { checkConnection } from '@ngeldata/hubs/GMServer/saga';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';
import isEmpty from 'lodash/isEmpty';
import { Errors } from '@ngel/data/autoErrors';
import { setTables } from '@ngel/data/tables/reducer';
import { v4 as uuid } from 'uuid';
import { updateServiceEventMail } from 'src/store/reducers/pages';
import { Models } from '@ngel/data/models';

export function* deniedAck (data: GMServerHubModels.PDeniedAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - deniedAck`);

    yield put(setDenied(true));
    yield put(push('/denied'));

    return data;
}

export function* checkAuthenticationAck (data: GMServerHubModels.PCheckAuthenticationAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - checkAuthenticationAck`);

    return data;
}

export function* checkConnectionAck (data: GMServerHubModels.PCheckConnectionAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - checkConnectionAck`);

    return data;
}

export function* connectedAck (data: GMServerHubModels.PConnectedAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - connectedAck`);

    const homepageSettings: HomepageSettingsState = yield select((state: RootState) => state.homepageSettings);

    if (homepageSettings.clientMasterVersion < data.recommendClientMasterVersion) {
        yield put(setExistsUpdate(true));
        yield put(setNotifyUpdateDone(false));
        return data;
    }

    if (homepageSettings.clientMasterVersion <= data.recommendClientMasterVersion && homepageSettings.clientUpdateVersion < data.recommendClientUpdateVersion) {
        yield put(setExistsUpdate(true));
        yield put(setNotifyUpdateDone(false));
        return data;
    }
    
    if (homepageSettings.clientMasterVersion <= data.recommendClientMasterVersion && homepageSettings.clientUpdateVersion <= data.recommendClientUpdateVersion && homepageSettings.clientMaintenanceVersion < data.recommendClientMaintenanceVersion) {
        yield put(setExistsUpdate(true));
        yield put(setNotifyUpdateDone(false));
        return data;
    }

    yield put(setExistsUpdate(false));
    yield put(setNotifyUpdateDone(true));
    return data;
}

export function* signInAck (data: GMServerHubModels.PSignInAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - signInAck`);

    const user: UserState = yield select((state: RootState) => state.user);
    const gmServerHub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (Errors.None == data.error && data.user && data.token) {
        if (window) {
            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', data.token);
            window.location.href = process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS';
        }
    } else if (window) {
        if ('production' !== process.env.NODE_ENV)
            console.log('remove the expired token.');

        Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');

        switch (data.error) {
            case Errors.SignIn_AlreadySignIn:
                alert('이미 로그인 중입니다.');
                break;

            default:
                alert('로그인 실패.');
        }

        yield put(stopLoading());
        yield put(push(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth'));
    }

    return data;
}

export function* signInLDAPAck (data: GMServerHubModels.PSignInLDAPAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - signInLDAPAck`);

    const user: UserState = yield select((state: RootState) => state.user);
    const gmServerHub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (Errors.None == data.error && data.user && data.token) {
        if (window) {
            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', data.token);
            window.location.href = process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS';
        }
    } else if (window) {
        if ('production' !== process.env.NODE_ENV)
            console.log('remove the expired token.');

        Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');

        switch (data.error) {
            case Errors.SignIn_AlreadySignIn:
                alert('이미 로그인 중입니다.');
                break;

            default:
                alert('로그인 실패.');
        }

        yield put(stopLoading());
    }

    return data;
}

export function* signInEmailAck (data: GMServerHubModels.PSignInEmailAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - signInEmailAck`);

    const user: UserState = yield select((state: RootState) => state.user);
    const gmServerHub: GMServerHubState = yield select((state: RootState) => state.GMServerHub);
    if (Errors.None == data.error && data.user && data.token) {
        if (window) {
            Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', data.token);
            window.location.href = process.env.NEXT_PUBLIC_MANAGE_PATH ?? '/GMS';
        }
    } else if (window) {
        if ('production' !== process.env.NODE_ENV)
            console.log('remove the expired token.');

        Helpers.setCookie(process.env.NEXT_PUBLIC_TOKEN_NAME ?? 'NGTK', '');
        
        switch (data.error) {
            case Errors.SignInEmail_AlreadySignIn:
                alert('이미 로그인 중입니다.');
                break;

            case Errors.SignInEmail_ConfirmRequired:
                alert('관리자에게 받은 비밀번호 초기화 페이지에서\n비밀번호를 설정해 주세요.');
                break;

            case Errors.SignInEmail_TooLongLatestSignin:
                alert('90일 이상 접속하지 않아 휴면계정 상태로 변경 됐습니다.\n관리자에게 휴면상태 해제를 요청해 주세요.');
                break;

            case Errors.SignInEmail_TooManyFailedSignin:
                alert('연속 5회 이상 로그인을 실패하여 로그인이 제한 됐습니다.\n관리자에게 비밀번호 초기화를 요청해 주세요.');
                break;

            default:
                alert(`로그인 실패(${data.countFailedSignin}/5).`);
        }

        yield put(stopLoading());
    }

    return data;
}

export function* dataTableAck (data: GMServerHubModels.PDataTableAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - dataTableAck`);

    yield put(setTables(data.dataTable));

    return data;
}

export function* signOutAck (data: GMServerHubModels.PSignOutAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - signOutAck`);

    if (Errors.None == data.error) {
        yield put(signOut());
        yield put(push(process.env.NEXT_PUBLIC_AUTH_PATH ?? '/NGAuth'));
    }

    return data;
}

export function* sendGameMailResultAck (data: GMServerHubModels.PSendGameMailResultAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - sendGameMailResultAck`);

    if (Errors.None != data.error)
        alert(`우편 발송실패!(error: ${Errors[data.error]})`);

    return data;
}

export function* insertEventMailResultAck (data: GMServerHubModels.PInsertEventMailResultAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - insertEventMailResultAck`);

    alert(data.message);

    if (Errors.None == data.error) {
        const urlParams = new URLSearchParams(window.location.search);
        const page = urlParams.get('page');
        yield put(push(`/GMS/Services/EventMail${(page ? `?page=${page}` : '')}`));
    }

    return data;
}

export function* updateEventMailResultAck (data: GMServerHubModels.PUpdateEventMailResultAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - updateEventMailResultAck`);

    alert(data.message);
    if (Errors.None == data.error) {
        const urlParams = new URLSearchParams(window.location.search);
        const page = urlParams.get('page');
        yield put(push(`/GMS/Services/EventMail${(page ? `?page=${page}` : '')}`));
    }

    return data;
}

export function* removeEventMailResultAck (data: GMServerHubModels.PRemoveEventMailResultAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - removeEventMailResultAck`);

    alert(data.message);
    if (Errors.None == data.error) {
        yield put(updateServiceEventMail());
    }

    return data;
}

export function* sendUserJobAck (data: GMServerHubModels.PSendUserJobAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - sendUserJobAck`);

    if (null == data || null == data.userJob)
        return;

    const user: UserState = yield select((state: RootState) => state.user);
    if (false == isEmpty(user.signinId)) {
        const existsJob = user.jobs.find(_ => _.id === data.userJob?.id);
        if (null == existsJob)
            yield put(openNotification());

        yield put(updateJob(data.userJob));

        const resultJob = user.jobs.find(_ => _.id === data.userJob?.id);
        if (null != resultJob && resultJob.jobCount == (resultJob.succeededCount + resultJob.failedCount))
            yield put(openNotification());
    }
    return data;
}

export function* sendChattingMessageResultAck (data: GMServerHubModels.PSendChattingMessageResultAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - sendChattingMessageResultAck`);

    return data;
}

export function* receiveChattingMessageAck (data: GMServerHubModels.PReceiveChattingMessageAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - receiveChattingMessageAck`);

    yield put(pushChattingMessage(data.chattingMessage as Models.ChattingMessage));

    return data;
}

export function* receiveChattingMessageAllAck (data: GMServerHubModels.PReceiveChattingMessageAllAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - receiveChattingMessageAllAck`);

    yield put(pushChattingMessage(data.chattingMessage as Models.ChattingMessage));

    return data;
}

export function* testAck (data: GMServerHubModels.PTestAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - testAck`);

    return data;
}

export function* noticeAck (data: GMServerHubModels.PNoticeAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - noticeAck`);

    return data;
}

export function* commonNoticeAck (data: GMServerHubModels.PCommonNoticeAck) {
    if ('production' !== process.env.NODE_ENV)
        console.log(`packet - noticeAck`);

    return data;
}