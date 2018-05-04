import createReducer from '../utilities/createReducer'
import * as application from '../applicationActions'
import * as candidates from './actions'
import * as comments from '../comments/actions'
import * as notifications from '../notifications/actions'

const initialState = {
  candidates: {},
  candidateStatus: '',
  offset: 0,
  candidatesPerPage: 15,
  totalCount: 0,
  sortingField: '',
  sortingDirection: 'desc',
  onUpdating: '',
  onDeleting: '',
  onResumeUploading: ''
}

export default createReducer(initialState, {
  [application.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState,
    candidateStatus: payload.initialState.candidateStatus ? payload.initialState.candidateStatus : initialState.candidateStatus,
    offset: payload.initialState.offset ? payload.initialState.offset : initialState.offset,
    candidatesPerPage: payload.initialState.candidatesPerPage ? payload.initialState.candidatesPerPage : initialState.candidatesPerPage,
    sortingField: payload.initialState.sortingField ? payload.initialState.sortingField : initialState.sortingField,
    sortingDirection: payload.initialState.sortingDirection ? payload.initialState.sortingDirection : initialState.sortingDirection
  }),

  [candidates.setCandidateStatusSuccess]: (state, {payload}) => ({
    ...state,
    candidateStatus: payload.status,
    offset: 0,
    sortingField: '',
    sortingDirection:'desc',
  }),

  [candidates.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    candidates: payload.candidates,
    totalCount: payload.totalCount,
    onUpdating: '',
    onDeleting: '',
  }),

  [candidates.addCandidateSuccess]: state => ({
    ...state,
    offset: state.totalCount - state.totalCount % state.candidatesPerPage
  }),

  [candidates.updateCandidateSuccess]: (state, {payload}) => {
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

  [candidates.deleteCandidateSuccess]: (state) => ({
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

  [candidates.setOffsetSuccess]: (state, {payload}) => ({
    ...state,
    offset: payload
  }),

  [candidates.setCandidatesPerPageSuccess]: (state, {payload}) => ({
    ...state,
    candidatesPerPage: payload
  }),

  [candidates.setSortingFieldSuccess]: (state, {payload}) => ({
    ...state,
    sortingField: payload,
    sortingDirection: 'desc'
  }),

  [candidates.toggleSortingDirectionSuccess]: (state) => ({
    ...state,
    sortingDirection: state.sortingDirection === 'desc' ? 'asc' : 'desc'
  }),

  [candidates.uploadResumeSuccess]: (state, {payload}) => ({
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

  [candidates.setOnResumeUploading]: (state, {payload}) => ({
    ...state,
    onResumeUploading: payload.intervieweeId
  }),

  [candidates.setOnUpdating]: (state, { payload }) => ({
    ...state,
    onUpdating: payload.candidateId
  }),

  [candidates.setOnDeleting]: (state, { payload }) => ({
    ...state,
    onDeleting: payload.candidateId
  }),

  [comments.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    candidates: { [payload.candidate.id]: payload.candidate },
    candidateStatus: payload.candidate.status,
    totalCount: 1,
  }),

  [comments.addCommentSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateId] : {
        ...state.candidates[payload.candidateId],
        commentAmount: state.candidates[payload.candidateId].commentAmount + 1
      }
    }
  }),

  [comments.deleteCommentSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateId]: {
        ...state.candidates[payload.candidateId],
        commentAmount: state.candidates[payload.candidateId].commentAmount - 1
      }
    }
  }),

  [notifications.subscribeSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateId]: {
        ...state.candidates[payload.candidateId],
        subscribers: {
          ...state.candidates[payload.candidateId].subscribers,
          [payload.username]: payload.username
        }
      }
    }
  }),

  [notifications.unsubscribeSuccess]: (state, {payload}) => {
    const subscribers = state.candidates[payload.candidateId].subscribers
    delete subscribers[payload.username]
    return {
      ...state,
      candidates: {
        ...state.candidates,
        [payload.candidateId]: {
          ...state.candidates[payload.candidateId],
          subscribers: subscribers
        }
    }
  }},
})

export const SELECTORS = {
  CANDIDATES: state => state.candidates,
  CANDIDATESTATUS: state => state.candidateStatus,
  OFFSET: state => state.offset,
  CANDIDATESPERPAGE: state => state.candidatesPerPage,
  TOTALCOUNT: state => state.totalCount,
  SORTINGFIELD: state => state.sortingField,
  SORTINGDIRECTION: state => state.sortingDirection,
  ONUPDATING: state => state.onUpdating,
  ONDELETING: state => state.onDeleting,
  ONRESUMEUPLOADING: state => state.onResumeUploading
}