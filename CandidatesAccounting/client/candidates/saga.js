import { all, takeEvery, takeLatest, put, call, select } from 'redux-saga/effects'
import * as applicationActions from '../applicationActions'
import * as actions from './actions'
import { uploadResume } from '../api/resumeService'
import { uploadAvatar } from '../api/avatarService'
import { getCandidates, addCandidate, deleteCandidate, updateCandidate } from '../api/candidateService.js'
import Comment from '../utilities/comment'
import findCandidateStateDifference from '../utilities/findCandidateStateDifference'
import createCandidateUpdateMessage from '../utilities/createCandidateUpdateMessage'
import createInterviewScheduledMessage from '../utilities/createInterviewScheduledMessage'
import { SELECTORS } from '../rootReducer'

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
      watchUploadResume(),
      watchUploadAvatar()
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

  function* watchUploadAvatar() {
    yield takeEvery(actions.uploadAvatar, uploadAvatarSaga)
  }


  function* getCandidatesSaga() {
    try {
      const candidatesPerPage = yield select(state => SELECTORS.CANDIDATES.CANDIDATESPERPAGE(state))
      const offset = yield select(state => SELECTORS.CANDIDATES.OFFSET(state))
      const candidateStatus = yield select(state => SELECTORS.CANDIDATES.CANDIDATESTATUS(state))
      const sortingField = yield select(state => SELECTORS.CANDIDATES.SORTINGFIELD(state))
      const sortingDirection = yield select(state => SELECTORS.CANDIDATES.SORTINGDIRECTION(state))
      const searchRequest = yield select(state => SELECTORS.APPLICATION.SEARCHREQUEST(state))
      const serverResponse = yield call(
        getCandidates,
        candidatesPerPage,
        offset,
        candidateStatus,
        sortingField,
        sortingDirection,
        searchRequest)
      yield call(history.replace, '/'
        + (candidateStatus === '' ? '' : candidateStatus.toLowerCase() + 's')
        + '?take=' + candidatesPerPage
        + (offset === 0 ? '' : '&skip=' + offset)
        + (sortingField === '' ? '' : '&sort=' + sortingField + '&sortDir=' + sortingDirection)
        + (searchRequest === '' ? '' : '&q=' + encodeURIComponent(searchRequest)))
      yield put(actions.getCandidatesSuccess({
        candidates: serverResponse.candidates,
        totalCount: serverResponse.total
      }))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Get candidate error.'}))
    }
  }

  function* addCandidateSaga(action) {
    try {
      const { candidate } = action.payload
      yield put(applicationActions.enableFetching())
      candidate.comments = {}
      candidate.comments['initialStatus'] = new Comment('SYSTEM', ' Initial status: ' + candidate.status)

      const resumeFile = candidate.resumeFile
      delete candidate.resumeFile
      const avatarFile = candidate.avatarFile
      delete candidate.avatarFile

      const candidateId = yield call(addCandidate, candidate)

      if (resumeFile) {
        yield put(actions.uploadResume({intervieweeId: candidateId, resume: resumeFile}))
      }

      if (avatarFile) {
        yield put(actions.uploadAvatar({candidateStatus: candidate.status, candidateId: candidateId, avatar: avatarFile}))
      }

      yield put(actions.addCandidateSuccess({ candidate }))
      yield put(actions.getCandidates())
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Add candidate error.'}))
    }
  }

  function* updateCandidateSaga(action) {
    try {
      const { candidate, previousState } = action.payload
      const candidateStatus = yield select(state => SELECTORS.CANDIDATES.CANDIDATESTATUS(state))
      const pageTitle = yield select(state => SELECTORS.APPLICATION.PAGETITLE)

      const resumeFile = candidate.resumeFile
      delete candidate.resumeFile
      const avatarFile = candidate.avatarFile
      delete candidate.avatarFile

      const candidateStateDifference = findCandidateStateDifference(previousState, candidate)
      candidate.comments = {}
      if (Object.keys(candidateStateDifference).length !== 0) {
        candidate.comments['candidateUpdateUpdateMessage'] = new Comment('SYSTEM', createCandidateUpdateMessage(candidateStateDifference))
        candidate.commentAmount++
        if (candidateStateDifference.interviewDate && candidateStateDifference.interviewDate.newState && candidateStateDifference.interviewDate.newState !== '') {
          candidate.comments['interviewSchduledMessage'] = new Comment('SYSTEM', createInterviewScheduledMessage(candidateStateDifference.interviewDate.newState))
          candidate.commentAmount++
        }
      }

      yield put(actions.setOnUpdating({candidateId: candidate.id}))

      yield call(updateCandidate, candidate)

      if (resumeFile) {
        yield put(actions.uploadResume({intervieweeId: candidate.id, resume: resumeFile}))
      }

      if (avatarFile) {
        yield put(actions.uploadAvatar({candidateStatus: candidate.status, candidateId: candidate.id, avatar: avatarFile}))
      }

      yield put(actions.updateCandidateSuccess({
        candidate,
        shouldMoveToAnotherTable: candidateStatus !== '' && candidateStatus !== candidate.status && pageTitle === 'Candidate Accounting'
      }))

      if (candidateStateDifference.status) {
        yield call(history.replace,
          history.location.pathname.replace(
            candidateStateDifference.status.previousState.toLowerCase() + 's',
            candidateStateDifference.status.newState.toLowerCase() + 's')
          + history.location.search)
        yield put(actions.setCandidateStatus({ status: candidateStateDifference.status.newState }))
      }
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
      const previousStatus = yield select(state => SELECTORS.CANDIDATES.CANDIDATESTATUS(state))
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

  function* uploadAvatarSaga(action) {
    try {
      const {candidateStatus, candidateId, avatar} = action.payload
      yield call(uploadAvatar, candidateStatus, candidateId, avatar)
      yield put(actions.uploadAvatarSuccess({ candidateId }))
    }
    catch (error) {
      yield put(applicationActions.setErrorMessage({message: error + '. Upload avatar error.'}))
    }
  }

  return candidatesSaga()
}

export default creator