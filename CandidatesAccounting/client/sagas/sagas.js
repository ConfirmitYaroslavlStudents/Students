import {
  takeEvery,
  takeLatest,
  all,
  put,
  call,
  select
} from 'redux-saga/effects'
import * as A from '../actions/actions'
import { login, logout } from '../api/authorizationService'
import { getInitialState, getNotifications } from '../api/commonService'
import { addComment, deleteComment, addCommentAttachment } from '../api/commentService.js'
import { subscribe, unsubscribe } from '../api/subscribeService'
import { noticeNotification, deleteNotification } from '../api/notificationService'
import { uploadResume } from '../api/resumeService'
import {
  getCandidates,
  getCandidate,
  addCandidate,
  deleteCandidate,
  updateCandidate
} from '../api/candidateService.js'

export default function* rootSaga() {
  yield all([
    watchFetchInitialState(),
    watchLogin(),
    watchLogout(),
    watchUploadResume(),
    watchCandidateAdd(),
    watchCandidateUpdate(),
    watchCandidateDelete(),
    watchCommentAdd(),
    watchCommentDelete(),
    watchSubscribe(),
    watchUnsubscribe(),
    watchNoticeNotification(),
    watchDeleteNotification(),
    watchLoadCandidates(),
    watchGetCandidates(),
    watchGetCandidate(),
    watchSearch(),
    watchTableChange(),
  ])
}

function* watchFetchInitialState() {
  yield takeLatest(A.fetchInitialState, fetchInitialStateSaga)
}

function* watchLogin() {
  yield takeLatest(A.login, loginSaga)
}

function* watchLogout() {
  yield takeLatest(A.logout, logoutSaga)
}

function* watchUploadResume() {
  yield takeEvery(A.uploadResume, uploadResumeSaga)
}

function* watchCandidateAdd() {
  yield takeEvery(A.addCandidate, addCandidateSaga)
}

function* watchCandidateUpdate() {
  yield takeEvery(A.updateCandidate, updateCandidateSaga)
}

function* watchCandidateDelete() {
  yield takeEvery(A.deleteCandidate, deleteCandidateSaga)
}

function* watchCommentAdd() {
  yield takeEvery(A.addComment, addCommentSaga)
}

function* watchCommentDelete() {
  yield takeEvery(A.deleteComment, deleteCommentSaga)
}

function* watchSubscribe() {
  yield takeEvery(A.subscribe, subscribeSaga)
}

function* watchNoticeNotification() {
  yield takeEvery(A.noticeNotification, noticeNotificationSaga)
}

function* watchDeleteNotification() {
  yield takeEvery(A.deleteNotification, deleteNotificationSaga)
}

function* watchUnsubscribe() {
  yield takeEvery(A.unsubscribe, unsubscribeSaga)
}

function* watchLoadCandidates() {
  yield takeLatest(A.refreshRows, loadCandidatesSaga)
}

function* watchGetCandidates() {
  yield takeLatest(A.getCandidates, getCandidatesSaga)
}

function* watchGetCandidate() {
  yield takeLatest(A.getCandidate, getCandidateSaga)
}

function* watchSearch() {
  yield takeLatest(A.search, searchSaga)
}

function* watchTableChange() {
  yield takeLatest(A.changeTable, changeTableSaga)
}

/*___SAGAS_______________________________________________________*/

function* fetchInitialStateSaga() {
  try {
    const state = yield select(state => state)
    const initialState = yield call(getInitialState,
      state.username,
      state.candidatesPerPage,
      state.offset,
      state.candidateStatus,
      state.sortingField,
      state.sortingDirection,
      state.searchRequest)
    yield put(A.fetchInitialStateSuccess(initialState))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Incorrect login or password.'))
  }
}

function* loginSaga(action) {
  try {
    const { email, password } = action.payload
    yield put(A.setAuthorizingStatus(true))
    const username = yield call(login, email, password)
    let notifications = yield call(getNotifications, username)
    yield put(A.loginSuccess({username, notifications}))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Incorrect login or password.'))
  }
}

function* logoutSaga() {
  try {
    yield put(A.setAuthorizingStatus(true))
    yield call(logout)
    yield put(A.logoutSuccess())
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Logout error.'))
  }
}

