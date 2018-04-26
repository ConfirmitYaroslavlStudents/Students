import createReducer from './createReducer'
import * as A from '../actions/commentActions'

export default createReducer({}, {
  [A.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    comments: payload.comments,
    candidates: { [payload.candidate.id]: payload.candidate },
    candidateStatus: payload.candidate.status,
    pageTitle: payload.candidate.name,
    totalCount: 1,
    initializing: false,
    fetching: false,
  }),

  [A.addCommentSuccess]: (state, {payload}) => ({
    ...state,
    comments: {
      ...state.comments,
      [payload.comment.id]: payload.comment
    },
    candidates: {
      ...state.candidates,
      [payload.candidateId] : {
        ...state.candidates[payload.candidateId],
        commentAmount: state.candidates[payload.candidateId].commentAmount + 1
      }
    }
  }),

  [A.deleteCommentSuccess]: (state, {payload}) => {
    let comments = { ...state.comments }
    delete comments[payload.commentId]
    return {
      ...state,
      comments: {
        ...comments
      },
      candidates: {
        ...state.candidates,
        [payload.candidateId]: {
          ...state.candidates[payload.candidateId],
          commentAmount: state.candidates[payload.candidateId].commentAmount - 1
        }
      }
    }
  },
})