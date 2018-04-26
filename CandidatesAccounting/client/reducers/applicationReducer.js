import createReducer from './createReducer'
import * as A from '../actions/applicationActions'

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
  [A.init]: (state, {payload}) => ({
    ...state,
    ...initialState,
    ...payload,
    authorized: payload.username !== '',
    authorizing: false
  }),

  [A.setFetching]: (state, {payload}) => ({
    ...state,
    fetching: payload.fetching
  }),

  [A.setSearchRequest]: (state, {payload}) => ({
    ...state,
    searchRequest: payload.searchRequest
  }),

  [A.setErrorMessage]: (state, {payload}) => ({
    ...state,
    errorMessage: payload.message
  }),
})