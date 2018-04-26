import { takeEvery, takeLatest, all, put, call, select } from 'redux-saga/effects'
import * as applicationActions from '../actions/applicationActions'
import * as authorizationActions from '../actions/authorizationActions'
import * as candidateActions from '../actions/candidateActions'
import * as commentActions from '../actions/commentActions'
import * as notificationActions from '../actions/notificationActions'
import * as tagActions from '../actions/tagActions'
import { login, logout } from '../api/authorizationService'
import { addComment, deleteComment, addCommentAttachment } from '../api/commentService.js'
import { subscribe, unsubscribe } from '../api/subscribeService'
import { noticeNotification, deleteNotification, getNotifications } from '../api/notificationService'
import { getTags } from '../api/tagService'
import { uploadResume } from '../api/resumeService'
import { getCandidates, getCandidate, addCandidate, deleteCandidate, updateCandidate } from '../api/candidateService.js'
import Comment from '../utilities/comment'

const creator = ({ history }) => {

  function* rootSaga() {
    yield all([
      watchInitialFetch(),
      watchGetNotifications(),
      watchGetTags(),
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
      watchToggleSortingDirection()
    ])
  }

  function* watchInitialFetch() {
    yield takeLatest(applicationActions.initialServerFetch, initialFetchSaga)
  }

  function* watchGetNotifications() {
    yield takeLatest(notificationActions.getNotifications, getNotificationsSaga)
  }

  function* watchGetTags() {
    yield takeLatest(tagActions.getTags, getTagsSaga)
  }

  function* watchLogin() {
    yield takeLatest(authorizationActions.login, loginSaga)
  }

  function* watchLogout() {
    yield takeLatest(authorizationActions.logout, logoutSaga)
  }

  function* watchUploadResume() {
    yield takeEvery(candidateActions.uploadResume, uploadResumeSaga)
  }

  function* watchCandidateAdd() {
    yield takeEvery(candidateActions.addCandidate, addCandidateSaga)
  }

  function* watchCandidateUpdate() {
    yield takeEvery(candidateActions.updateCandidate, updateCandidateSaga)
  }

  function* watchCandidateDelete() {
    yield takeEvery(candidateActions.deleteCandidate, deleteCandidateSaga)
  }

  function* watchCommentAdd() {
    yield takeEvery(commentActions.addComment, addCommentSaga)
  }

  function* watchCommentDelete() {
    yield takeEvery(commentActions.deleteComment, deleteCommentSaga)
  }

  function* watchSubscribe() {
    yield takeEvery(notificationActions.subscribe, subscribeSaga)
  }

  function* watchNoticeNotification() {
    yield takeEvery(notificationActions.noticeNotification, noticeNotificationSaga)
  }

  function* watchDeleteNotification() {
    yield takeEvery(notificationActions.deleteNotification, deleteNotificationSaga)
  }

  function* watchUnsubscribe() {
    yield takeEvery(notificationActions.unsubscribe, unsubscribeSaga)
  }

  function* watchGetCandidates() {
    yield takeLatest(candidateActions.getCandidates, getCandidatesSaga)
  }

  function* watchOpenCommentPage() {
    yield takeLatest(commentActions.openCommentPage, openCommentPageSaga)
  }

  function* watchSearch() {
    yield takeLatest(applicationActions.search, searchSaga)
  }

  function* watchSetCandidateStatus() {
    yield takeLatest(candidateActions.setCandidateStatus, setCandidateStatusSaga)
  }

  function* watchSetOffset() {
    yield takeLatest(candidateActions.setOffset, setOffsetSaga)
  }

  function* watchSetCandidatePerPage() {
    yield takeLatest(candidateActions.setCandidatesPerPage, setCandidatesPerPageSaga)
  }

  function* watchSetSortingField() {
    yield takeLatest(candidateActions.setSortingField, setSortingFieldSaga)
  }

  function* watchToggleSortingDirection() {
    yield takeLatest(candidateActions.toggleSortingDirection, toggleSortingDirectionSaga)
  }

  /*___SAGAS_______________________________________________________*/

  function* initialFetchSaga(action) {
    try {
      const { username, candidateId, candidateStatus } = action.payload

      if (candidateId) {
        yield put(commentActions.openCommentPage({ candidate: { id: candidateId, status: candidateStatus }}))
      } else {
        yield put(candidateActions.getCandidates())
      }

      if (username !== '') {
        yield put(notificationActions.getNotifications({ username}))
      }

      yield put(tagActions.getTags())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Initial fetching error.'}))
    }
  }

  function* getNotificationsSaga(action) {
    try {
      const {username} = action.payload
      const notifications = yield call(getNotifications, username)
      yield put(notificationActions.getNotificationsSuccess({notifications}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Get notifications error.'}))
    }
  }

  function* getTagsSaga() {
    try {
      const tags = yield call(getTags)
      yield put(tagActions.getTagsSuccess({tags}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Get tags error.'}))
    }
  }

  function* loginSaga(action) {
    try {
      const {email, password} = action.payload
      yield put(authorizationActions.setAuthorizing({authorizing: true}))
      const username = yield call(login, email, password)
      const notifications = yield call(getNotifications, username)
      yield put(authorizationActions.loginSuccess({username, notifications}))
    }
    catch (error) {
      yield put(authorizationActions.loginFailure({ error }))
    }
  }

  function* logoutSaga() {
    try {
      yield put(authorizationActions.setAuthorizing({authorizing: true}))
      yield call(logout)
      yield put(authorizationActions.logoutSuccess())
    }
    catch (error) {
      yield put(authorizationActions.logoutFailure({ error }))
    }
  }

  function* getCandidatesSaga() {
    try {
      const state = yield select(state => state)
      const serverResponse = yield call(
        getCandidates,
        state.candidatesPerPage,
        state.offset,
        state.candidateStatus,
        state.sortingField,
        state.sortingDirection,
        state.searchRequest)
      yield call(history.replace, '/'
        + (state.candidateStatus === '' ? '' : state.candidateStatus.toLowerCase() + 's')
        + '?take=' + state.candidatesPerPage
        + (state.offset === 0 ? '' : '&skip=' + state.offset)
        + (state.sortingField === '' ? '' : '&sort=' + state.sortingField + '&sortDir=' + state.sortingDirection)
        + (state.searchRequest === '' ? '' : '&q=' + encodeURIComponent(state.searchRequest)))
      yield put(candidateActions.getCandidatesSuccess({
        candidates: serverResponse.candidates,
        totalCount: serverResponse.total
      }))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Get candidates error.'}))
    }
  }

  function* openCommentPageSaga(action) {
    try {
      const {candidate} = action.payload
      yield put(applicationActions.setFetching({fetching: true}))
      const getCandidateResult = yield call(getCandidate, candidate.id)
      yield call(history.replace, '/' + candidate.status.toLowerCase() + 's/' + candidate.id + '/comments')
      yield put(commentActions.openCommentPageSuccess({...getCandidateResult}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Open comment page error.'}))
    }
  }

  function* addCandidateSaga(action) {
    try {
      const {candidate} = action.payload
      yield put(applicationActions.setFetching({fetching: true}))
      candidate.comments = {}
      candidate.comments['initialStatus'] = new Comment('SYSTEM', ' Initial status: ' + candidate.status)
      yield call(addCandidate, candidate)
      yield put(candidateActions.addCandidateSuccess())
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Add candidate error.'}))
    }
  }

  function* updateCandidateSaga(action) {
    try {
      const {candidate, previousStatus} = action.payload
      const candidateStatus = yield select(state => state.candidateStatus)
      candidate.comments = {}
      if (candidate.status !== previousStatus) {
        candidate.comments['newStatus'] = new Comment('SYSTEM', ' New status: ' + candidate.status)
      }
      yield put(candidateActions.setOnUpdating({candidateId: candidate.id}))
      yield call(updateCandidate, candidate)
      yield put(candidateActions.updateCandidateSuccess({candidate, shouldMoveToAnotherTable: candidateStatus !== '' && candidateStatus !== candidate.status}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Update candidate error.'}))
    }
  }

  function* deleteCandidateSaga(action) {
    try {
      const {candidateId} = action.payload
      yield put(candidateActions.setOnDeleting({candidateId}))
      yield call(deleteCandidate, candidateId)
      yield put(candidateActions.deleteCandidateSuccess())
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Delete candidate error.'}))
    }
  }

  function* addCommentSaga(action) {
    try {
      const {candidateId, comment, commentAttachment} = action.payload
      comment.id = yield call(addComment, candidateId, comment)
      if (commentAttachment) {
        yield call(addCommentAttachment, candidateId, comment.id, commentAttachment)
      }
      yield put(commentActions.addCommentSuccess({candidateId, comment}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Add comment error.'}))
    }
  }

  function* deleteCommentSaga(action) {
    try {
      const {candidateId, commentId} = action.payload
      yield call(deleteComment, candidateId, commentId)
      yield put(commentActions.deleteCommentSuccess({candidateId, commentId}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Delete comment error.'}))
    }
  }

  function* subscribeSaga(action) {
    try {
      const {candidateId, email} = action.payload
      yield call(subscribe, candidateId, email)
      yield put(notificationActions.subscribeSuccess({candidateId, email}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Subsribe error.'}))
    }
  }

  function* unsubscribeSaga(action) {
    try {
      const {candidateId, email} = action.payload
      yield call(unsubscribe, candidateId, email)
      yield put(notificationActions.unsubscribeSuccess({candidateId, email}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Unsubsribe error.'}))
    }
  }

  function* noticeNotificationSaga(action) {
    try {
      const {username, notificationId} = action.payload
      yield call(noticeNotification, username, notificationId)
      yield put(notificationActions.noticeNotificationSuccess({notificationId}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Notice notification error.'}))
    }
  }

  function* deleteNotificationSaga(action) {
    try {
      const {username, notificationId} = action.payload
      yield call(deleteNotification, username, notificationId)
      yield put(notificationActions.deleteNotificationSuccess({notificationId}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Delete notification error.'}))
    }
  }

  function* searchSaga(action) {
    try {
      const { searchRequest } = action.payload
      yield put(applicationActions.setFetching({fetching: true}))
      yield put(applicationActions.setSearchRequest({ searchRequest }))
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Search error.'}))
    }
  }

  function* setCandidateStatusSaga(action) {
    try {
      const {newCandidateStatus} = action.payload
      yield put(applicationActions.setFetching({fetching: true}))
      yield put(candidateActions.setCandidateStatusSuccess({status: newCandidateStatus}))
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set candidate status error.'}))
    }
  }

  function* setOffsetSaga(action) {
    try {
      const {offset} = action.payload
      yield put(applicationActions.setFetching({fetching: true}))
      yield put(candidateActions.setOffsetSuccess(offset))
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* setCandidatesPerPageSaga(action) {
    try {
      const {candidatesPerPage} = action.payload
      yield put(applicationActions.setFetching({fetching: true}))
      yield put(candidateActions.setCandidatesPerPageSuccess(candidatesPerPage))
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set candidates per page error.'}))
    }
  }

  function* setSortingFieldSaga(action) {
    try {
      const {sortingField} = action.payload
      yield put(applicationActions.setFetching({fetching: true}))
      yield put(candidateActions.setSortingFieldSuccess(sortingField))
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* toggleSortingDirectionSaga() {
    try {
      yield put(applicationActions.setFetching({fetching: true}))
      yield put(candidateActions.toggleSortingDirectionSuccess())
      yield put(candidateActions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* uploadResumeSaga(action) {
    try {
      const {intervieweeId, resume} = action.payload
      yield put(candidateActions.setOnResumeUploading({intervieweeId}))
      yield call(uploadResume, intervieweeId, resume)
      yield put(candidateActions.uploadResumeSuccess({intervieweeId, resume: resume.name}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Upload resume error.'}))
    }
  }

  return rootSaga()
}

export default creator