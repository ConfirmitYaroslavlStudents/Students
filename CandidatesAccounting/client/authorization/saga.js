import { all, takeLatest, call, put } from 'redux-saga/effects'
import * as actions from './actions'
import * as applicationActions from '../applicationActions'
import * as candidateActions from '../candidates/actions'
import * as notificationActions from '../notifications/actions'
import * as tagActions from '../tags/actions'
import { login, logout } from '../api/authorizationService'

export default  function* authorizationSaga() {
  yield all([
    watchLogin(),
    watchLogout()
  ])
}

function* watchLogin() {
  yield takeLatest(actions.login, loginSaga)
}

function* watchLogout() {
  yield takeLatest(actions.logout, logoutSaga)
}

function* loginSaga(action) {
  try {
    const { email, password } = action.payload

    yield put(actions.enableAuthorizing())
    const username = yield call(login, email, password)

    yield put(notificationActions.getNotifications({ username }))
    yield put(tagActions.getTags())

    yield put(actions.loginSuccess({ username }))

    yield put(applicationActions.enableInitializing())
    yield put(candidateActions.getCandidates())
  }
  catch (error) {
    yield put(actions.loginFailure({error}))
  }
}

function* logoutSaga() {
  try {
    yield put(actions.enableAuthorizing())
    yield call(logout)
    yield put(actions.logoutSuccess())
  }
  catch (error) {
    yield put(actions.logoutFailure({error}))
  }
}