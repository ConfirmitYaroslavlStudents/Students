import { all, takeEvery, takeLatest, put, call } from 'redux-saga/effects'
import * as actions from './actions'
import * as applicationActions from '../applicationActions'
import { addComment, deleteComment, addCommentAttachment } from '../api/commentService.js'
import { getCandidate } from '../api/candidateService.js'

const creator = ({ history }) => {
  function* commentsSaga() {
    yield all([
      watchOpenCommentPage(),
      watchCommentAdd(),
      watchCommentDelete(),
      watchCommentRestore()
    ])
  }

  function* watchOpenCommentPage() {
    yield takeLatest(actions.openCommentPage, openCommentPageSaga)
  }

  function* watchCommentAdd() {
    yield takeEvery(actions.addComment, addCommentSaga)
  }

  function* watchCommentDelete() {
    yield takeEvery(actions.deleteComment, deleteCommentSaga)
  }

  function* watchCommentRestore() {
    yield takeEvery(actions.restoreComment, restoreCommentSaga)
  }

  function* openCommentPageSaga(action) {
    try {
      const {candidate} = action.payload
      yield put(applicationActions.enableFetching())
      const getCandidateResult = yield call(getCandidate, candidate.id)
      yield call(history.replace, '/' + getCandidateResult.candidate.status.toLowerCase() + 's/' + getCandidateResult.candidate.id + '/comments')
      yield put(actions.openCommentPageSuccess({...getCandidateResult}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Open comment page error.'}))
    }
  }

  function* addCommentSaga(action) {
    try {
      const {candidateId, comment, commentAttachment} = action.payload
      comment.id = yield call(addComment, candidateId, comment)
      if (commentAttachment) {
        yield call(addCommentAttachment, candidateId, comment.id, commentAttachment)
      }
      yield put(actions.addCommentSuccess({candidateId, comment}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Add comment error.'}))
    }
  }

  function* deleteCommentSaga(action) {
    try {
      const {candidateId, commentId} = action.payload
      yield call(deleteComment, candidateId, commentId)
      yield put(actions.deleteCommentSuccess({candidateId, commentId}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Delete comment error.'}))
    }
  }

  function* restoreCommentSaga(action) {
    try {
      const {candidateId, comment, commentAttachment} = action.payload
      comment.id = yield call(addComment, candidateId, comment)
      if (commentAttachment) {
        yield call(addCommentAttachment, candidateId, comment.id, commentAttachment)
      }
      yield put(actions.restoreCommentSuccess({candidateId, comment}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Restore comment error.'}))
    }
  }

  return commentsSaga()
}

export default creator