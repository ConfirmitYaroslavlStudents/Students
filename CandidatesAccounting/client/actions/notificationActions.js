import { createAction } from 'redux-actions'


export const getNotifications = createAction('GET_NOTIFICATIONS')

export const getNotificationsSuccess = createAction('GET_NOTIFICATIONS_SUCCESS')

export const subscribe = createAction('SUBSCRIBE')

export const subscribeSuccess = createAction('SUBSCRIBE_SUCCESS')

export const unsubscribe = createAction('UNSUBSCRIBE')

export const unsubscribeSuccess = createAction('UNSUBSCRIBE_SUCCESS')

export const noticeNotification = createAction('NOTICE_NOTIFICATION')

export const noticeNotificationSuccess = createAction('NOTICE_NOTIFICATION_SUCCESS')

export const deleteNotification = createAction('DELETE_NOTIFICATION')

export const deleteNotificationSuccess = createAction('DELETE_NOTIFICATION_SUCCESS')