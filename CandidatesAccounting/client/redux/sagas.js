import {
  takeEvery,
  takeLatest,
  all,
  put,
  call,
  select
} from 'redux-saga/effects'
import {
  setState,
  updateCandidateSuccess,
  addCommentSuccess,
  deleteCommentSuccess,
  subscribeSuccess,
  unsubscribeSuccess,
  noticeNotificationSuccess,
  deleteNotificationSuccess,
  setErrorMessage,
  setApplicationStatus,
  uploadResumeSuccess,
} from './actions'
import { login, logout } from '../api/authorizationService'
import { getNotifications } from '../api/commonService'
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
    watchGetCandidate(),
  ])
}

function* watchLogin() {
  yield takeLatest('LOGIN', loginSaga)
}

function* watchLogout() {
  yield takeLatest('LOGOUT', logoutSaga)
}

function* watchUploadResume() {
  yield takeEvery('UPLOAD_RESUME', uploadResumeSaga)
}

function* watchCandidateAdd() {
  yield takeEvery('ADD_CANDIDATE', addCandidateSaga)
}

function* watchCandidateUpdate() {
  yield takeEvery('UPDATE_CANDIDATE', updateCandidateSaga)
}

function* watchCandidateDelete() {
  yield takeEvery('DELETE_CANDIDATE', deleteCandidateSaga)
}

function* watchCommentAdd() {
  yield takeEvery('ADD_COMMENT', addCommentSaga)
}

function* watchCommentDelete() {
  yield takeEvery('DELETE_COMMENT', deleteCommentSaga)
}

function* watchSubscribe() {
  yield takeEvery('SUBSCRIBE', subscribeSaga)
}

function* watchNoticeNotification() {
  yield takeEvery('NOTICE_NOTIFICATION', noticeNotificationSaga)
}

function* watchDeleteNotification() {
  yield takeEvery('DELETE_NOTIFICATION', deleteNotificationSaga)
}

function* watchUnsubscribe() {
  yield takeEvery('UNSUBSCRIBE', unsubscribeSaga)
}

function* watchLoadCandidates() {
  yield takeLatest('LOAD_CANDIDATES', loadCandidatesSaga)
}

function* watchGetCandidate() {
  yield takeLatest('GET_CANDIDATE', getCandidateSaga)
}

function* loginSaga(action) {
  try {
    yield put(setApplicationStatus('signining'))
    const username = yield call(login, action.email, action.password)
    yield put(setState({
      username,
      authorized: true
    }))
    let notifications = yield call(getNotifications, username)
    yield put(setState({ notifications }))
    yield put(setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Incorrect login or password.'))
    yield put(setApplicationStatus('error'))
  }
}

function* logoutSaga() {
  try {
    yield put(setApplicationStatus('refreshing'))
    yield call(logout)
    yield put(setState({
      username: '',
      authorized: false,
      notifications: {}
    }))
    yield put(setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Logout error.'))
    yield put(setApplicationStatus('error'))
  }
}

function* uploadResumeSaga(action) {
  try {
    yield put(setApplicationStatus('uploading-' + action.intervieweeID))
    yield call(uploadResume, action.intervieweeID, action.resume)
    yield put(uploadResumeSuccess(action.intervieweeID, action.resume.name))
    yield put(setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Upload resume error.'))
    yield put(setApplicationStatus('error'))
  }
}

function* addCandidateSaga(action) {
  try {
    yield call(addCandidate, action.candidate)
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add candidate error.'))
    yield put(setApplicationStatus('error'))
  }
}

function* updateCandidateSaga(action) {
  try {
    yield put(setApplicationStatus('updating-' + action.candidate.id))
    yield call(updateCandidate, action.candidate)
    yield put(updateCandidateSuccess(action.candidate))
    yield put(setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Update candidate error.'))
    yield put(setApplicationStatus('error'))
  }
}

function* deleteCandidateSaga(action) {
  try {
    yield call(deleteCandidate, action.candidateID)
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete candidate error.'))
    yield put(setApplicationStatus('error'))
  }
}

function* addCommentSaga(action) {
  try {
    const comment = action.comment
    comment.id = yield call(addComment, action.candidateID, action.comment)
    if (action.commentAttachment) {
      yield call(addCommentAttachment, action.candidateID, action.comment.id, action.commentAttachment)
    }
    yield put(addCommentSuccess(action.candidateID, comment))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add comment error.'))
  }
}

function* deleteCommentSaga(action) {
  try {
    yield call(deleteComment, action.candidateID, action.commentID)
    yield put(deleteCommentSuccess(action.candidateID, action.commentID))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete comment error.'))
  }
}

function* subscribeSaga(action) {
  try {
    yield call(subscribe, action.candidateID, action.email)
    yield put(subscribeSuccess(action.candidateID, action.email))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Subsribe error.'))
  }
}

function* unsubscribeSaga(action) {
  try {
    yield call(unsubscribe, action.candidateID, action.email)
    yield put(unsubscribeSuccess(action.candidateID, action.email))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Unsubsribe error.'))
  }
}

function* noticeNotificationSaga(action) {
  try {
    yield call(noticeNotification, action.username, action.notificationID)
    yield put(noticeNotificationSuccess(action.username, action.notificationID))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Notice notification error.'))
  }
}

function* deleteNotificationSaga(action) {
  try {
    yield call(deleteNotification, action.username, action.notificationID)
    yield put(deleteNotificationSuccess(action.username, action.notificationID))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete notification error.'))
  }
}

function* loadCandidatesSaga(action) {
  try {
    const applicationStatus = action.stateChanges.applicationStatus ? action.stateChanges.applicationStatus : 'loading'
    yield put(setApplicationStatus(applicationStatus))
    yield put(setState(action.stateChanges));
    const candidateStatus = yield select((state) => { return state.candidateStatus })
    const candidatesPerPage = yield select((state) => { return state.candidatesPerPage })
    const candidatesOffset = yield select((state) => { return state.offset })
    const sortingField = yield select((state) => { return state.sortingField })
    const sortingDirection = yield select((state) => { return state.sortingDirection })
    const searchRequest = yield select((state) => { return state.searchRequest })
    yield call(action.browserHistory.replace, '/'
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
    yield put(setState({
      candidates: serverResponse.candidates,
      totalCount: serverResponse.total
    }))
    yield put(setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Load candidates error.'))
    yield put(setApplicationStatus('error'))
  }
}

function* getCandidateSaga(action) {
  try {
    yield put(setApplicationStatus('loading'))
    const candidate = yield call(getCandidate, action.id)
    const candidates = {}
    candidates[candidate.id] = candidate
    yield put(setState({
      candidates: candidates
    }));
    yield put(setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Get candidate error.'))
    yield put(setApplicationStatus('error'))
  }
}