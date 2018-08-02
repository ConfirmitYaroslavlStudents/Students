import { all, takeLatest, select, call, put } from 'redux-saga/effects'
import * as actions from './actions'
import electron from 'electron'
import fs from 'fs'
import deleteFile from './utilities/deleteFile'
import updateMetadata from './utilities/updateMetadata'

export default function* commentsSaga() {
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
    const fallenTests = yield select(state => state.fallenTests)
    const metadata = yield select(state => state.metadata)

    for (const testIndex in fallenTests) {
      if (fallenTests.hasOwnProperty(testIndex)) {
        const test = fallenTests[testIndex]
        if (test.markedToUpdate) {
          yield call(deleteFile, test.baseScreenshotURL)
          yield call(deleteFile, test.diffScreenshotURL)
          yield call(fs.renameSync, test.newScreenshotURL, test.baseScreenshotURL)
          yield call(updateMetadata, metadata, test)
        }
      }
    }
    const metadataURL = yield select(state => state.metadataURL)
    yield call(fs.writeFileSync, metadataURL, JSON.stringify(metadata))

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