import { loadingActions } from './actions';
import { put, takeLatest } from 'redux-saga/effects';

function loaderWrapper(func) {
  return function* Loading(...args) {
    yield put(loadingActions.showLoading());
    yield func.apply(this, args);
    yield put(loadingActions.hideLoading());
  };
}

function* takeLatestGenerator(sagas) {
  for (const [actionType, saga] of Object.entries(sagas)) {
    yield takeLatest(actionType, loaderWrapper(saga));
  }
}

export default function* () {
  yield takeLatestGenerator(null);
}
