import { all, takeEvery, takeLatest, put, call, select } from 'redux-saga/effects'
import * as applicationActions from '../applicationActions'
import * as actions from './actions'
import { uploadResume } from '../api/resumeService'
import { getCandidates, addCandidate, deleteCandidate, updateCandidate } from '../api/candidateService.js'
import Comment from '../utilities/comment'

const creator = ({ history }) => {
  function* candidatesSaga() {
    yield all([
      watchGetCandidates(),
      watchCandidateAdd(),
      watchCandidateUpdate(),
      watchCandidateDelete(),
      watchSetCandidateStatus(),
      watchSetOffset(),
      watchSetCandidatePerPage(),
      watchSetSortingField(),
      watchToggleSortingDirection(),
      watchUploadResume()
    ])
  }

  function* watchGetCandidates() {
    yield takeLatest(actions.getCandidates, getCandidatesSaga)
  }

  function* watchCandidateAdd() {
    yield takeEvery(actions.addCandidate, addCandidateSaga)
  }

  function* watchCandidateUpdate() {
    yield takeEvery(actions.updateCandidate, updateCandidateSaga)
  }

  function* watchCandidateDelete() {
    yield takeEvery(actions.deleteCandidate, deleteCandidateSaga)
  }

  function* watchSetCandidateStatus() {
    yield takeLatest(actions.setCandidateStatus, setCandidateStatusSaga)
  }

  function* watchSetOffset() {
    yield takeLatest(actions.setOffset, setOffsetSaga)
  }

  function* watchSetCandidatePerPage() {
    yield takeLatest(actions.setCandidatesPerPage, setCandidatesPerPageSaga)
  }

  function* watchSetSortingField() {
    yield takeLatest(actions.setSortingField, setSortingFieldSaga)
  }

  function* watchToggleSortingDirection() {
    yield takeLatest(actions.toggleSortingDirection, toggleSortingDirectionSaga)
  }

  function* watchUploadResume() {
    yield takeEvery(actions.uploadResume, uploadResumeSaga)
  }

  function* getCandidatesSaga() {
    try {
      const candidateState = yield select(state => state.candidates)
      const searchRequest = yield select(state => state.application.searchRequest)
      const serverResponse = yield call(
        getCandidates,
        candidateState.candidatesPerPage,
        candidateState.offset,
        candidateState.candidateStatus,
        candidateState.sortingField,
        candidateState.sortingDirection,
        searchRequest)
      yield call(history.replace, '/'
        + (candidateState.candidateStatus === '' ? '' : candidateState.candidateStatus.toLowerCase() + 's')
        + '?take=' + candidateState.candidatesPerPage
        + (candidateState.offset === 0 ? '' : '&skip=' + candidateState.offset)
        + (candidateState.sortingField === '' ? '' : '&sort=' + candidateState.sortingField + '&sortDir=' + candidateState.sortingDirection)
        + (searchRequest === '' ? '' : '&q=' + encodeURIComponent(searchRequest)))
      yield put(actions.getCandidatesSuccess({
        candidates: serverResponse.candidates,
        totalCount: serverResponse.total
      }))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Get actions error.'}))
    }
  }

  function* addCandidateSaga(action) {
    try {
      const {candidate} = action.payload
      yield put(applicationActions.enableFetching())
      candidate.comments = {}
      candidate.comments['initialStatus'] = new Comment('SYSTEM', ' Initial status: ' + candidate.status)
      yield call(addCandidate, candidate)
      yield put(actions.addCandidateSuccess())
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Add candidate error.'}))
    }
  }

  function* updateCandidateSaga(action) {
    try {
      const {candidate, previousStatus} = action.payload
      const candidateStatus = yield select(state => state.candidates.candidateStatus)
      candidate.comments = {}
      if (candidate.status !== previousStatus) {
        candidate.comments['newStatus'] = new Comment('SYSTEM', ' New status: ' + candidate.status)
      }
      yield put(actions.setOnUpdating({candidateId: candidate.id}))
      yield call(updateCandidate, candidate)
      yield put(actions.updateCandidateSuccess({
        candidate,
        shouldMoveToAnotherTable: candidateStatus !== '' && candidateStatus !== candidate.status
      }))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Update candidate error.'}))
    }
  }

  function* deleteCandidateSaga(action) {
    try {
      const {candidateId} = action.payload
      yield put(actions.setOnDeleting({candidateId}))
      yield call(deleteCandidate, candidateId)
      yield put(actions.deleteCandidateSuccess())
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Delete candidate error.'}))
    }
  }

  function* setCandidateStatusSaga(action) {
    try {
      const { status } = action.payload
      const previousStatus = yield select(state => state.candidates.candidateStatus)
      yield put(applicationActions.enableFetching())
      if (status === previousStatus) {
        yield put(applicationActions.resetSearchRequest())
      }
      yield put(actions.setCandidateStatusSuccess({ status }))
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set candidate status error.'}))
    }
  }

  function* setOffsetSaga(action) {
    try {
      const {offset} = action.payload
      yield put(applicationActions.enableFetching())
      yield put(actions.setOffsetSuccess(offset))
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* setCandidatesPerPageSaga(action) {
    try {
      const {candidatesPerPage} = action.payload
      yield put(applicationActions.enableFetching())
      yield put(actions.setCandidatesPerPageSuccess(candidatesPerPage))
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set actions per page error.'}))
    }
  }

  function* setSortingFieldSaga(action) {
    try {
      const {sortingField} = action.payload
      yield put(applicationActions.enableFetching())
      yield put(actions.setSortingFieldSuccess(sortingField))
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* toggleSortingDirectionSaga() {
    try {
      yield put(applicationActions.enableFetching())
      yield put(actions.toggleSortingDirectionSuccess())
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Set offset error.'}))
    }
  }

  function* uploadResumeSaga(action) {
    try {
      const {intervieweeId, resume} = action.payload
      yield put(actions.setOnResumeUploading({intervieweeId}))
      yield call(uploadResume, intervieweeId, resume)
      yield put(actions.uploadResumeSuccess({intervieweeId, resume: resume.name}))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Upload resume error.'}))
    }
  }

  return candidatesSaga()
}

export default creator