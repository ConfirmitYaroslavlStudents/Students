import {
  takeEvery,
  takeLatest,
  all,
  put,
  call,
  select
} from 'redux-saga/effects';
import createCandidate from '../utilities/createCandidate';
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
} from './actions';
import {
  login,
  logout
} from '../api/authorizationService';
import {
  getInitialState,
  getUserState
} from '../api/commonService';
import {
  getCandidates,
  getCandidate,
  addCandidate,
  deleteCandidate,
  updateCandidate
} from '../api/candidateService.js';
import {
  addComment,
  deleteComment,
  addCommentAttachment
} from '../api/commentService.js';
import {
  subscribe,
  unsubscribe
} from '../api/subscribeService';
import {
  noticeNotification,
  deleteNotification
} from '../api/notificationService';
import { uploadResume } from '../api/resumeService';

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
  yield takeEvery('LOGIN', loginSaga);
}

function* watchLogout() {
  yield takeEvery('LOGOUT', logoutSaga);
}

function* watchUploadResume() {
  yield takeEvery('UPLOAD_RESUME', uploadResumeSaga);
}

function* watchCandidateAdd() {
  yield takeEvery('ADD_CANDIDATE', addCandidateSaga);
}

function* watchCandidateUpdate() {
  yield takeEvery('UPDATE_CANDIDATE', updateCandidateSaga);
}

function* watchCandidateDelete() {
  yield takeEvery('DELETE_CANDIDATE', deleteCandidateSaga);
}

function* watchCommentAdd() {
  yield takeEvery('ADD_COMMENT', addCommentSaga);
}

function* watchCommentDelete() {
  yield takeEvery('DELETE_COMMENT', deleteCommentSaga);
}

function* watchSubscribe() {
  yield takeEvery('SUBSCRIBE', subscribeSaga);
}

function* watchNoticeNotification() {
  yield takeEvery('NOTICE_NOTIFICATION', noticeNotificationSaga);
}

function* watchDeleteNotification() {
  yield takeEvery('DELETE_NOTIFICATION', deleteNotificationSaga);
}

function* watchUnsubscribe() {
  yield takeEvery('UNSUBSCRIBE', unsubscribeSaga);
}

function* watchLoadCandidates() {
  yield takeLatest('LOAD_CANDIDATES', loadCandidatesSaga);
}

function* watchGetCandidate() {
  yield takeLatest('GET_CANDIDATE', getCandidateSaga);
}

function* loginSaga(action) {
  try {
    yield put(setApplicationStatus('refreshing'));
    const username = yield call(login, action.email, action.password);
    let newState = yield call(getUserState, username);
    yield put(setState({
      username: username,
      notifications: newState.notifications
    }));
    yield put(setApplicationStatus('ok'));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Incorrect login or password.'));
    yield put(setApplicationStatus('error'));
  }
}

function* logoutSaga(action) {
  try {
    yield put(setApplicationStatus('refreshing'));
    yield call(logout);
    let newState = yield call(getUserState, '');
    yield put(setState({
      username: '',
      notifications: newState.notifications
    }));
    yield put(setApplicationStatus('ok'));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Logout error.'));
    yield put(setApplicationStatus('error'));
  }
}

function* uploadResumeSaga(action) {
  try {
    yield put(setApplicationStatus('uploading-' + action.intervieweeID));
    yield call(uploadResume, action.intervieweeID, action.resume);
    yield put(uploadResumeSuccess(action.intervieweeID, action.resume.name));
    yield put(setApplicationStatus('ok'));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Upload resume error.'));
    yield put(setApplicationStatus('error'));
  }
}

function* addCandidateSaga(action) {
  try {
    yield call(addCandidate, createCandidate(action.candidate.status, action.candidate));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add candidate error.'));
    yield put(setApplicationStatus('error'));
  }
}

