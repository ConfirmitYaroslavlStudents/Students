import { all, takeLatest, select, call, put } from 'redux-saga/effects'
import * as actions from './actions'
import fs from 'fs'
import deleteFile from './utilities/deleteFile'
import electron from 'electron'

function* commentsSaga() {
  yield all([
    watchCommit(),
    watchClose()
  ])
}

function* watchCommit() {
  yield takeLatest(actions.commit, commitSaga)
}

function* watchClose() {
  yield takeLatest(actions.close, closeSaga)
}

function* commitSaga() {
  try {
    const fallenTests = yield select(state => state.testGroup)

    for (const testIndex in fallenTests) {
      if (fallenTests.hasOwnProperty(testIndex)) {
        const test = fallenTests[testIndex]
        if (test.markedToUpdate) {
          yield call(deleteFile, test.baseScreenshotURL)
          yield call(deleteFile, test.diffScreenshotURL)
          yield call(fs.renameSync, test.newScreenshotURL, test.baseScreenshotURL)
        }
      }
    }
    yield put(actions.close())
  }
  catch (error) {
    console.log(error)
  }
}

function* closeSaga() {
  try {
    electron.remote.getCurrentWindow().close()
  }
  catch (error) {
    console.log(error)
  }
}

export default commentsSaga