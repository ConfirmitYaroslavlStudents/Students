import { all, takeLatest, put, select } from 'redux-saga/effects'
import * as actions from './applicationActions'
import * as candidatesActions from './candidates/actions'
import * as commentsActions from './comments/actions'
import * as notificationsActions from './notifications/actions'
import * as tagsActions from './tags/actions'
import getStateArgsFromURL from './utilities/getStateArgsFromURL'
import getCandidateIdFromURL from './utilities/getCandidateIdFromURL'
import { SELECTORS } from './rootReducer'

const creator = ({ history }) => {
  function* applicationSaga() {
    yield all([
      watchInit(),
      watchGetInitialStateFromServer(),
      watchSearch()
    ])
  }

  function* watchInit() {
    yield takeLatest(actions.init, initSaga)
  }

  function* watchGetInitialStateFromServer() {
    yield takeLatest(actions.getInitialStateFromServer, getInitialStateFromServerSaga)
  }

  function* watchSearch() {
    yield takeLatest(actions.search, searchSaga)
  }

  function* initSaga(action) {
    try {
      const {username} = action.payload
      const stateArgs = getStateArgsFromURL(history.location.pathname + history.location.search)
      const initialState = {
        ...stateArgs,
        username
      }
      yield put(actions.initSuccess({initialState}))
    }
    catch (error) {
      yield put(actions.setErrorMessage({message: error + '. Initializing error.'}))
    }
  }

  function* getInitialStateFromServerSaga() {
    try {
      const username = yield select(state => SELECTORS.AUTHORIZATION.USERNAME(state))
      const candidateStatus = yield select(state => SELECTORS.CANDIDATES.CANDIDATESTATUS(state))
      const candidateId = getCandidateIdFromURL(history.location.pathname)

      if (candidateId) {
        yield put(commentsActions.openCommentPage({candidate: {id: candidateId, status: candidateStatus}}))
      } else {
        yield put(candidatesActions.getCandidates())
      }

      if (username !== '') {
        yield put(notificationsActions.getNotifications({username}))
      }

      yield put(tagsActions.getTags())
    }
    catch (error) {
      yield put(actions.setErrorMessage({message: error + '. Get initial state from server error.'}))
    }
  }

  function* searchSaga(action) {
    try {
      const {searchRequest} = action.payload
      yield put(actions.enableFetching())
      yield put(actions.setSearchRequest({searchRequest}))
      yield put(candidatesActions.getCandidates())
    }
    catch (error) {
      yield put(actions.setErrorMessage({message: error + '. Search error.'}))
    }
  }

  return applicationSaga()
}

export default creator