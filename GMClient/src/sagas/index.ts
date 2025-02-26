import { all, call, fork } from 'redux-saga/effects';
import { watchGMServerHub } from '@ngeldata/hubs/GMServer/saga';

export default function* rootSaga() {
    yield all([
        call(watchGMServerHub),
    ]);
}