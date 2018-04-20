import createReducer from './createReducer'
import * as A from '../actions/actions'

const initialState = {
  authorized: false,
  authorizing: true,
  username: '',
  notifications: {},
  authorizationStatus: 'not-authorized',

  fetching: false,
  initializing: true,
  applicationStatus: 'loading',
  inactiveCandidateId: '',
  pageTitle: 'Candidate Accounting',
  errorMessage: '',
  searchRequest: '',

  candidateStatus: '',
  offset: 0,
  candidatesPerPage: 15,
  totalCount: 0,
  sortingField: '',
  sortingDirection: 'desc',

  candidates: {},

  tags: [],
}

export default createReducer(initialState, {
  [A.init]: (state, {payload}) => ({
    ...state,
    ...initialState,
    ...payload,
    authorized: payload.username !== '',
    authorizing: false
  }),

  [A.fetchInitialStateSuccess]: (state, {payload}) => ({
    ...state,
    ...payload,
    initializing: false
  }),

  [A.setState]: (state, {payload}) => ({
    ...state,
    ...payload
  }),

  [A.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    candidates: payload.candidates,
    totalCount: payload.totalCount,
    fetching: false,
  }),

  [A.getCandidateSuccess]: (state, {payload}) => ({
    ...state,
    candidates: { [payload.id]: payload },
    totalCount: 1,
    fetching: false,
  }),

  [A.setFetchStatus]: (state, {payload}) => ({
    ...state,
    fetching: payload
  }),

  [A.loginSuccess]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: true,
    username: payload.username,
    notifications: payload.notifications
  }),

  [A.logoutSuccess]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: '',
    notifications: {}
  }),

  [A.setAuthorizingStatus]: (state, {payload}) => ({
    ...state,
    authorizing: payload
  }),

  [A.changeTableOptionsSuccess]: (state, {payload}) => ({
    ...state,
    offset: payload.offset,
    candidatesPerPage: payload.candidatesPerPage,
    sortingField: payload.sortingField,
    sortingDirection: payload.sortingDirection,
  }),

  [A.setSearchRequest]: (state, {payload}) => ({
    ...state,
    searchRequest: payload
  }),

  [A.changeTableSuccess]: (state, {payload}) => ({
    ...state,
    searchRequest: payload === state.candidateStatus ? '' : state.searchRequest,
    candidateStatus: payload,
    offset: 0,
    sortingField: '',
    sortingDirection:'desc',
    pageTitle: 'Candidate Accounting'
  }),

  [A.setApplicationStatus]: (state, {payload}) => ({
    ...state,
    applicationStatus: payload
  }),

  [A.setErrorMessage]: (state, {payload}) => ({
    ...state,
    errorMessage: payload
  }),

  [A.setOffset]: (state, {payload}) => ({
    ...state,
    offset: payload
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