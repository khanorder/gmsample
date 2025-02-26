import { GMServerHubModels } from './model';
import { Errors } from '@ngel/data/autoErrors';
import { HubConnectionBuilder, HubConnection, HubConnectionState, LogLevel } from '@microsoft/signalr';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import deepmerge from 'deepmerge';
import { dayjs, Dayjs, timezoneName } from 'src/helpers/localizedDayjs';
import isEmpty from 'lodash/isEmpty';

interface GMServerHubState {
    denied: boolean;
    connection: HubConnection|null;
    connectionState: HubConnectionState;
    serverId: string;
    serverVersion: string;
    recommendClientMasterVersion: number;
    recommendClientUpdateVersion: number;
    recommendClientMaintenanceVersion: number;
    countTryConnect: number;
    SignInAck: GMServerHubModels.PSignInAck|null;
    SignInLDAPAck: GMServerHubModels.PSignInLDAPAck|null;
    SignInEmailAck: GMServerHubModels.PSignInEmailAck|null;
    DataTableAck: GMServerHubModels.PDataTableAck|null;
    SendGameMailResultAck: GMServerHubModels.PSendGameMailResultAck|null;
    InsertEventMailResultAck: GMServerHubModels.PInsertEventMailResultAck|null;
    UpdateEventMailResultAck: GMServerHubModels.PUpdateEventMailResultAck|null;
    RemoveEventMailResultAck: GMServerHubModels.PRemoveEventMailResultAck|null;
    SendUserJobAck: GMServerHubModels.PSendUserJobAck|null;
    SignOutAck: GMServerHubModels.PSignOutAck|null;
    DeniedAck: GMServerHubModels.PDeniedAck|null;
    SendChattingMessageResultAck: GMServerHubModels.PSendChattingMessageResultAck|null;
    ReceiveChattingMessageAck: GMServerHubModels.PReceiveChattingMessageAck|null;
    TestAck: GMServerHubModels.PTestAck|null;
    NoticeAck: GMServerHubModels.PNoticeAck|null;
    ReceiveChattingMessageAllAck: GMServerHubModels.PReceiveChattingMessageAllAck|null;
}

const initialState: GMServerHubState = {
    denied: false,
    connection: null,
    connectionState: HubConnectionState.Disconnected,
    serverId: '',
    serverVersion: '',
    recommendClientMasterVersion: 0,
    recommendClientUpdateVersion: 0,
    recommendClientMaintenanceVersion: 0,
    countTryConnect: 0,
    SignInAck: null,
    SignInLDAPAck: null,
    SignInEmailAck: null,
    DataTableAck: null,
    SendGameMailResultAck: null,
    InsertEventMailResultAck: null,
    UpdateEventMailResultAck: null,
    RemoveEventMailResultAck: null,
    SendUserJobAck: null,
    SignOutAck: null,
    DeniedAck: null,
    SendChattingMessageResultAck: null,
    ReceiveChattingMessageAck: null,
    TestAck: null,
    NoticeAck: null,
    ReceiveChattingMessageAllAck: null,
}

