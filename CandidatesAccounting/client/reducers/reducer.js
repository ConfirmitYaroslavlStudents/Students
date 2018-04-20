import createReducer from './createReducer'
import * as A from '../actions/actions'

const initialState = {
  applicationStatus: 'loading',
  pageTitle: 'Candidate Accounting',
  errorMessage: '',
  searchRequest: '',
  authorizationStatus: 'not-authorized',
  username: '',
  offset: 0,
  candidatesPerPage: 15,
  totalCount: 0,
  sortingField: '',
  sortingDirection: 'desc',
  candidateStatus: 'Candidate',
  candidates: {},
  tags: [],
  notifications: {}
}

export default createReducer(initialState, {
  [A.init]: (state, {payload}) => {
     return {
      ...state,
      ...initialState,
      ...payload
    }
  },

  [A.setState]: (state, {payload}) => ({
    ...state,
    ...payload
  }),

  [A.setApplicationStatus]: (state, {payload}) => {
    return {
    ...state,
    applicationStatus: payload
  }},

  [A.setErrorMessage]: (state, {payload}) => ({
    ...state,
    errorMessage: payload
  }),

  [A.updateCandidateSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidate.id] : {
        ...state.candidates[payload.candidate.id],
        ...payload.candidate
      }
    }
  }),

  [A.addCommentSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateID] : {
        ...state.candidates[payload.candidateID],
        comments: {
          ...state.candidates[payload.candidateID].comments,
          [payload.comment.id]: payload.comment,
        }
      }
    }
  }),

  [A.deleteCommentSuccess]: (state, {payload}) => {
    let comments = { ...state.candidates[payload.candidateID].comments }
    delete comments[payload.commentID]
    return {
      ...state,
      candidates: {
        ...state.candidates,
        [payload.candidateID]: {
          ...state.candidates[payload.candidateID],
          comments: comments
        }
      }
    }
  },

  [A.subscribeSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateID]: {
        ...state.candidates[payload.candidateID],
        subscribers: {
          ...state.candidates[payload.candidateID].subscribers,
          [payload.email]: payload.email
        }
      }
    }
  }),

  [A.unsubscribeSuccess]: (state, {payload}) => {
    let subscribers = { ...state.candidates[payload.candidateID].subscribers }
    delete subscribers[payload.email]
    return {
      ...state,
      candidates: {
        ...state.candidates,
        [payload.candidateID]: {
          ...state.candidates[payload.candidateID],
          subscribers: subscribers
        }
      }
    }
  },

  [A.noticeNotificationSuccess]: (state, {payload}) => ({
    ...state,
    notifications: {
      ...state.notifications,
      [payload.notificationID]: {
        ...state.notifications[payload.notificationID],
        recent: false
      }
    }
  }),

  [A.deleteNotificationSuccess]: (state, {payload}) => {
    let notifications = { ...state.notifications }
    delete notifications[payload.notificationID]
    return {
      ...state,
      notifications: notifications
    }
  },

  [A.uploadResumeSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.intervieweeID]: {
        ...state.candidates[payload.intervieweeID],
        resume: payload.resume
      }
    }
  }),
})