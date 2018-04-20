import { createAction } from 'redux-actions'

/*_____APPLICATION__________________________________*/

export const init = createAction('INIT')

export const fetchInitialState = createAction('FETCH_INITIAL_STATE')

export const fetchInitialStateSuccess = createAction('FETCH_INITIAL_STATE_SUCCESS')

export const setFetchStatus = createAction('SET_FETCH_STATUS')

export const setState = createAction('SET_STATE')

export const setApplicationStatus = createAction('SET_APPLICATION_STATUS')

export const setErrorMessage = createAction('SET_ERROR_MESSAGE')

export const setOffset = createAction('SET_OFFSET')

export const setSearchRequest = createAction('SET_SEARCH_REQUEST')

export const search = createAction('SEARCH')

export const changeTable = createAction('CHANGE_TABLE')

export const changeTableSuccess = createAction('CHANGE_TABLE_SUCCESS')

export const changeTableOptions = createAction('CHANGE_TABLE_OPTIONS')

export const changeTableOptionsSuccess = createAction('CHANGE_TABLE_OPTIONS_SUCCESS')

/*____AUTHORIZATION______________________________________*/

export const login = createAction('LOGIN')

export const loginSuccess = createAction('LOGIN_SUCCESS')

export const logout = createAction('LOGOUT')

export const logoutSuccess = createAction('LOGOUT_SUCCESS')

export const setAuthorizingStatus = createAction('SET_AUTHORIZING_STATUS')

/*_____CANDIDATES________________________________________*/

export const loadCandidates = createAction('LOAD_CANDIDATES')

export const getCandidates = createAction('GET_CANDIDATES')

export const getCandidatesSuccess = createAction('GET_CANDIDATES_SUCCESS')

export const getCandidate = createAction('GET_CANDIDATE')

export const getCandidateSuccess = createAction('GET_CANDIDATE_SUCCESS')

export const addCandidate = createAction('ADD_CANDIDATE')

export const deleteCandidate = createAction('DELETE_CANDIDATE')

export const updateCandidate = createAction('UPDATE_CANDIDATE')

export const updateCandidateSuccess = createAction('UPDATE_CANDIDATE_SUCCESS')

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