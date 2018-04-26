import createReducer from './createReducer'
import * as A from '../actions/authorizationActions'

export default createReducer({}, {
  [A.loginSuccess]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: true,
    username: payload.username,
    notifications: payload.notifications
  }),

  [A.loginFailure]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: '',
    notifications: {},
    errorMessage: payload.error + '. Login failure.'
  }),

  [A.logoutSuccess]: state => ({
    ...state,
    authorizing: false,
    authorized: false,
    username: '',
    notifications: {}
  }),

  [A.logoutFailure]: (state, {payload}) => ({
    ...state,
    authorizing: false,
    errorMessage: payload.error + '. Logout failure.'
  }),

  [A.setAuthorizing]: (state, {payload}) => ({
    ...state,
    authorizing: payload.authorizing
  }),
})