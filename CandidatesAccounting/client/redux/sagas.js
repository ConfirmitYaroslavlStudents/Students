import {delay} from 'redux-saga';
import {takeEvery, takeLatest, all, put, call, select} from 'redux-saga/effects';
import {getInitialState} from '../api/commonService';
import {login, logout} from '../api/authorizationService';
import {getCandidates, addCandidate, deleteCandidate, updateCandidate} from '../api/candidateService.js';
import {addComment, deleteComment} from '../api/commentService.js';
import {subscribe, unsubscribe} from '../api/subscribeService';
import {noticeNotification, deleteNotification} from '../api/notificationService';
import {setInitialState, loginSuccess, logoutSuccess, addCandidateSuccess, deleteCandidateSuccess, updateCandidateSuccess, addCommentSuccess, deleteCommentSuccess,
        subscribeSuccess, unsubscribeSuccess, noticeNotificationSuccess, deleteNotificationSuccess, setErrorMessage, search, setStatus} from './actions';
import createCandidate from '../utilities/createCandidate';

export default function* rootSaga() {
  yield all([
    watchLogin(),
    watchLogout(),
    watchCandidateAdd(),
    watchCandidateUpdate(),
    watchCandidateDelete(),
    watchCommentAdd(),
    watchCommentDelete(),
    watchSubscribe(),
    watchUnsubscribe(),
    watchNoticeNotification(),
    watchDeleteNotification(),
    watchChangeSearchRequest(),
    watchSearch(),
    watchChangeURL(),
  ])
}

function* watchLogin() {
  yield takeEvery('LOGIN', loginSaga);
}

function* watchLogout() {
  yield takeEvery('LOGOUT', logoutSaga);
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

function* watchChangeSearchRequest() {
  yield takeLatest('SET_SEARCH_REQUEST', setSearchRequestSaga);
}

function* watchSearch() {
  yield takeLatest('SEARCH', searchSaga);
}

function* watchChangeURL() {
  yield takeLatest('CHANGE_URL', changeURLSaga);
}

function* loginSaga(action) {
  try {
    const userName = yield call(login, action.email, action.password);
    yield put(loginSuccess(userName));
    let newState = yield call(getInitialState, userName);
    yield put(setInitialState({
      userName: userName,
      authorizationStatus: 'authorized',
      tags: newState.tags,
      notifications: newState.notifications
    }));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Incorrect login or password.'));
  }
}

function* logoutSaga(action) {
  try {
    yield call(logout);
    yield put(logoutSuccess());
    let newState = yield call(getInitialState, '');
    yield put(setInitialState({
      userName: '',
      authorizationStatus: 'not-authorized',
      tags: newState.tags,
      notifications: newState.notifications
    }));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Logout error.'));
  }
}

function* addCandidateSaga(action) {
  try {
    let newCandidate = createCandidate(action.candidate.status, action.candidate);
    newCandidate.id = yield call(addCandidate, newCandidate);
    yield put(addCandidateSuccess(newCandidate));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add candidate error.'));
  }
}

function* updateCandidateSaga(action) {
  try {
    yield call(updateCandidate, action.candidate);
    yield put(updateCandidateSuccess(action.candidate));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Update candidate error.'));
  }
}

function* deleteCandidateSaga(action) {
  try {
    yield call(deleteCandidate, action.candidateID);
    yield put(deleteCandidateSuccess(action.candidateID));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete candidate error.'));
  }
}

function* addCommentSaga(action) {
  try {
    let comment = action.comment;
    comment.id = yield call(addComment, action.candidateID, action.comment);
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

function* setSearchRequestSaga(action) {
  yield call(delay, action.delay);
  yield put(search(action.searchRequest, action.browserHistory));
}

function* searchSaga(action) {
  let newURL = '';
  if (action.searchRequest.trim() !== '') {
    newURL = action.browserHistory.location.pathname + '?q=' + encodeURIComponent(action.searchRequest);
  } else {
    newURL = action.browserHistory.location.pathname;
  }
  action.browserHistory.replace(newURL);
}

function* changeURLSaga(action) {
  try {
    yield put(setStatus('loading'));
    action.browserHistory.replace(action.newURL);
    let separatedURL = separateURL(action.newURL);
    let candidatesPerPage = yield select((state) => {return state.get('candidatesPerPage')});
    let serverResponse = yield call(getCandidates, separatedURL.args.take ? Number(separatedURL.args.take) : candidatesPerPage,
      separatedURL.args.skip ? separatedURL.args.skip : 0, separatedURL.candidateStatus);
    yield put(setInitialState({
      candidates: serverResponse.candidates,
      candidatesOffset: separatedURL.args.skip ? Number(separatedURL.args.skip) : 0,
      candidatesPerPage: separatedURL.args.take ? Number(separatedURL.args.take) : candidatesPerPage,
      candidatesTotalCount: serverResponse.total
    }));

    yield put(setStatus('ok'));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. ChangeURL notification error.'));
  }
}

function separateURL(url) {
  let splitedURL = url.split('?');
  let path = splitedURL[0];
  let candidateStatus = '';
  switch (path.split('/')[1]) {
    case 'interviewees':
      candidateStatus = 'Interviewee';
      break;
    case 'students':
      candidateStatus = 'Student';
      break;
    case 'trainees':
      candidateStatus = 'Trainee';
      break;
  }
  let args = splitedURL[1];
  let argsObject = {};
  if (args) {
    let argsArray = args.split('&');
    argsArray.forEach((arg) => {
      let splited = arg.split('=');
      argsObject[splited[0]] = splited[1];
    });
  }
  return {
    candidateStatus: candidateStatus,
    args: argsObject
  }
}