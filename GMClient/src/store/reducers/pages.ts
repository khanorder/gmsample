import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { v4 as uuid } from 'uuid';

interface PagesState {
    servicesEventMail: string;
};

const initialState: PagesState = {
    servicesEventMail: uuid(),
}

const pagesSlice = createSlice({
    name: 'Pages',
    initialState,
    reducers: {
        updateServiceEventMail: (state) => {
            state.servicesEventMail = uuid();
        }
    }
});

export type { PagesState };
export const { updateServiceEventMail } = pagesSlice.actions;
export default pagesSlice.reducer;