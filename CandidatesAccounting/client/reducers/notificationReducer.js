import createReducer from './createReducer'
import A from '../actions'

const initialState = {
  notifications: {}
}

export default createReducer(initialState, {
  [A.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState
  }),

  [A.getNotificationsSuccess]: (state, {payload}) => ({
    ...state,
    notifications: payload.notifications
  }),

  [A.noticeNotificationSuccess]: (state, {payload}) => ({
    ...state,
    notifications: {
      ...state.notifications,
      [payload.notificationId]: {
        ...state.notifications[payload.notificationId],
        recent: false
      }
    }
  }),

  [A.deleteNotificationSuccess]: (state, {payload}) => {
    let notifications = { ...state.notifications }
    delete notifications[payload.notificationId]
    return {
      ...state,
      notifications: notifications
    }
  },

  [A.loginSuccess]: (state, {payload}) => ({
    ...state,
    notifications: payload.notifications
  }),

  [A.loginFailure]: (state, {payload}) => ({
    ...state,
    notifications: {}
  }),

  [A.logoutSuccess]: state => ({
    ...state,
    notifications: {}
  }),
})

export const SELECTORS = {
  NOTIFICATIONS: state => state.notifications
}