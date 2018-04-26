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

  comments: {},

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

  [A.getNotificationsSuccess]: (state, {payload}) => ({
    ...state,
    notifications: payload.notifications
  }),

  [A.getTagsSuccess]: (state, {payload}) => ({
    ...state,
    tags: payload.tags
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
    searchRequest: payload.status === state.candidateStatus ? '' : state.searchRequest,
    candidateStatus: payload.status,
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
    comments: {},
    candidates: payload.candidates,
    totalCount: payload.totalCount,
    onUpdating: '',
    onDeleting: '',
    initializing: false,
    fetching: false,
  }),

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

  /*____NOTIFICATIONS___________________________________*/

  [A.subscribeSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateId]: {
        ...state.candidates[payload.candidateId],
        subscribers: {
          ...state.candidates[payload.candidateId].subscribers,
          [payload.email]: payload.email
        }
      }
    }
  }),

  [A.unsubscribeSuccess]: (state, {payload}) => {
    let subscribers = { ...state.candidates[payload.candidateId].subscribers }
    delete subscribers[payload.email]
    return {
      ...state,
      candidates: {
        ...state.candidates,
        [payload.candidateId]: {
          ...state.candidates[payload.candidateId],
          subscribers: subscribers
        }
      }
    }
  },

  [A.noticeNotificationSuccess]: (state, {payload}) => ({
    ...state,
    notifications: {
      ...state.notifications,
      [payload.notificationId]: {
        ...state.notifications[payload.notificationId],
        recent: false
      }
    }
  }),

  [A.deleteNotificationSuccess]: (state, {payload}) => {
    let notifications = { ...state.notifications }
    delete notifications[payload.notificationId]
    return {
      ...state,
      notifications: notifications
    }
  },
})