import { combineReducers, AnyAction } from 'redux';
import { routerReducer } from 'connected-next-router';
import { HYDRATE } from 'next-redux-wrapper';
import homepageSettings from './settings/hompageSettings';
import user from './user';
import layouts from './layouts';
import pages from './pages';
import chattings from './chattings';
import tables from '@ngeldata/tables/reducer';
import GMServerHub from '@ngeldata/hubs/GMServer/reducer';

const reducers = combineReducers({
    router: routerReducer,
    homepageSettings,
    user,
    layouts,
    tables,
    pages,
    chattings,
    GMServerHub
});

const rootReducer = (state: RootState | undefined, action: AnyAction) => {
    switch (action.type) {
        case HYDRATE:
            return action.payload;

        default: {
            return reducers(state, action);
        }
    }
}

export type RootState = ReturnType<typeof reducers>;
export default rootReducer;