import { createAction } from 'redux-actions'


export const setCandidateStatus = createAction('SET_CANDIDATE_STATUS')

export const setCandidateStatusSuccess = createAction('SET_CANDIDATE_STATUS_SUCCESS')

export const getCandidates = createAction('GET_CANDIDATES')

export const getCandidatesSuccess = createAction('GET_CANDIDATES_SUCCESS')

export const addCandidate = createAction('ADD_CANDIDATE')

export const addCandidateSuccess = createAction('ADD_CANDIDATE_SUCCESS')

export const deleteCandidate = createAction('DELETE_CANDIDATE')

export const deleteCandidateSuccess = createAction('DELETE_CANDIDATE_SUCCESS')

export const updateCandidate = createAction('UPDATE_CANDIDATE')

export const updateCandidateSuccess = createAction('UPDATE_CANDIDATE_SUCCESS')

export const setOffset = createAction('SET_OFFSET')

export const setOffsetSuccess = createAction('SET_OFFSET_SUCCESS')

export const setCandidatesPerPage = createAction('SET_CANDIDATES_PER_PAGE')

export const setCandidatesPerPageSuccess = createAction('SET_CANDIDATES_PER_PAGE_SUCCESS')

export const setSortingField = createAction('SET_SORTING_FIELD')

export const setSortingFieldSuccess = createAction('SET_SORTING_FIELD_SUCCESS')

export const toggleSortingDirection = createAction('TOGGLE_SORTING_DIRECTION')

export const toggleSortingDirectionSuccess = createAction('TOGGLE_SORTING_DIRECTION_SUCCESS')

export const uploadResume = createAction('UPLOAD_RESUME')

export const uploadResumeSuccess = createAction('UPLOAD_RESUME_SUCCESS')

export const uploadAvatar = createAction('UPLOAD_AVATAR')

export const setOnResumeUploading = createAction('SET_ON_RESUME_UPLOADING')

export const setOnUpdating = createAction('SET_ON_UPDATING')

export const setOnDeleting = createAction('SET_ON_DELETING')