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
    watchGetCandidates(),
    watchOpenCommentPage(),
    watchSearch(),
    watchSetCandidateStatus(),
    watchSetOffset(),
    watchSetCandidatePerPage(),
    watchSetSortingField(),
    watchSetSortingDirection()
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

function* watchGetCandidates() {
  yield takeLatest(A.getCandidates, getCandidatesSaga)
}

function* watchOpenCommentPage() {
  yield takeLatest(A.openCommentPage, openCommentPageSaga)
}

function* watchSearch() {
  yield takeLatest(A.search, searchSaga)
}

function* watchSetCandidateStatus() {
  yield takeLatest(A.setCandidateStatus, setCandidateStatusSaga)
}

function* watchSetOffset() {
  yield takeLatest(A.setOffset, setOffsetSaga)
}

function* watchSetCandidatePerPage() {
  yield takeLatest(A.setCandidatesPerPage, setCandidatesPerPageSaga)
}

function* watchSetSortingField() {
  yield takeLatest(A.setSortingField, setSortingFieldSaga)
}

function* watchSetSortingDirection() {
  yield takeLatest(A.setSortingDirection, setSortingDirectionSaga)
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
    yield put(A.setErrorMessage({ message: error + '. Fetching initial state error.' }))
  }
}

function* loginSaga(action) {
  try {
    const { email, password } = action.payload
    yield put(A.setAuthorizing({ authorizing: true }))
    const username = yield call(login, email, password)
    const notifications = yield call(getNotifications, username)
    yield put(A.loginSuccess({ username, notifications }))
  }
  catch(error) {
    yield put(A.setErrorMessage({ message: error + '. Incorrect login or password.' }))
  }
}

function* logoutSaga() {
  try {
    yield put(A.setAuthorizing({ authorizing: true }))
    yield call(logout)
    yield put(A.logoutSuccess())
  }
  catch(error) {
    yield put(A.setErrorMessage({ message: error + '. Logout error.' }))
  }
}

function* getCandidatesSaga(action) {
  try {
    const { history } = action.payload
    const state = yield select(state => state)
    yield call(history.replace, '/'
      + (state.candidateStatus === '' ? '' : state.candidateStatus.toLowerCase() + 's')
      + '?take=' + state.candidatesPerPage
      + (state.offset === 0 ? '' : '&skip=' + state.offset)
      + (state.sortingField === '' ? '' : '&sort=' + state.sortingField + '&sortDir=' + state.sortingDirection)
      + (state.searchRequest === '' ? '' : '&q=' + encodeURIComponent(state.searchRequest)))
    const serverResponse = yield call(
      getCandidates,
      state.candidatesPerPage,
      state.offset,
      state.candidateStatus,
      state.sortingField,
      state.sortingDirection,
      state.searchRequest)
    yield put(A.getCandidatesSuccess({
      candidates: serverResponse.candidates,
      totalCount: serverResponse.total
    }))
  }
  catch(error) {
    yield put(A.setErrorMessage({ message: error + '. Get candidates error.' }))
  }
}

function* openCommentPageSaga(action) {
  try {
    const { candidate, history } = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield call(history.replace, '/' + candidate.status.toLowerCase() + 's/' + candidate.id + '/comments')
    const commentPageOwner = yield call(getCandidate, candidate.id)
    yield put(A.openCommentPageSuccess({ candidate: commentPageOwner }))
  }
  catch(error) {
    yield put(A.setErrorMessage({ message: error + '. Open comment page error.' }))
  }
}

function* addCandidateSaga(action) {
  try {
    const { candidate, history } = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield call(addCandidate, candidate)
    yield put(A.addCandidateSuccess())
    yield put(A.getCandidates({ history }))
  }
  catch(error) {
    yield put(A.setErrorMessage({ message: error + '. Add candidate error.' }))
  }
}

function* updateCandidateSaga(action) {
  try {
    const { candidate } = action.payload
    yield put(A.setOnUpdating({ candidateId: candidate.id }))
    yield call(updateCandidate, candidate)
    yield put(A.updateCandidateSuccess({ candidate }))
  }
  catch(error) {
    yield put(A.setErrorMessage({ message: error + '. Update candidate error.' }))
  }
}

function* deleteCandidateSaga(action) {
  try {
    const { candidateId, history } = action.payload
    yield put(A.setOnDeleting({ candidateId }))
    yield call(deleteCandidate, candidateId)
    yield put(A.deleteCandidateSuccess())
    yield put(A.getCandidates({ history }))
  }
  catch(error) {
    yield put(A.setErrorMessage({ message: error + '. Delete candidate error.' }))
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

function* searchSaga(action) {
  try {
    const history = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield put(A.getCandidates({history}))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Search error.'))
  }
}

function* setCandidateStatusSaga(action) {
  try {
    const { newCandidateStatus, history } = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield put(A.setCandidateStatusSuccess(newCandidateStatus))
    yield put(A.getCandidates({history}))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Set candidate status error.'))
  }
}

function* setOffsetSaga(action) {
  try {
    const { offset, history } = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield put(A.setOffsetSuccess(offset))
    yield put(A.getCandidates({history}))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Set offset error.'))
  }
}

function* setCandidatesPerPageSaga(action) {
  try {
    const { candidatesPerPage, history } = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield put(A.setCandidatesPerPageSuccess(candidatesPerPage))
    yield put(A.getCandidates({history}))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Set candidates per page error.'))
  }
}

function* setSortingFieldSaga(action) {
  try {
    const { sortingField, history } = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield put(A.setSortingFieldSuccess(sortingField))
    yield put(A.getCandidates({history}))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Set offset error.'))
  }
}

function* setSortingDirectionSaga(action) {
  try {
    const { history } = action.payload
    yield put(A.setFetching({ fetching: true }))
    yield put(A.setSortingDirectionSuccess())
    yield put(A.getCandidates({history}))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Set offset error.'))
  }
}

function* uploadResumeSaga(action) {
  try {
    const { intervieweeId, resume } = action.payload
    yield put(A.setOnResumeUploading({ intervieweeId }))
    yield call(uploadResume, intervieweeId, resume)
    yield put(A.uploadResumeSuccess({ intervieweeId, resume: resume.name }))
  }
  catch(error) {
    yield put(A.setErrorMessage(error + '. Upload resume error.'))
  }
}