function* uploadResumeSaga(action) {
  try {
    yield put(A.setApplicationStatus('uploading-' + action.intervieweeID))
    yield call(uploadResume, action.intervieweeID, action.resume)
    yield put(A.uploadResumeSuccess(action.intervieweeID, action.resume.name))
    yield put(A.setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Upload resume error.'))
    yield put(A.setApplicationStatus('error'))
  }
}

function* addCandidateSaga(action) {
  try {
    yield call(addCandidate, action.candidate)
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Add candidate error.'))
    yield put(A.setApplicationStatus('error'))
  }
}

function* updateCandidateSaga(action) {
  try {
    yield put(A.setApplicationStatus('updating-' + action.candidate.id))
    yield call(updateCandidate, action.candidate)
    yield put(A.updateCandidateSuccess(action.candidate))
    yield put(A.setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Update candidate error.'))
    yield put(A.setApplicationStatus('error'))
  }
}

function* deleteCandidateSaga(action) {
  try {
    yield call(deleteCandidate, action.candidateID)
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Delete candidate error.'))
    yield put(A.setApplicationStatus('error'))
  }
}

function* addCommentSaga(action) {
  try {
    const comment = action.comment
    comment.id = yield call(addComment, action.candidateID, action.comment)
    if (action.commentAttachment) {
      yield call(addCommentAttachment, action.candidateID, action.comment.id, action.commentAttachment)
    }
    yield put(A.addCommentSuccess(action.candidateID, comment))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Add comment error.'))
  }
}

function* deleteCommentSaga(action) {
  try {
    yield call(deleteComment, action.candidateID, action.commentID)
    yield put(A.deleteCommentSuccess(action.candidateID, action.commentID))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Delete comment error.'))
  }
}

function* subscribeSaga(action) {
  try {
    yield call(subscribe, action.candidateID, action.email)
    yield put(A.subscribeSuccess(action.candidateID, action.email))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Subsribe error.'))
  }
}

function* unsubscribeSaga(action) {
  try {
    yield call(unsubscribe, action.candidateID, action.email)
    yield put(A.unsubscribeSuccess(action.candidateID, action.email))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Unsubsribe error.'))
  }
}

function* noticeNotificationSaga(action) {
  try {
    yield call(noticeNotification, action.username, action.notificationID)
    yield put(A.noticeNotificationSuccess(action.username, action.notificationID))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Notice notification error.'))
  }
}

function* deleteNotificationSaga(action) {
  try {
    yield call(deleteNotification, action.username, action.notificationID)
    yield put(A.deleteNotificationSuccess(action.username, action.notificationID))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Delete notification error.'))
  }
}

function* loadCandidatesSaga(action) {
  try {
    const applicationStatus = action.payload.applicationStatus ? action.payload.applicationStatus : 'loading'
    yield put(A.setApplicationStatus(applicationStatus))
    yield put(A.setState(action.payload));
    const candidateStatus = yield select((state) => { return state.candidateStatus })
    const candidatesPerPage = yield select((state) => { return state.candidatesPerPage })
    const candidatesOffset = yield select((state) => { return state.offset })
    const sortingField = yield select((state) => { return state.sortingField })
    const sortingDirection = yield select((state) => { return state.sortingDirection })
    const searchRequest = yield select((state) => { return state.searchRequest })
    yield call(action.payload.history.replace, '/'
      + (candidateStatus === '' ? '' : candidateStatus.toLowerCase() + 's')
      + '?take=' + candidatesPerPage
      + (candidatesOffset === 0 ? '' : '&skip=' + candidatesOffset)
      + (sortingField === '' ? '' : '&sort=' + sortingField + '&sortDir=' + sortingDirection)
      + (searchRequest === '' ? '' : '&q=' + encodeURIComponent(searchRequest)))
    const serverResponse = yield call(
      getCandidates,
      candidatesPerPage,
      candidatesOffset,
      candidateStatus,
      sortingField,
      sortingDirection,
      searchRequest)
    yield put(A.setState({
      candidates: serverResponse.candidates,
      totalCount: serverResponse.total
    }))
    yield put(A.setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Load candidates error.'))
    yield put(A.setApplicationStatus('error'))
  }
}

function* getCandidatesSaga(action) {
  try {
    const history = action.payload
    const candidateStatus = yield select(state => state.candidateStatus)
    const candidatesPerPage = yield select(state => state.candidatesPerPage)
    const candidatesOffset = yield select(state => state.offset)
    const sortingField = yield select(state => state.sortingField)
    const sortingDirection = yield select(state => state.sortingDirection)
    const searchRequest = yield select(state => state.searchRequest)
    yield call(history.replace, '/'
      + (candidateStatus === '' ? '' : candidateStatus.toLowerCase() + 's')
      + '?take=' + candidatesPerPage
      + (candidatesOffset === 0 ? '' : '&skip=' + candidatesOffset)
      + (sortingField === '' ? '' : '&sort=' + sortingField + '&sortDir=' + sortingDirection)
      + (searchRequest === '' ? '' : '&q=' + encodeURIComponent(searchRequest)))
    const serverResponse = yield call(
      getCandidates,
      candidatesPerPage,
      candidatesOffset,
      candidateStatus,
      sortingField,
      sortingDirection,
      searchRequest)
    yield put(A.getCandidatesSuccess({
      candidates: serverResponse.candidates,
      totalCount: serverResponse.total
    }))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Get candidates error.'))
  }
}

function* getCandidateSaga(action) {
  try {
    const { candidateId } = action.payload
    yield put(A.setFetchStatus(true))
    const candidate = yield call(getCandidate, candidateId)
    yield put(A.getCandidatesSuccess(candidate))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Get candidate error.'))
  }
}

function* searchSaga(action) {
  try {
    const history = action.payload
    yield put(A.setFetchStatus(true))
    yield put(A.getCandidates(history))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Search error.'))
  }
}

function* changeTableSaga(action) {
  try {
    const { newCandidateStatus, history } = action.payload
    yield put(A.setFetchStatus(true))
    yield put(A.changeTableSuccess(newCandidateStatus))
    yield put(A.getCandidates(history))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Search error.'))
  }
}