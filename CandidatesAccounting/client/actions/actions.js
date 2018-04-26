import { createAction } from 'redux-actions'

/*_____APPLICATION__________________________________*/

export const init = createAction('INIT')

export const initialServerFetch = createAction('INITIAL_SERVER_FETCH')

export const getNotifications = createAction('GET_NOTIFICATIONS')

export const getNotificationsSuccess = createAction('GET_NOTIFICATIONS_SUCCESS')

export const getTags = createAction('GET_TAGS')

export const getTagsSuccess = createAction('GET_TAGS_SUCCESS')

export const setFetching = createAction('SET_FETCHING')

export const setState = createAction('SET_STATE')

export const setErrorMessage = createAction('SET_ERROR_MESSAGE')

export const setSearchRequest = createAction('SET_SEARCH_REQUEST')

export const search = createAction('SEARCH')

export const setCandidateStatus = createAction('SET_CANDIDATE_STATUS')

export const setCandidateStatusSuccess = createAction('SET_CANDIDATE_STATUS_SUCCESS')

export const openCommentPage = createAction('OPEN_COMMENT_PAGE')

export const openCommentPageSuccess = createAction('OPEN_COMMENT_PAGE_SUCCESS')

/*____AUTHORIZATION______________________________________*/

export const login = createAction('LOGIN')

export const loginSuccess = createAction('LOGIN_SUCCESS')

export const loginFailure = createAction('LOGIN_FAILURE')

export const logout = createAction('LOGOUT')

export const logoutSuccess = createAction('LOGOUT_SUCCESS')

export const logoutFailure = createAction('LOGOUT_FAILURE')

export const setAuthorizing = createAction('SET_AUTHORIZING')

/*_____CANDIDATES________________________________________*/

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

export const setOnUpdating = createAction('SET_ON_UPDATING')

export const setOnDeleting = createAction('SET_ON_DELETING')

export const setOnResumeUploading = createAction('SET_ON_RESUME_UPLOADING')

/*____RESUME___________________________________________*/

export const uploadResume = createAction('UPLOAD_RESUME')

export const uploadResumeSuccess = createAction('UPLOAD_RESUME_SUCCESS')

/*____COMMENTS_________________________________________*/

export const addComment = createAction('ADD_COMMENT')

export const addCommentSuccess = createAction('ADD_COMMENT_SUCCESS')

export const deleteComment = createAction('DELETE_COMMENT')

export const deleteCommentSuccess = createAction('DELETE_COMMENT_SUCCESS')

/*____NOTIFICATIONS___________________________________*/

export const subscribe = createAction('SUBSCRIBE')

export const subscribeSuccess = createAction('SUBSCRIBE_SUCCESS')

export const unsubscribe = createAction('UNSUBSCRIBE')

export const unsubscribeSuccess = createAction('UNSUBSCRIBE_SUCCESS')

export const noticeNotification = createAction('NOTICE_NOTIFICATION')

export const noticeNotificationSuccess = createAction('NOTICE_NOTIFICATION_SUCCESS')

export const deleteNotification = createAction('DELETE_NOTIFICATION')

export const deleteNotificationSuccess = createAction('DELETE_NOTIFICATION_SUCCESS')