const hubSlice = createSlice({
    name: 'GMServerHub',
    initialState,
    reducers: {
        setDenied: (state, action: PayloadAction<boolean>) => {
            state.denied = action.payload;
        },
        initConnection: (state, action: PayloadAction<string>) => {
            console.log(`reducer - initConnection: ${JSON.stringify(action.payload)}`);
        },
        setConnection: (state, action: PayloadAction<HubConnection|null>) => {
            console.log(`reducer - setConnection: ${JSON.stringify(action.payload)}`);

            state.connection = action.payload;
        },
        setConnectionState: (state, action: PayloadAction<HubConnectionState>) => {
            console.log(`reducer - setConnectionState: ${JSON.stringify(action.payload)}`);

            state.connectionState = action.payload;
        },
        setConnectedAck: (state, action: PayloadAction<GMServerHubModels.PConnectedAck>) => {
            console.log(`reducer - setConnectedAck - error: ${Errors[action.payload.error]}, data: ${JSON.stringify(action.payload)}`);

            state.connectionState = HubConnectionState.Connected;
            state.serverId = action.payload.serverId;
            state.serverVersion = action.payload.serverVersion;
            state.recommendClientMasterVersion = action.payload.recommendClientMasterVersion;
            state.recommendClientUpdateVersion = action.payload.recommendClientUpdateVersion;
            state.recommendClientMaintenanceVersion = action.payload.recommendClientMaintenanceVersion;
        },
        addCountTryConnect: (state) => {
            console.log(`reducer - addCountTryConnect: ${(state.countTryConnect + 1)}`);

            state.countTryConnect += 1;
        },
        resetCountTryConnect: (state) => {
            console.log(`reducer - resetCountTryConnect`);

            state.countTryConnect = 0;
        },
        startReconnecting: (state,) => {
            console.log(`reducer - startReconnecting`);
        },
        disconnect: (state) => {
            console.log('reducer - disconnect');

            if (state.connection && HubConnectionState.Connected === state.connection.state) {
                try {
                    state.connection.stop();
                } catch (error) {
                    console.error(error)
                }
            }
        },
        setSignInAck: (state, action: PayloadAction<GMServerHubModels.PSignInAck|null>) => {
            console.log(`reducer - setSignInAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.SignInAck = action.payload;
        },
        setSignInLDAPAck: (state, action: PayloadAction<GMServerHubModels.PSignInLDAPAck|null>) => {
            console.log(`reducer - setSignInLDAPAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.SignInLDAPAck = action.payload;
        },
        setSignInEmailAck: (state, action: PayloadAction<GMServerHubModels.PSignInEmailAck|null>) => {
            console.log(`reducer - setSignInEmailAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.SignInEmailAck = action.payload;
        },
        setDataTableAck: (state, action: PayloadAction<GMServerHubModels.PDataTableAck|null>) => {
            console.log(`reducer - setDataTableAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.DataTableAck = action.payload;
        },
        setSendGameMailResultAck: (state, action: PayloadAction<GMServerHubModels.PSendGameMailResultAck|null>) => {
            console.log(`reducer - setSendGameMailResultAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.SendGameMailResultAck = action.payload;
        },
        setInsertEventMailResultAck: (state, action: PayloadAction<GMServerHubModels.PInsertEventMailResultAck|null>) => {
            console.log(`reducer - setInsertEventMailResultAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.InsertEventMailResultAck = action.payload;
        },
        setUpdateEventMailResultAck: (state, action: PayloadAction<GMServerHubModels.PUpdateEventMailResultAck|null>) => {
            console.log(`reducer - setUpdateEventMailResultAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.UpdateEventMailResultAck = action.payload;
        },
        setRemoveEventMailResultAck: (state, action: PayloadAction<GMServerHubModels.PRemoveEventMailResultAck|null>) => {
            console.log(`reducer - setRemoveEventMailResultAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.RemoveEventMailResultAck = action.payload;
        },
        setSendUserJobAck: (state, action: PayloadAction<GMServerHubModels.PSendUserJobAck|null>) => {
            console.log(`reducer - setSendUserJobAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.SendUserJobAck = action.payload;
        },
        setSignOutAck: (state, action: PayloadAction<GMServerHubModels.PSignOutAck|null>) => {
            console.log(`reducer - setSignOutAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.SignOutAck = action.payload;
        },
        setDeniedAck: (state, action: PayloadAction<GMServerHubModels.PDeniedAck|null>) => {
            console.log(`reducer - setDeniedAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.DeniedAck = action.payload;
        },
        setSendChattingMessageResultAck: (state, action: PayloadAction<GMServerHubModels.PSendChattingMessageResultAck|null>) => {
            console.log(`reducer - setSendChattingMessageResultAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.SendChattingMessageResultAck = action.payload;
        },
        setReceiveChattingMessageAck: (state, action: PayloadAction<GMServerHubModels.PReceiveChattingMessageAck|null>) => {
            console.log(`reducer - setReceiveChattingMessageAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.ReceiveChattingMessageAck = action.payload;
        },
        setTestAck: (state, action: PayloadAction<GMServerHubModels.PTestAck|null>) => {
            console.log(`reducer - setTestAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.TestAck = action.payload;
        },
        setNoticeAck: (state, action: PayloadAction<GMServerHubModels.PNoticeAck|null>) => {
            console.log(`reducer - setNoticeAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.NoticeAck = action.payload;
        },
        setReceiveChattingMessageAllAck: (state, action: PayloadAction<GMServerHubModels.PReceiveChattingMessageAllAck|null>) => {
            console.log(`reducer - setReceiveChattingMessageAllAck - error: ${(action.payload ? Errors[action.payload.error] : "")}, data: ${JSON.stringify(action.payload)}`);

            state.ReceiveChattingMessageAllAck = action.payload;
        },
        signInReq: (state, action: PayloadAction<GMServerHubModels.PSignInReq>) => {
            console.log(`reducer - signInReq - data: ${JSON.stringify(action.payload)}`);
        },
        signInLDAPReq: (state, action: PayloadAction<GMServerHubModels.PSignInLDAPReq>) => {
            console.log(`reducer - signInLDAPReq - data: ${JSON.stringify(action.payload)}`);
        },
        signInEmailReq: (state, action: PayloadAction<GMServerHubModels.PSignInEmailReq>) => {
            console.log(`reducer - signInEmailReq - data: ${JSON.stringify(action.payload)}`);
        },
        signOutReq: (state) => {
            console.log(`reducer - signOutReq`);
        },
        sendGameMailReq: (state, action: PayloadAction<GMServerHubModels.PSendGameMailReq>) => {
            console.log(`reducer - sendGameMailReq - data: ${JSON.stringify(action.payload)}`);
        },
        insertEventMailReq: (state, action: PayloadAction<GMServerHubModels.PInsertEventMailReq>) => {
            console.log(`reducer - insertEventMailReq - data: ${JSON.stringify(action.payload)}`);
        },
        updateEventMailReq: (state, action: PayloadAction<GMServerHubModels.PUpdateEventMailReq>) => {
            console.log(`reducer - updateEventMailReq - data: ${JSON.stringify(action.payload)}`);
        },
        removeEventMailReq: (state, action: PayloadAction<GMServerHubModels.PRemoveEventMailReq>) => {
            console.log(`reducer - removeEventMailReq - data: ${JSON.stringify(action.payload)}`);
        },
        sendChattingMessageReq: (state, action: PayloadAction<GMServerHubModels.PSendChattingMessageReq>) => {
            console.log(`reducer - sendChattingMessageReq - data: ${JSON.stringify(action.payload)}`);
        },
        testReq: (state, action: PayloadAction<GMServerHubModels.PTestReq>) => {
            console.log(`reducer - testReq - data: ${JSON.stringify(action.payload)}`);
        },
    }
});

export type { GMServerHubState };
export const {
    setDenied,
    initConnection,
    startReconnecting,
    setConnection,
    setConnectionState,
    setConnectedAck,
    addCountTryConnect,
    resetCountTryConnect,
    disconnect,
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
} = hubSlice.actions;
export default hubSlice.reducer;
