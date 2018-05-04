import { all, takeLatest, put, call } from 'redux-saga/effects'
import * as actions from './actions'
import * as applicationActions from '../applicationActions'
import { getTags } from '../api/tagService'

export default function* tagsSaga() {
  yield all([
    watchGetTags(),
    watchSearchByTag()
  ])
}

function* watchGetTags() {
  yield takeLatest(actions.getTags, getTagsSaga)
}

function* watchSearchByTag() {
  yield takeLatest(actions.searchByTag, searchByTagSaga)
}

function* getTagsSaga() {
  try {
    const tags = yield call(getTags)
    yield put(actions.getTagsSuccess({ tags }))
  }
  catch (error) {
    yield put(applicationActions.setErrorMessage({message: error + '. Get tags error.'}))
  }
}

function* searchByTagSaga(action) {
  try {
    const { tag } = action.payload
    yield put(applicationActions.search({ searchRequest: tag }))
  }
  catch (error) {
    yield put(applicationActions.setErrorMessage({message: error + '. Get tags error.'}))
  }
}