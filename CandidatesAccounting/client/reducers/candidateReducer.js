import createReducer from './createReducer'
import * as A from '../actions/candidateActions'

export default createReducer({}, {
  [A.setCandidateStatusSuccess]: (state, {payload}) => ({
    ...state,
    searchRequest: payload.status === state.candidateStatus ? '' : state.searchRequest,
    candidateStatus: payload.status,
    offset: 0,
    sortingField: '',
    sortingDirection:'desc',
    pageTitle: 'Candidate Accounting'
  }),

  [A.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    comments: {},
    candidates: payload.candidates,
    totalCount: payload.totalCount,
    onUpdating: '',
    onDeleting: '',
    initializing: false,
    fetching: false
  }),

  [A.addCandidateSuccess]: state => ({
    ...state,
    offset: state.totalCount - state.totalCount % state.candidatesPerPage
  }),

  [A.updateCandidateSuccess]: (state, {payload}) => {
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

  [A.toggleSortingDirectionSuccess]: (state) => ({
    ...state,
    sortingDirection: state.sortingDirection === 'desc' ? 'asc' : 'desc'
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

  [A.setOnResumeUploading]: (state, {payload}) => ({
    ...state,
    onResumeUploading: payload.intervieweeId
  }),

  [A.setOnUpdating]: (state, { payload }) => ({
    ...state,
    onUpdating: payload.candidateId
  }),

  [A.setOnDeleting]: (state, { payload }) => ({
    ...state,
    onDeleting: payload.candidateId
  }),
})