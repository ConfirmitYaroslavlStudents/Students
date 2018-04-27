import { takeEvery, takeLatest, all, put, call, select } from 'redux-saga/effects'
import A from '../actions'
import { login, logout } from '../api/authorizationService'
import { addComment, deleteComment, addCommentAttachment } from '../api/commentService.js'
import { subscribe, unsubscribe } from '../api/subscribeService'
import { noticeNotification, deleteNotification, getNotifications } from '../api/notificationService'
import { getTags } from '../api/tagService'
import { uploadResume } from '../api/resumeService'
import { getCandidates, getCandidate, addCandidate, deleteCandidate, updateCandidate } from '../api/candidateService.js'
import getStateArgsFromURL from '../utilities/getStateArgsFromURL'
import getCandidateIdFromURL from '../utilities/getCandidateIdFromURL'
import Comment from '../utilities/comment'

const creator = ({ history }) => {

  function* rootSaga() {
    yield all([
      watchInit(),
      watchGetInitialStateFromServer(),
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

  function* watchInit() {
    yield takeLatest(A.init, initSaga)
  }

  function* watchGetInitialStateFromServer() {
    yield takeLatest(A.getInitialStateFromServer, getInitialStateFromServerSaga)
  }

  function* watchGetNotifications() {
    yield takeLatest(A.getNotifications, getNotificationsSaga)
  }

  function* watchGetTags() {
    yield takeLatest(A.getTags, getTagsSaga)
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

  function* watchToggleSortingDirection() {
    yield takeLatest(A.toggleSortingDirection, toggleSortingDirectionSaga)
  }

  /*___SAGAS_______________________________________________________*/

  function* initSaga(action) {
    try {
      const { username } = action.payload
      const stateArgs = getStateArgsFromURL(history.location.pathname + history.location.search)
      const initialState = {
        ...stateArgs,
        username
      }
      yield put(A.initSuccess({ initialState }))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Initializing error.'}))
    }
  }

  function* getInitialStateFromServerSaga() {
    try {
      const username = yield select(state => state.authorization.username)
      const candidateStatus = yield select(state => state.candidates.candidateStatus)
      const candidateId = getCandidateIdFromURL(history.location.pathname)

      if (candidateId) {
        yield put(A.openCommentPage({ candidate: { id: candidateId, status: candidateStatus }}))
      } else {
        yield put(A.getCandidates())
      }

      if (username !== '') {
        yield put(A.getNotifications({ username }))
      }

      yield put(A.getTags())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Get initial state from server error.'}))
    }
  }

  function* getNotificationsSaga(action) {
    try {
      const {username} = action.payload
      const notifications = yield call(getNotifications, username)
      yield put(A.getNotificationsSuccess({notifications}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Get notifications error.'}))
    }
  }

  function* getTagsSaga() {
    try {
      const tags = yield call(getTags)
      yield put(A.getTagsSuccess({tags}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Get tags error.'}))
    }
  }

  function* loginSaga(action) {
    try {
      const {email, password} = action.payload
      yield put(A.enableAuthorizing())
      const username = yield call(login, email, password)
      const notifications = yield call(getNotifications, username)
      yield put(A.loginSuccess({username, notifications}))
    }
    catch (error) {
      yield put(A.loginFailure({ error }))
    }
  }

  function* logoutSaga() {
    try {
      yield put(A.enableAuthorizing())
      yield call(logout)
      yield put(A.logoutSuccess())
    }
    catch (error) {
      yield put(A.logoutFailure({ error }))
    }
  }

  function* getCandidatesSaga() {
    try {
      const candidateState = yield select(state => state.candidates)
      const searchRequest = yield select(state => state.application.searchRequest)
      const serverResponse = yield call(
        getCandidates,
        candidateState.candidatesPerPage,
        candidateState.offset,
        candidateState.candidateStatus,
        candidateState.sortingField,
        candidateState.sortingDirection,
        searchRequest)
      yield call(history.replace, '/'
        + (candidateState.candidateStatus === '' ? '' : candidateState.candidateStatus.toLowerCase() + 's')
        + '?take=' + candidateState.candidatesPerPage
        + (candidateState.offset === 0 ? '' : '&skip=' + candidateState.offset)
        + (candidateState.sortingField === '' ? '' : '&sort=' + candidateState.sortingField + '&sortDir=' + candidateState.sortingDirection)
        + (searchRequest === '' ? '' : '&q=' + encodeURIComponent(searchRequest)))
      yield put(A.getCandidatesSuccess({
        candidates: serverResponse.candidates,
        totalCount: serverResponse.total
      }))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Get candidates error.'}))
    }
  }

  function* openCommentPageSaga(action) {
    try {
      const {candidate} = action.payload
      yield put(A.enableFetching())
      const getCandidateResult = yield call(getCandidate, candidate.id)
      yield call(history.replace, '/' + candidate.status.toLowerCase() + 's/' + candidate.id + '/comments')
      yield put(A.openCommentPageSuccess({...getCandidateResult}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Open comment page error.'}))
    }
  }

  function* addCandidateSaga(action) {
    try {
      const {candidate} = action.payload
      yield put(A.enableFetching())
      candidate.comments = {}
      candidate.comments['initialStatus'] = new Comment('SYSTEM', ' Initial status: ' + candidate.status)
      yield call(addCandidate, candidate)
      yield put(A.addCandidateSuccess())
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Add candidate error.'}))
    }
  }

  function* updateCandidateSaga(action) {
    try {
      const {candidate, previousStatus} = action.payload
      const candidateStatus = yield select(state => state.candidates.candidateStatus)
      candidate.comments = {}
      if (candidate.status !== previousStatus) {
        candidate.comments['newStatus'] = new Comment('SYSTEM', ' New status: ' + candidate.status)
      }
      yield put(A.setOnUpdating({candidateId: candidate.id}))
      yield call(updateCandidate, candidate)
      yield put(A.updateCandidateSuccess({candidate, shouldMoveToAnotherTable: candidateStatus !== '' && candidateStatus !== candidate.status}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Update candidate error.'}))
    }
  }

  function* deleteCandidateSaga(action) {
    try {
      const {candidateId} = action.payload
      yield put(A.setOnDeleting({candidateId}))
      yield call(deleteCandidate, candidateId)
      yield put(A.deleteCandidateSuccess())
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Delete candidate error.'}))
    }
  }

  function* addCommentSaga(action) {
    try {
      const {candidateId, comment, commentAttachment} = action.payload
      comment.id = yield call(addComment, candidateId, comment)
      if (commentAttachment) {
        yield call(addCommentAttachment, candidateId, comment.id, commentAttachment)
      }
      yield put(A.addCommentSuccess({candidateId, comment}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Add comment error.'}))
    }
  }

  function* deleteCommentSaga(action) {
    try {
      const {candidateId, commentId} = action.payload
      yield call(deleteComment, candidateId, commentId)
      yield put(A.deleteCommentSuccess({candidateId, commentId}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Delete comment error.'}))
    }
  }

  function* subscribeSaga(action) {
    try {
      const {candidateId} = action.payload
      const username = yield select(state => state.authorization.username)
      yield call(subscribe, candidateId, username)
      yield put(A.subscribeSuccess({ candidateId, username }))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Subsribe error.'}))
    }
  }

  function* unsubscribeSaga(action) {
    try {
      const {candidateId} = action.payload
      const username = yield select(state => state.authorization.username)
      yield call(unsubscribe, candidateId, username)
      yield put(A.unsubscribeSuccess({ candidateId, username }))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Unsubsribe error.'}))
    }
  }

  function* noticeNotificationSaga(action) {
    try {
      const {username, notificationId} = action.payload
      yield call(noticeNotification, username, notificationId)
      yield put(A.noticeNotificationSuccess({notificationId}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Notice notification error.'}))
    }
  }

  function* deleteNotificationSaga(action) {
    try {
      const {username, notificationId} = action.payload
      yield call(deleteNotification, username, notificationId)
      yield put(A.deleteNotificationSuccess({notificationId}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Delete notification error.'}))
    }
  }

  function* searchSaga(action) {
    try {
      const { searchRequest } = action.payload
      yield put(A.enableFetching())
      yield put(A.setSearchRequest({ searchRequest }))
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Search error.'}))
    }
  }

  function* setCandidateStatusSaga(action) {
    try {
      const {newCandidateStatus} = action.payload
      yield put(A.enableFetching())
      yield put(A.setCandidateStatusSuccess({status: newCandidateStatus}))
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Set candidate status error.'}))
    }
  }

  function* setOffsetSaga(action) {
    try {
      const {offset} = action.payload
      yield put(A.enableFetching())
      yield put(A.setOffsetSuccess(offset))
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* setCandidatesPerPageSaga(action) {
    try {
      const {candidatesPerPage} = action.payload
      yield put(A.enableFetching())
      yield put(A.setCandidatesPerPageSuccess(candidatesPerPage))
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Set candidates per page error.'}))
    }
  }

  function* setSortingFieldSaga(action) {
    try {
      const {sortingField} = action.payload
      yield put(A.enableFetching())
      yield put(A.setSortingFieldSuccess(sortingField))
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* toggleSortingDirectionSaga() {
    try {
      yield put(A.enableFetching())
      yield put(A.toggleSortingDirectionSuccess())
      yield put(A.getCandidates())
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* uploadResumeSaga(action) {
    try {
      const {intervieweeId, resume} = action.payload
      yield put(A.setOnResumeUploading({intervieweeId}))
      yield call(uploadResume, intervieweeId, resume)
      yield put(A.uploadResumeSuccess({intervieweeId, resume: resume.name}))
    }
    catch (error) {
      yield put(A.setErrorMessage({message: error + '. Upload resume error.'}))
    }
  }

  return rootSaga()
}

export default creator