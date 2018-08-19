import { all, takeEvery, takeLatest, put, call, select } from 'redux-saga/effects'
import * as actions from './actions'
import * as applicationActions from '../applicationActions'
import { subscribe, unsubscribe } from '../api/subscribeService'
import { noticeNotification, deleteNotification, getNotifications } from '../api/notificationService'
import { SELECTORS } from '../rootReducer'

export default function* notificationsSaga() {
  yield all([
    watchGetNotifications(),
    watchSubscribe(),
    watchUnsubscribe(),
    watchNoticeNotification(),
    watchDeleteNotification()
  ])
}

function* watchGetNotifications() {
  yield takeLatest(actions.getNotifications, getNotificationsSaga)
}

function* watchSubscribe() {
  yield takeEvery(actions.subscribe, subscribeSaga)
}

function* watchNoticeNotification() {
  yield takeEvery(actions.noticeNotification, noticeNotificationSaga)
}

function* watchDeleteNotification() {
  yield takeEvery(actions.deleteNotification, deleteNotificationSaga)
}

function* watchUnsubscribe() {
  yield takeEvery(actions.unsubscribe, unsubscribeSaga)
}

function* getNotificationsSaga(action) {
  try {
    const { username } = action.payload
    const downloadedNotifications = yield call(getNotifications, username)
    yield put(actions.getNotificationsSuccess({ notifications: downloadedNotifications }))
  }
  catch (error) {
    yield put(applicationActions.setErrorMessage({message: error + '. Get actions error.'}))
  }
}

function* subscribeSaga(action) {
  try {
    const {candidateId} = action.payload
    const username = yield select(state => SELECTORS.AUTHORIZATION.USERNAME(state))
    yield call(subscribe, candidateId, username)
    yield put(actions.subscribeSuccess({ candidateId, username }))
  }
  catch (error) {
    yield put(applicationActions.setErrorMessage({message: error + '. Subsribe error.'}))
  }
}

function* unsubscribeSaga(action) {
  try {
    const {candidateId} = action.payload
    const username = yield select(state => SELECTORS.AUTHORIZATION.USERNAME(state))
    yield call(unsubscribe, candidateId, username)
    yield put(actions.unsubscribeSuccess({ candidateId, username }))
  }
  catch (error) {
    yield put(applicationActions.setErrorMessage({message: error + '. Unsubsribe error.'}))
  }
}

function* noticeNotificationSaga(action) {
  try {
    const {username, notificationId} = action.payload
    yield call(noticeNotification, username, notificationId)
    yield put(actions.noticeNotificationSuccess({notificationId}))
  }
  catch (error) {
    yield put(applicationActions.setErrorMessage({message: error + '. Notice notification error.'}))
  }
}

function* deleteNotificationSaga(action) {
  try {
    const {username, notificationId} = action.payload
    yield call(deleteNotification, username, notificationId)
    yield put(actions.deleteNotificationSuccess({notificationId}))
  }
  catch (error) {
    yield put(applicationActions.setErrorMessage({message: error + '. Delete notification error.'}))
  }
}