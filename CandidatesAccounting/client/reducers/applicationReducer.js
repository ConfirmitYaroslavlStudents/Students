import createReducer from './createReducer'
import A from '../actions'

const initialState = {
  initializing: true,
  fetching: false,
  pageTitle: 'Candidate Accounting',
  errorMessage: '',
  searchRequest: '',
}

export default createReducer(initialState, {
  [A.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState,
    searchRequest: payload.initialState.searchRequest ? payload.initialState.searchRequest : initialState.searchRequest
  }),

  [A.enableFetching]: state => ({
    ...state,
    fetching: true
  }),

  [A.setSearchRequest]: (state, {payload}) => ({
    ...state,
    searchRequest: payload.searchRequest
  }),

  [A.setErrorMessage]: (state, {payload}) => ({
    ...state,
    errorMessage: payload.message
  }),

  [A.setCandidateStatusSuccess]: (state, {payload}) => ({
    ...state,
    searchRequest: payload.status === state.candidateStatus ? '' : state.searchRequest
  }),

  [A.getCandidatesSuccess]: (state, {payload}) => ({
    ...state,
    pageTitle: 'Candidate Accounting',
    initializing: false,
    fetching: false
  }),

  [A.openCommentPageSuccess]: (state, {payload}) => ({
    ...state,
    pageTitle: payload.candidate.name,
    initializing: false,
    fetching: false,
  }),

  [A.loginFailure]: (state, {payload}) => ({
    ...state,
    errorMessage: payload.error + '. Login failure.'
  }),

  [A.logoutFailure]: (state, {payload}) => ({
    ...state,
    errorMessage: payload.error + '. Logout failure.'
  }),
})