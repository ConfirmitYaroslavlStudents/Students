import { all, takeEvery, takeLatest, put, call, select } from 'redux-saga/effects'
import * as actions from './actions'
import * as applicationActions from '../applicationActions'
import { addComment, deleteComment, addCommentAttachment, getCommentAttachmentArrayBuffer } from '../api/commentService.js'
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
      const {candidateId, comment} = action.payload
      let commentAttachment = null

      if (comment.attachment && comment.attachment !== '') {
        const attachmentArrayBuffer = yield call(getCommentAttachmentArrayBuffer, candidateId, comment.id)
        commentAttachment = new File([attachmentArrayBuffer], comment.attachment)
      }

      yield call(deleteComment, candidateId, comment.id)
      yield put(actions.deleteCommentSuccess({candidateId, commentId: comment.id, commentAttachment}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Delete comment error.'}))
    }
  }

  function* restoreCommentSaga(action) {
    try {
      const {candidateId, comment } = action.payload
      comment.id = yield call(addComment, candidateId, comment)

      let commentAttachment = yield select(state => state.comments.lastDeletedCommentAttachment)
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