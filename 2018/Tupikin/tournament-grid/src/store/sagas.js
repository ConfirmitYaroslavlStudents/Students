
import store from 'store/store';
import { loadingActions, namesActions } from './actions';
import { put, takeLatest } from 'redux-saga/effects';

function loaderWrapper(func) {
  return function* Loading(...args) {
    yield put(loadingActions.showLoading());
    yield func.apply(this, args);
    yield put(loadingActions.hideLoading());
  };
}

const namesSagas = {
  * ADD_NAME(data) {
    const names = store.getState().data.names;
    const update = [...names, data.payload];
    yield put(namesActions.addNameSuccess({ names: update }));
  },

  * DELETE_NAME(data) {
    const names = store.getState().data.names;
    const update = [...names.splice(names.indexOf(data.payload), 1)];
    console.log(update);
    yield put(namesActions.deleteNameSuccess({ names: update }));
  }
};

function* takeLatestGenerator(sagas) {
  for (const [actionType, saga] of Object.entries(sagas)) {
    yield takeLatest(actionType, loaderWrapper(saga));
  }
}

export default function* () {
  yield takeLatestGenerator(namesSagas);
}
