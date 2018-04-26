import createReducer from './createReducer'
import * as applicationActions from '../actions/applicationActions'
import * as authorizationActions from '../actions/authorizationActions'
import * as candidateActions from '../actions/candidateActions'
import * as commentActions from '../actions/commentActions'
import * as notificationActions from '../actions/notificationActions'
import * as resumeActions from '../actions/candidateActions'
import * as tagActions from '../actions/tagActions'

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

  [applicationActions.init]: (state, {payload}) => ({
    ...state,
    ...initialState,
    ...payload,
    authorized: payload.username !== '',
    authorizing: false
  }),

  [applicationActions.setFetching]: (state, {payload}) => ({
    ...state,
    fetching: payload.fetching
  }),

  [applicationActions.setSearchRequest]: (state, {payload}) => ({
    ...state,
    searchRequest: payload.searchRequest
  }),

  [applicationActions.setErrorMessage]: (state, {payload}) => ({
    ...state,
    errorMessage: payload.message
  }),

  /*____AUTHORIZATION______________________________________*/

  [authorizationActions.loginSuccess]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: true,
    username: payload.username,
    notifications: payload.notifications
  }),

  [authorizationActions.loginFailure]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: '',
    notifications: {},
    errorMessage: payload.error + '. Login failure.'
  }),

  [authorizationActions.logoutSuccess]: state => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: '',
    notifications: {}
  }),

  [authorizationActions.logoutFailure]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    errorMessage: payload.error + '. Logout failure.'
  }),

  [authorizationActions.setAuthorizing]: (state, {payload}) => ({
    ...state,
    authorizing: payload.authorizing
  }),

  /*_____CANDIDATES________________________________________*/

  [candidateActions.setCandidateStatusSuccess]: (state, {payload}) => ({
    ...state,
    searchRequest: payload.status === state.candidateStatus ? '' : state.searchRequest,
    candidateStatus: payload.status,
    offset: 0,
    sortingField: '',
    sortingDirection:'desc',
    pageTitle: 'Candidate Accounting'
  }),

  [candidateActions.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    comments: {},
    candidates: payload.candidates,
    totalCount: payload.totalCount,
    onUpdating: '',
    onDeleting: '',
    initializing: false,
    fetching: false
  }),

  [candidateActions.addCandidateSuccess]: state => ({
    ...state,
    offset: state.totalCount - state.totalCount % state.candidatesPerPage
  }),

  [candidateActions.updateCandidateSuccess]: (state, {payload}) => {
    if (payload.shouldMoveToAnotherTable) {
      let candidates = state.candidates
      delete candidates[payload.candidate.id]
      return {
        ...state,
        candidates: {
          ...candidates
        },
        onUpdating: ''
      }
    } else {
      return {
        ...state,
        candidates: {
          ...state.candidates,
          [payload.candidate.id] : {
            ...state.candidates[payload.candidate.id],
            ...payload.candidate
          }
        },
        onUpdating: ''
      }
    }
  },

  [candidateActions.deleteCandidateSuccess]: (state) => ({
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

  [candidateActions.setOnUpdating]: (state, { payload }) => ({
    ...state,
    onUpdating: payload.candidateId
  }),

  [candidateActions.setOnDeleting]: (state, { payload }) => ({
    ...state,
    onDeleting: payload.candidateId
  }),

  [candidateActions.setOffsetSuccess]: (state, {payload}) => ({
    ...state,
    offset: payload
  }),

  [candidateActions.setCandidatesPerPageSuccess]: (state, {payload}) => ({
    ...state,
    candidatesPerPage: payload
  }),

  [candidateActions.setSortingFieldSuccess]: (state, {payload}) => ({
    ...state,
    sortingField: payload,
    sortingDirection: 'desc'
  }),

  [candidateActions.toggleSortingDirectionSuccess]: (state) => ({
    ...state,
    sortingDirection: state.sortingDirection === 'desc' ? 'asc' : 'desc'
  }),

  /*____RESUME___________________________________________*/

  [resumeActions.setOnResumeUploading]: (state, {payload}) => ({
    ...state,
    onResumeUploading: payload.intervieweeId
  }),

  [resumeActions.uploadResumeSuccess]: (state, {payload}) => ({
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

  [commentActions.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    comments: payload.comments,
    candidates: { [payload.candidate.id]: payload.candidate },
    candidateStatus: payload.candidate.status,
    pageTitle: payload.candidate.name,
    totalCount: 1,
    initializing: false,
    fetching: false,
  }),

  [commentActions.addCommentSuccess]: (state, {payload}) => ({
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

  [commentActions.deleteCommentSuccess]: (state, {payload}) => {
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

  [notificationActions.getNotificationsSuccess]: (state, {payload}) => ({
    ...state,
    notifications: payload.notifications
  }),

  [notificationActions.subscribeSuccess]: (state, {payload}) => ({
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

  [notificationActions.unsubscribeSuccess]: (state, {payload}) => {
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

  [notificationActions.noticeNotificationSuccess]: (state, {payload}) => ({
    ...state,
    notifications: {
      ...state.notifications,
      [payload.notificationId]: {
        ...state.notifications[payload.notificationId],
        recent: false
      }
    }
  }),

  [notificationActions.deleteNotificationSuccess]: (state, {payload}) => {
    let notifications = { ...state.notifications }
    delete notifications[payload.notificationId]
    return {
      ...state,
      notifications: notifications
    }
  },

  /*___ TAGS _______________________________*/

  [tagActions.getTagsSuccess]: (state, {payload}) => ({
    ...state,
    tags: payload.tags
  }),
})