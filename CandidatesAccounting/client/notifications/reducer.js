import createReducer from '../utilities/createReducer'
import * as application from '../applicationActions'
import * as notifications from './actions'
import * as authorization from '../authorization/actions'

const initialState = {
  notifications: {}
}

export default createReducer(initialState, {
  [application.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState
  }),

  [notifications.getNotificationsSuccess]: (state, {payload}) => ({
    ...state,
    notifications: payload.notifications
  }),

  [notifications.noticeNotificationSuccess]: (state, {payload}) => ({
    ...state,
    notifications: {
      ...state.notifications,
      [payload.notificationId]: {
        ...state.notifications[payload.notificationId],
        recent: false
      }
    }
  }),

  [notifications.deleteNotificationSuccess]: (state, {payload}) => {
    let notifications = { ...state.notifications }
    delete notifications[payload.notificationId]
    return {
      ...state,
      notifications: notifications
    }
  },

  [authorization.loginSuccess]: (state, {payload}) => ({
    ...state,
    notifications: payload.notifications
  }),

  [authorization.loginFailure]: (state, {payload}) => ({
    ...state,
    notifications: {}
  }),

  [authorization.logoutSuccess]: state => ({
    ...state,
    notifications: {}
  })
})

export const SELECTORS = {
  NOTIFICATIONS: state => state.notifications
}