import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import UAParser from 'ua-parser-js';

interface HomepageSettingsState {
    siteName: string;
    url: string;
    title: string;
    siteType: string;
    classification: string;
    description: string;
    keyword: string;
    author: string;
    companyName: string;
    companyNameEn: string;
    copyrights: string;
    copyrightsFirst: string;
    copyrightsSecond: string;
    email: string;
    replyTo: string;
    ogImageUrl: string;
    ogImageUrlTwitter: string;
    configName: string;
    uaParser: UAParser;
    clientMasterVersion: number;
    clientUpdateVersion: number;
    clientMaintenanceVersion: number;
    existsUpdate: boolean;
    notifyUpdateDone: boolean;
};

const devEnv = 'development';

const initialState: HomepageSettingsState = {
    siteName: devEnv === process.env.NODE_ENV  ? '원더러스 운영툴' : '샘플 운영툴',
    url: 'https://gm.baejangho.com',
    title: devEnv === process.env.NODE_ENV ? '원더러스 운영툴' : '샘플 운영툴',
    siteType: 'website',
    classification: 'website',
    description: devEnv === process.env.NODE_ENV ? '원더러스 운영툴' : '샘플 운영툴',
    keyword: devEnv === process.env.NODE_ENV ? '원더러스 운영툴' : '샘플 운영툴',
    author: 'BAEJANGHO.COM Co.,Ltd',
    companyName: 'BAEJANGHO.COM Co.,Ltd',
    companyNameEn: 'BAEJANGHO.COM Co.,Ltd',
    copyrights: 'Copyrights ⓒ BAEJANGHO.COM Co.,Ltd. All Rights Reserved.',
    copyrightsFirst: 'Copyrights ⓒ BAEJANGHO.COM Co.,Ltd.',
    copyrightsSecond: 'All Rights Reserved.',
    email: 'zanghobae@gmail.com',
    replyTo: 'zanghobae@gmail.com',
    ogImageUrl: '/images/logo-m3_gmbaejangho.gif',
    ogImageUrlTwitter: '/images/logo-m3_gmbaejangho.gif',
    configName: 'configSample',
    uaParser: new UAParser(),
    clientMasterVersion: 0,
    clientUpdateVersion: 240228,
    clientMaintenanceVersion: 6,
    existsUpdate: false,
    notifyUpdateDone: false
}

const homepageSettingsSlice = createSlice({
    name: 'HomepageSettings',
    initialState,
    reducers: {
        setTitle: (state, action: PayloadAction<string>) => {
            state.title = action.payload;
        },
        setCompanyName: (state, action: PayloadAction<string>) => {
            state.companyName = action.payload;
        },
        setCompanyNameEn: (state, action: PayloadAction<string>) => {
            state.companyNameEn = action.payload;
        },
        setCopyrights: (state, action: PayloadAction<string>) => {
            state.copyrights = action.payload;
        },
        setExistsUpdate: (state, action: PayloadAction<boolean>) => {
            state.existsUpdate = action.payload;
        },
        setNotifyUpdateDone: (state, action: PayloadAction<boolean>) => {
            state.notifyUpdateDone = action.payload;
        }
    }
});

export type { HomepageSettingsState };
export const { setTitle, setCopyrights, setExistsUpdate, setNotifyUpdateDone } = homepageSettingsSlice.actions;
export default homepageSettingsSlice.reducer;