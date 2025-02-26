import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import leftBarStyles from '@styles/layouts/leftBar.module.sass';

interface LayoutState {
    loadingActive: boolean;
    authActive: boolean;
    cancelRedirectUrl: string;
    cancelString: string;
    loadingMessage: string;
    leftBarActive: boolean;
    leftBarClass: string;
    leftBarWidth: number;
    notificationActive: boolean;
};

const initialState: LayoutState = {
    loadingActive: false,
    authActive: false,
    cancelRedirectUrl: '',
    cancelString: '',
    loadingMessage: '',
    leftBarActive: true,
    leftBarClass: `${leftBarStyles.leftBar} ${leftBarStyles.leftBarActive}`,
    leftBarWidth: 240,
    notificationActive: false
}

const layoutsSlice = createSlice({
    name: 'Layout',
    initialState,
    reducers: {
        setAuthActive: (state, action: PayloadAction<boolean>) => {
            state.authActive = action.payload;
        },
        startLoading: (state) => {
            state.loadingActive = true;
        },
        startLoadingMessage: (state, action: PayloadAction<string>) => {
            state.loadingMessage = action.payload;
            state.loadingActive = true;
        },
        startLoadingMessageRedirect: (state, action: PayloadAction<[string, string]>) => {
            state.loadingMessage = action.payload[0];
            state.cancelRedirectUrl = action.payload[1];
            state.loadingActive = true;
        },
        stopLoading: (state) => {
            state.loadingMessage = '';
            state.cancelRedirectUrl = '';
            state.cancelString = '';
            state.loadingActive = false;
        },
        toggleLoading: (state) => {
            if (state.loadingActive) {
                state.loadingMessage = '';
                state.cancelRedirectUrl = '';
                state.cancelString = '';
            }
            state.loadingActive = !state.loadingActive;
        },
        toggleLeftBar: (state) => {
            state.leftBarActive = !state.leftBarActive;
            if (state.leftBarActive) {
                state.leftBarClass = `${leftBarStyles.leftBar} ${leftBarStyles.leftBarActive}`;
            } else {
                state.leftBarClass = leftBarStyles.leftBar;
            }
        },
        closeLeftBar: (state) => {
            state.leftBarActive = false;
            state.leftBarClass = leftBarStyles.leftBar;
        },
        setLeftBarClass: (state, action: PayloadAction<string>) => {
            state.leftBarClass = action.payload;
        },
        toggleNotification: (state) => {
            state.notificationActive = !state.notificationActive;
        },
        openNotification: (state) => {
            state.notificationActive = true;
        },
        closeNotification: (state) => {
            state.notificationActive = false;
        }
    }
});

export type { LayoutState };
export const { toggleLeftBar, closeLeftBar, toggleLoading, startLoading, startLoadingMessage, startLoadingMessageRedirect, stopLoading, setAuthActive, toggleNotification, openNotification, closeNotification } = layoutsSlice.actions;
export default layoutsSlice.reducer;