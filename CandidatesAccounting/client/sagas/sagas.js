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
  yield takeLatest(A.loadCandidates, loadCandidatesSaga)
}

function* watchGetCandidate() {
  yield takeLatest(A.getCandidate, getCandidateSaga)
}

function* loginSaga(action) {
  try {
    const { email, password } = action.payload
    yield put(A.setApplicationStatus('signining'))
    const username = yield call(login, email, password)
    yield put(A.setState({
      username,
      authorizationStatus: 'authorized'
    }))
    let notifications = yield call(getNotifications, username)
    yield put(A.setState({ notifications }))
    yield put(A.setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Incorrect login or password.'))
    yield put(A.setApplicationStatus('error'))
  }
}

function* logoutSaga() {
  try {
    yield put(A.setApplicationStatus('refreshing'))
    yield call(logout)
    yield put(A.setState({
      username: '',
      authorizationStatus: 'not-authorized',
      notifications: {}
    }))
    yield put(A.setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Logout error.'))
    yield put(A.setApplicationStatus('error'))
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

function* getCandidateSaga(action) {
  try {
    yield put(A.setApplicationStatus('loading'))
    const candidate = yield call(getCandidate, action.id)
    const candidates = {}
    candidates[candidate.id] = candidate
    yield put(A.setState({
      candidates: candidates
    }))
    yield put(A.setApplicationStatus('ok'))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Get candidate error.'))
    yield put(A.setApplicationStatus('error'))
  }
}