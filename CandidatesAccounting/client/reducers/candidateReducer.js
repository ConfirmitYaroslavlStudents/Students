import createReducer from './createReducer'
import A from '../actions'

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
  [A.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState,
    candidateStatus: payload.initialState.candidateStatus ? payload.initialState.candidateStatus : initialState.candidateStatus,
    offset: payload.initialState.offset ? payload.initialState.offset : initialState.offset,
    candidatesPerPage: payload.initialState.candidatesPerPage ? payload.initialState.candidatesPerPage : initialState.candidatesPerPage,
    sortingField: payload.initialState.sortingField ? payload.initialState.sortingField : initialState.sortingField,
    sortingDirection: payload.initialState.sortingDirection ? payload.initialState.sortingDirection : initialState.sortingDirection
  }),

  [A.setCandidateStatusSuccess]: (state, {payload}) => ({
    ...state,
    candidateStatus: payload.status,
    offset: 0,
    sortingField: '',
    sortingDirection:'desc',
  }),

  [A.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    candidates: payload.candidates,
    totalCount: payload.totalCount,
    onUpdating: '',
    onDeleting: '',
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

  [A.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    candidates: { [payload.candidate.id]: payload.candidate },
    candidateStatus: payload.candidate.status,
    totalCount: 1,
  }),

  [A.addCommentSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateId] : {
        ...state.candidates[payload.candidateId],
        commentAmount: state.candidates[payload.candidateId].commentAmount + 1
      }
    }
  }),

  [A.deleteCommentSuccess]: (state, {payload}) => ({
    candidates: {
      ...state.candidates,
      [payload.candidateId]: {
        ...state.candidates[payload.candidateId],
        commentAmount: state.candidates[payload.candidateId].commentAmount - 1
      }
    }
  }),

  [A.subscribeSuccess]: (state, {payload}) => ({
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

  [A.unsubscribeSuccess]: (state, {payload}) => {
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