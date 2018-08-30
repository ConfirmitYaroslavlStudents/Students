import store from 'store/store';
import { gridActions, loadingActions, namesActions } from './actions';
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
    const names = [...store.getState().data.names, data.payload];
    yield put(namesActions.addNameSuccess({ names }));
  },

  * DELETE_NAME(data) {
    const names = [...store.getState().data.names];
    names.splice(names.indexOf(data.payload), 1);
    yield put(namesActions.deleteNameSuccess({ names }));
  }
};

const gridSagas = {
  * ADD_GRID_TYPE(data) {
    const type = data.payload;
    yield put(gridActions.addGridTypeSuccess({ type }));
  }
};

function* takeLatestGenerator(sagas) {
  for (const [actionType, saga] of Object.entries(sagas)) {
    yield takeLatest(actionType, loaderWrapper(saga));
  }
}

export default function* () {
  yield* takeLatestGenerator(gridSagas);
  yield* takeLatestGenerator(namesSagas);
}
