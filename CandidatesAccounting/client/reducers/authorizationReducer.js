import createReducer from './createReducer'
import A from '../actions'

const initialState = {
  authorized: false,
  authorizing: true,
  username: '',
}

export default createReducer(initialState, {
  [A.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState,
    username: payload.initialState.username,
    authorized: payload.initialState.username !== '',
    authorizing: false
  }),

  [A.loginSuccess]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: true,
    username: payload.username
  }),

  [A.loginFailure]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: '',
  }),

  [A.logoutSuccess]: state => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: ''
  }),

  [A.logoutFailure]: (state, {payload}) => ({
    ...state,
    authorizing: false
  }),

  [A.enableAuthorizing]: state => ({
    ...state,
    authorizing: true
  }),
})