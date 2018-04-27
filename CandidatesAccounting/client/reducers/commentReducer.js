import createReducer from './createReducer'
import A from '../actions'

const initialState = {
  comments: {}
}

export default createReducer(initialState, {
  [A.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState
  }),

  [A.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    comments: payload.comments
  }),

  [A.addCommentSuccess]: (state, {payload}) => ({
    ...state,
    comments: {
      ...state.comments,
      [payload.comment.id]: payload.comment
    }
  }),

  [A.deleteCommentSuccess]: (state, {payload}) => {
    let comments = { ...state.comments }
    delete comments[payload.commentId]
    return {
      ...state,
      comments: {
        ...comments
      }
    }
  },

  [A.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    comments: {}
  }),
})