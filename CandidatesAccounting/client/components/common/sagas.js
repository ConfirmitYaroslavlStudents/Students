import {delay} from 'redux-saga';
import {takeEvery, takeLatest, all, put, call} from 'redux-saga/effects';
import {addCandidate, deleteCandidate, editCandidate} from '../../api/candidateService.js';
import {addComment, deleteComment} from '../comments/commentService.js';
import {addCandidateSuccess, deleteCandidateSuccess, editCandidateSuccess, addCommentSuccess, deleteCommentSuccess,
        setErrorMessage, search} from './actions';

export default function* rootSaga() {
  yield all([
    watchCandidateAdd(),
    watchCandidateDelete(),
    watchCandidateEdit(),
    watchCommentAdd(),
    watchCommentDelete(),
    watchChangeSearchRequest(),
    watchSearch()
  ])
}

function* watchCandidateAdd() {
  yield takeEvery('ADD_CANDIDATE', addCandidateSaga);
}

function* watchCandidateDelete() {
  yield takeEvery('DELETE_CANDIDATE', deleteCandidateSaga);
}

function* watchCandidateEdit() {
  yield takeEvery('EDIT_CANDIDATE', editCandidateSaga);
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
    yield call(addCandidate, action.candidate);
    yield put(addCandidateSuccess(action.candidate));
  }
  catch (error) {
    yield put(setErrorMessage(error + '. Add candidate error. Please, refresh the page.'));
  }
}

function* deleteCandidateSaga(action) {
  try {
    yield call(deleteCandidate, action.id);
    yield put(deleteCandidateSuccess(action.id));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Delete candidate error. Please, refresh the page.'));
  }
}

function* editCandidateSaga(action) {
  try {
    yield call(editCandidate, action.id, action.candidateNewState);
    yield put(editCandidateSuccess(action.id, action.candidateNewState));
  }
  catch(error) {
    put(setErrorMessage(error + '. Edit candidate error. Please, refresh the page.'));
  }
}

function* addCommentSaga(action) {
  try {
    yield call(addComment, action.candidateId, action.comment);
    yield put(addCommentSuccess(action.candidateId, action.comment));
  }
  catch(error) {
    yield put(setErrorMessage(error + '. Add comment error. Please, refresh the page.'));
  }
}

function* deleteCommentSaga(action) {
  try {
    yield call(deleteComment, action.candidateId, action.commentId);
    yield put(deleteCommentSuccess(action.candidateId, action.commentId));
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