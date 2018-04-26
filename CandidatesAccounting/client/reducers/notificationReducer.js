import createReducer from './createReducer'
import * as A from '../actions/notificationActions'

export default createReducer({}, {
  [A.getNotificationsSuccess]: (state, {payload}) => ({
    ...state,
    notifications: payload.notifications
  }),

  [A.subscribeSuccess]: (state, {payload}) => ({
    ...state,
    candidates: {
      ...state.candidates,
      [payload.candidateId]: {
        ...state.candidates[payload.candidateId],
        subscribers: {
          ...state.candidates[payload.candidateId].subscribers,
          [payload.email]: payload.email
        }
      }
    }
  }),

  [A.unsubscribeSuccess]: (state, {payload}) => {
    let subscribers = { ...state.candidates[payload.candidateId].subscribers }
    delete subscribers[payload.email]
    return {
      ...state,
      candidates: {
        ...state.candidates,
        [payload.candidateId]: {
          ...state.candidates[payload.candidateId],
          subscribers: subscribers
        }
      }
    }
  },

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
})