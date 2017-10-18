import { takeEvery, all} from 'redux-saga/effects';
import { addCandidate, deleteCandidate, editCandidate, addComment, deleteComment } from './fetcher.js';

export default function* rootSaga() {
  yield all([
    watchCandidateAdd(),
    watchCandidateDelete(),
    watchCandidateEdit(),
    watchCommentAdd(),
    watchCommentDelete()
  ])
}

export function* watchCandidateAdd() {
  yield takeEvery('ADD_CANDIDATE', addCandidateSaga);
}

export function* watchCandidateDelete() {
  yield takeEvery('DELETE_CANDIDATE', deleteCandidateSaga);
}

export function* watchCandidateEdit() {
  yield takeEvery('EDIT_CANDIDATE', editCandidateSaga);
}

export function* watchCommentAdd() {
  yield takeEvery('ADD_COMMENT', addCommentSaga);
}

export function* watchCommentDelete() {
  yield takeEvery('DELETE_COMMENT', deleteCommentSaga);
}

export function* addCandidateSaga(action) {
  addCandidate(action.candidate);
}

export function* deleteCandidateSaga(action) {
  deleteCandidate(action.id);
}

export function* editCandidateSaga(action) {
  editCandidate(action.id, action.candidateNewState);
}

export function* addCommentSaga(action) {
  addComment(action.candidateId, action.comment);
}

export function* deleteCommentSaga(action) {
  deleteComment(action.candidateId, action.commentId);
}





