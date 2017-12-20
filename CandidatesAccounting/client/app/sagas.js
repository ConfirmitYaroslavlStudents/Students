import {delay} from 'redux-saga';
import {takeEvery, takeLatest, all, put, call} from 'redux-saga/effects';
import {addCandidate, deleteCandidate, updateCandidate} from '../api/candidateService.js';
import {addComment, deleteComment} from '../api/commentService.js';
import {addCandidateSuccess, deleteCandidateSuccess, updateCandidateSuccess, addCommentSuccess, deleteCommentSuccess,
        setErrorMessage, search} from './actions';
import {createCandidate} from '../databaseDocumentClasses/index';

export default function* rootSaga() {
  yield all([
    watchCandidateAdd(),
    watchCandidateUpdate(),
    watchCandidateDelete(),
    watchCommentAdd(),
    watchCommentDelete(),
    watchChangeSearchRequest(),
    watchSearch()
  ])
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

function* watchChangeSearchRequest() {
  yield takeLatest('SET_SEARCH_REQUEST', setSearchRequestSaga);
}

function* watchSearch() {
  yield takeLatest('SEARCH', searchSaga);
}

function* addCandidateSaga(action) {
  try {
    let newCandidate = createCandidate(action.candidate.status, action.candidate);
    newCandidate.id = yield call(addCandidate, newCandidate);
    yield put(addCandidateSuccess(newCandidate));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add candidate error. Please, refresh the page.'));
  }
}

function* updateCandidateSaga(action) {
  try {
    let candidateNewState = createCandidate(action.candidate.status, action.candidate);
    yield call(updateCandidate, candidateNewState);
    yield put(updateCandidateSuccess(candidateNewState));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Update candidate error. Please, refresh the page.'));
  }
}

function* deleteCandidateSaga(action) {
  try {
    yield call(deleteCandidate, action.candidateID);
    yield put(deleteCandidateSuccess(action.candidateID));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete candidate error. Please, refresh the page.'));
  }
}

function* addCommentSaga(action) {
  try {
    yield call(addComment, action.candidateID, action.comment);
    yield put(addCommentSuccess(action.candidateID, action.comment));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add comment error. Please, refresh the page.'));
  }
}

function* deleteCommentSaga(action) {
  try {
    yield call(deleteComment, action.candidateID, action.comment);
    yield put(deleteCommentSuccess(action.candidateID, action.comment));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete comment error. Please, refresh the page.'));
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