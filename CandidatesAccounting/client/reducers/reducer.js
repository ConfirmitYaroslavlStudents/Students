import createReducer from './createReducer'
import * as A from '../actions/actions'

const initialState = {
  authorized: false,
  authorizing: true,
  username: '',
  notifications: {},

  initializing: true,
  fetching: false,
  pageTitle: 'Candidate Accounting',
  errorMessage: '',
  searchRequest: '',

  candidateStatus: '',
  offset: 0,
  candidatesPerPage: 15,
  totalCount: 0,
  sortingField: '',
  sortingDirection: 'desc',
  onUpdating: '',
  onDeleting: '',
  onResumeUploading: '',

  candidates: {},

  tags: [],
}

export default createReducer(initialState, {

  /*_____APPLICATION__________________________________*/

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

  [A.setFetching]: (state, {payload}) => ({
    ...state,
    fetching: payload.fetching
  }),

  [A.setSearchRequest]: (state, {payload}) => ({
    ...state,
    searchRequest: payload
  }),

  [A.setCandidateStatusSuccess]: (state, {payload}) => ({
    ...state,
    searchRequest: payload === state.candidateStatus ? '' : state.searchRequest,
    candidateStatus: payload,
    offset: 0,
    sortingField: '',
    sortingDirection:'desc',
    pageTitle: 'Candidate Accounting'
  }),

  [A.setErrorMessage]: (state, {payload}) => ({
    ...state,
    errorMessage: payload.message
  }),

  /*____AUTHORIZATION______________________________________*/

  [A.loginSuccess]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: true,
    username: payload.username,
    notifications: payload.notifications
  }),

  [A.logoutSuccess]: state => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: '',
    notifications: {}
  }),

  [A.setAuthorizing]: (state, {payload}) => ({
    ...state,
    authorizing: payload.authorizing
  }),

  /*_____CANDIDATES________________________________________*/

  [A.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    candidates: payload.candidates,
    totalCount: payload.totalCount,
    fetching: false,
    onUpdating: '',
    onDeleting: '',
  }),

  [A.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    candidates: { [payload.candidate.id]: payload.candidate },
    candidateStatus: payload.candidate.status,
    pageTitle: payload.candidate.name,
    totalCount: 1,
    fetching: false,
  }),

  [A.addCandidateSuccess]: state => ({
    ...state,
    offset: state.totalCount - state.totalCount % state.candidatesPerPage
  }),

  [A.updateCandidateSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidate.id] : {
        ...state.candidates[payload.candidate.id],
        ...payload.candidate
      }
    },
    onUpdating: ''
  }),

  [A.deleteCandidateSuccess]: (state) => ({
    ...state,
    offset:
      state.totalCount - state.offset - 1 === 0 ?
        state.offset > state.candidatesPerPage ?
          state.offset - state.candidatesPerPage
          :
          0
        :
        state.offset
  }),

  [A.setOnUpdating]: (state, { payload }) => ({
    ...state,
    onUpdating: payload.candidateId
  }),

  [A.setOnDeleting]: (state, { payload }) => ({
    ...state,
    onDeleting: payload.candidateId
  }),

  [A.setOffsetSuccess]: (state, {payload}) => ({
    ...state,
    offset: payload
  }),

  [A.setCandidatesPerPageSuccess]: (state, {payload}) => ({
    ...state,
    candidatesPerPage: payload
  }),

  [A.setSortingFieldSuccess]: (state, {payload}) => ({
    ...state,
    sortingField: payload,
    sortingDirection: 'desc'
  }),

  [A.setSortingDirectionSuccess]: (state) => ({
    ...state,
    sortingDirection: state.sortingDirection === 'desc' ? 'asc' : 'desc'
  }),

  /*____RESUME___________________________________________*/

  [A.setOnResumeUploading]: (state, {payload}) => ({
    ...state,
    onResumeUploading: payload.intervieweeId
  }),

  [A.uploadResumeSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.intervieweeId]: {
        ...state.candidates[payload.intervieweeId],
        resume: payload.resume
      }
    },
    onResumeUploading: ''
  }),

  /*____COMMENTS_________________________________________*/

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

  /*____NOTIFICATIONS___________________________________*/

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
})