function* updateCandidateSaga(action) {
  try {
    yield put(setApplicationStatus('updating-' + action.candidate.id));
    yield call(updateCandidate, action.candidate);
    yield put(updateCandidateSuccess(action.candidate));
    yield put(setApplicationStatus('ok'));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Update candidate error.'));
    yield put(setApplicationStatus('error'));
  }
}

function* deleteCandidateSaga(action) {
  try {
    yield call(deleteCandidate, action.candidateID);
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete candidate error.'));
    yield put(setApplicationStatus('error'));
  }
}

function* addCommentSaga(action) {
  try {
    let comment = action.comment;
    comment.id = yield call(addComment, action.candidateID, action.comment);
    if (action.commentAttachment) {
      yield call(addCommentAttachment, action.candidateID, action.comment.id, action.commentAttachment);
    }
    yield put(addCommentSuccess(action.candidateID, comment));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add comment error.'));
  }
}

function* deleteCommentSaga(action) {
  try {
    yield call(deleteComment, action.candidateID, action.commentID);
    yield put(deleteCommentSuccess(action.candidateID, action.commentID));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete comment error.'));
  }
}

function* subscribeSaga(action) {
  try {
    yield call(subscribe, action.candidateID, action.email);
    yield put(subscribeSuccess(action.candidateID, action.email));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Subsribe error.'));
  }
}

function* unsubscribeSaga(action) {
  try {
    yield call(unsubscribe, action.candidateID, action.email);
    yield put(unsubscribeSuccess(action.candidateID, action.email));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Unsubsribe error.'));
  }
}

function* noticeNotificationSaga(action) {
  try {
    yield call(noticeNotification, action.username, action.notificationID);
    yield put(noticeNotificationSuccess(action.username, action.notificationID));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Notice notification error.'));
  }
}

function* deleteNotificationSaga(action) {
  try {
    yield call(deleteNotification, action.username, action.notificationID);
    yield put(deleteNotificationSuccess(action.username, action.notificationID));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete notification error.'));
  }
}

function* loadCandidatesSaga(action) {
  try {
    yield put(setApplicationStatus(action.stateChanges['applicationStatus'] ? action.stateChanges['applicationStatus'] : 'loading'));
    yield put(setState(action.stateChanges));
    let candidateStatus = yield select((state) => {return state.get('candidateStatus')});
    let candidatesPerPage = yield select((state) => {return state.get('candidatesPerPage')});
    let candidatesOffset = yield select((state) => {return state.get('offset')});
    let sortingField = yield select((state) => {return state.get('sortingField')});
    let sortingDirection = yield select((state) => {return state.get('sortingDirection')});
    let searchRequest = yield select((state) => {return state.get('searchRequest')});
    yield call(action.browserHistory.replace, '/'
      + (candidateStatus === '' ? '' : candidateStatus.toLowerCase() + 's')
      + '?take=' + candidatesPerPage
      + (candidatesOffset === 0 ? '' : '&skip=' + candidatesOffset)
      + (sortingField === '' ? '' : '&sort=' + sortingField + '&sortDir=' + sortingDirection)
      + (searchRequest === '' ? '' : '&q=' + encodeURIComponent(searchRequest)));
    let serverResponse = yield call(getCandidates, candidatesPerPage, candidatesOffset, candidateStatus, sortingField, sortingDirection, searchRequest);
    yield put(setState({
      candidates: serverResponse.candidates,
      totalCount: serverResponse.total
    }));
    yield put(setApplicationStatus('ok'));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Load candidates error.'));
    yield put(setApplicationStatus('error'));
  }
}

function* getCandidateSaga(action) {
  try {
    yield put(setApplicationStatus('loading'));
    let candidates = [];
    let candidate = yield call(getCandidate, action.id);
    candidates.push(candidate);
    yield put(setState({
      candidates: candidates
    }));
    yield put(setApplicationStatus('ok'));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Get candidate error.'));
    yield put(setApplicationStatus('error'));
  }
}