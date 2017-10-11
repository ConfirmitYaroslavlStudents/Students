import { delay } from 'redux-saga'
import { put, takeEvery, all, call } from 'redux-saga/effects'
import fetcher from './fetcher.js';

// single entry point to start all Sagas at once
export default function* rootSaga() {
  yield all([
    watchCandidateAddition()
  ])
}

export function* addNewCandidate(action) {
  fetch("/candidates",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({candidate: action.candidate})
    })
    .then(function(response){ return response.json(); })
    .then(function(data){  })
}

export function* watchCandidateAddition() {
  yield takeEvery('ADD_CANDIDATE', addNewCandidate);
}