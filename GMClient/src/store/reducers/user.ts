import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Models } from '@ngeldata/models/index';
import { Defines } from '@ngel/data/autoDefines';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import deepmerge from 'deepmerge';
import isEmpty from 'lodash/isEmpty';

interface UserState extends Models.SignInUserInterface {
    jobs: Models.UserJob[]
};

export const initialState: UserState = {
    signinId: '',
    name: '',
    email: '',
    latestSignin: new Date(),
    latestSignout: new Date(),
    latestChangePW: new Date(),
    updatedTime: new Date(),
    emailConfirmId: '',
    isEmailConfirmed: false,
    provider: Defines.OAuthProvider.None,
    menus: [],
    menusLinear: [],
    roles: [],
    jobs: []
}

const userSlice = createSlice({
    name: 'User',
    initialState,
    reducers: {
        signIn: (state, action: PayloadAction<Models.SignInUser>) => {
            if (null == action.payload) {
                // if ("production" != process.env.NODE_ENV)
                    console.log(`failed to signin`);
                return;
            } else {
                // if ("production" != process.env.NODE_ENV)
                    console.log(`succeed signin.`);
            }
            state.signinId = action.payload.signinId;
            state.name = action.payload.name;
            state.email = action.payload.email;
            state.emailConfirmId = action.payload.emailConfirmId;
            state.isEmailConfirmed = action.payload.isEmailConfirmed;
            state.provider = action.payload.provider;
            state.latestSignin = action.payload.latestSignin;
            state.latestSignout = action.payload.latestSignout;
            state.latestChangePW = action.payload.latestChangePW;
            state.updatedTime = action.payload.updatedTime;
            state.roles = action.payload.roles;
            if (!state.menus || 1 > state.menus.length) {
                state.menus = action.payload.menus;
                state.menusLinear = [];

                if (1 > state.menus.length)
                    return;
                    
                for (let i = 0; i < state.menus.length; i++) {
                    const menuDepth1 = state.menus[i];
                    if (!menuDepth1)
                        continue;
    
                    if (!menuDepth1.path)
                        continue;
    
                    state.menusLinear.push(menuDepth1);
                    if (1 > menuDepth1.children.length)
                        continue;
                        
                    for (let j = 0; j < menuDepth1.children.length; j++) {
                        const menuDepth2 = menuDepth1.children[j];
                        if (!menuDepth2)
                            continue;
    
                        if (!menuDepth2.path)
                            continue;
    
                        state.menusLinear.push(menuDepth2);
                        if (1 > menuDepth2.children.length)
                            continue;
    
                        for (let k = 0; k < menuDepth2.children.length; k++) {
                            const menuDepth3 = menuDepth2.children[k];
                            if (!menuDepth3)
                                continue;
    
                            if (!menuDepth3.path)
                                continue;
    
                            state.menusLinear.push(menuDepth3);
                        }
                    }
                }
            }
            state.jobs = [];
        },
        signOut: (state) => {
            state.signinId = '';
            state.name = '';
            state.email = '';
            state.latestSignin = new Date();
            state.latestSignout = new Date();
            state.updatedTime = new Date();
            state.emailConfirmId = '';
            state.isEmailConfirmed = false;
            state.provider = Defines.OAuthProvider.None;
            state.menus = [];
            state.menusLinear = [];
            state.roles = [];
            state.jobs = []

            if ("production" != process.env.NODE_ENV)
                console.log(`signout.`);
        },
        openMenu: (state, action: PayloadAction<[number, number?, number?]>) => {
            const menuDepth1 = state.menus[action.payload[0] - 1];
            if (action.payload[1]) {
                const menuDepth2 = menuDepth1.children[action.payload[1] - 1];
                if (action.payload[2]) {
                } else if (menuDepth2) {
                    if (0 < menuDepth2.children.length)
                        menuDepth2.opened = !menuDepth2.opened;
                }
            } else if (menuDepth1) {
                if (0 < menuDepth1.children.length)
                    menuDepth1.opened = !menuDepth1.opened;
            }

            state.menus = deepmerge([], state.menus);
        },
        updateLatestChangePW: (state) => {
            state.latestChangePW = dayjs.utc().toDate();
            // state = deepmerge([], state);
        },
        updateJob: (state, action: PayloadAction<Models.UserJob|undefined|null>) => {
            if ('undefined' == typeof action|| null == action || 'undefined' == typeof action.payload || null == action.payload)
                return;

            const exitstsIndex = state.jobs.findIndex(_ => _.id === action.payload?.id);

            if (0 > exitstsIndex) {
                state.jobs.push(action.payload);
            } else {
                state.jobs.splice(exitstsIndex, 1, action.payload);
            }
            state.jobs = deepmerge([], state.jobs);
        },
        removeJob: (state, action: PayloadAction<string>) => {
            if (isEmpty(action.payload))
                return;
            
            const exitstsIndex = state.jobs.findIndex(_ => _.id === action.payload);

            if (0 <= exitstsIndex)
                state.jobs.splice(exitstsIndex, 1);
            
            state.jobs = deepmerge([], state.jobs);
        },
        toggleJobMessage: (state, action: PayloadAction<string>) => {
            if (isEmpty(action.payload))
                return;

            const jobIndex = state.jobs.findIndex(_ => _.id === action.payload);
            if (0 <= jobIndex) {
                const job = state.jobs[jobIndex];
                if (null == job)
                    return;

                job.isOpenMessage = !job.isOpenMessage;

                state.jobs.splice(jobIndex, 1, job);
                state.jobs = deepmerge([], state.jobs);
            }
        }
    }
});

export type { UserState };
export const { signIn, signOut, openMenu, updateLatestChangePW, updateJob, removeJob, toggleJobMessage } = userSlice.actions;
export default userSlice.reducer;