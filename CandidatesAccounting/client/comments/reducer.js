import createReducer from '../utilities/createReducer'
import * as application from '../applicationActions'
import * as authorization from '../authorization/actions'
import * as comments from './actions'
import * as candidates from '../candidates/actions'

const initialState = {
  comments: {},
  currentCandidateId: '',
  lastDeletedComment: {}
}

export default createReducer(initialState, {
  [application.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState
  }),

  [comments.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    currentCandidateId: payload.candidate.id,
    comments: payload.comments,
    lastDeletedComment: {}
  }),

  [comments.addCommentSuccess]: (state, {payload}) => ({
    ...state,
    comments: {
      ...state.comments,
      [payload.comment.id]: payload.comment
    }
  }),

  [comments.deleteCommentSuccess]: (state, {payload}) => {
    let comments = { ...state.comments }
    const lastDeletedComment = comments[payload.commentId]
    delete comments[payload.commentId]
    return {
      ...state,
      comments: {
        ...comments
      },
      lastDeletedComment
    }
  },

  [comments.restoreCommentSuccess]: (state, {payload}) => ({
    ...state,
    comments: {
      ...state.comments,
      [payload.comment.id]: payload.comment
    },
    lastDeletedComment: {}
  }),

  [authorization.logoutSuccess]: state => ({
    ...state,
    ...initialState
  }),

  [candidates.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    currentCandidateId: '',
    comments: {}
  })
})

export const SELECTORS = {
  COMMENTS: state => state.comments,
  CURRENTCANDIDATEID: state => state.currentCandidateId,
  LASTDELETEDCOMMENT: state => state.lastDeletedComment
}