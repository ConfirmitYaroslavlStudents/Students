import { createAction } from 'redux-actions'


export const init = createAction('INIT')

export const initSuccess = createAction('INIT_SUCCESS')

export const getInitialStateFromServer = createAction('GET_INITIAL_STATE_FROM_SERVER')

export const enableInitializing = createAction('ENADLE_INITIALIZING')

export const enableFetching = createAction('ENADLE_FETCHING')

export const disableFetching = createAction('DISABLE_FETCHING')

export const setErrorMessage = createAction('SET_ERROR_MESSAGE')

export const setSearchRequest = createAction('SET_SEARCH_REQUEST')

export const resetSearchRequest = createAction('RESET_SEARCH_REQUEST')

export const search = createAction('SEARCH')