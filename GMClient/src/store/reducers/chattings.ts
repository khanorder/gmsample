import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Models } from '@ngeldata/models/index';
import { Defines } from '@ngel/data/autoDefines';
import { dayjs, Dayjs, timezoneName } from '@helpers/localizedDayjs';
import deepmerge from 'deepmerge';

interface ChattingState {
    chattingList: Models.ChattingMessage[];
};

export const initialState: ChattingState = {
    chattingList: []
}

const chattingSlice = createSlice({
    name: 'Chatting',
    initialState,
    reducers: {
        pushChattingMessage: (state, action: PayloadAction<Models.ChattingMessage|null>) => {
            if (null == action.payload)
                return;

            action.payload.localReceiveTime = dayjs().utc().toDate();
            state.chattingList.push(action.payload);
            state.chattingList = deepmerge([], state.chattingList);
        },
    }
});

export type { ChattingState };
export const { pushChattingMessage } = chattingSlice.actions;
export default chattingSlice.reducer;