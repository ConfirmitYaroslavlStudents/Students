import { handleActions } from 'redux-actions'
import * as application from '../applicationActions'
import * as authorization from './actions'

const initialState = {
  authorized: false,
  authorizing: true,
  username: ''
}

const reducer = handleActions(
  {
    [application.initSuccess]: (state, {payload}) => ({
      ...state,
      ...initialState,
      username: payload.initialState.username,
      authorized: payload.initialState.username !== '',
      authorizing: false
    }),

    [authorization.loginSuccess]: (state, {payload}) => ({
      ...state,
      authorizing: false,
      authorized: true,
      username: payload.username
    }),

    [authorization.loginFailure]: (state) => ({
      ...state,
      authorizing: false,
      authorized: false,
      username: '',
    }),

    [authorization.logoutSuccess]: state => ({
      ...state,
      authorizing: false,
      authorized: false,
      username: ''
    }),

    [authorization.logoutFailure]: (state) => ({
      ...state,
      authorizing: false
    }),

    [authorization.enableAuthorizing]: state => ({
      ...state,
      authorizing: true
    })
  },
  initialState
)

export const SELECTORS = {
  AUTHORIZED: state => state.authorized,
  AUTHORIZING: state => state.authorizing,
  USERNAME: state => state.username
}

export default reducer