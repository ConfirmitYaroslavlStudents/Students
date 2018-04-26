import { createAction } from 'redux-actions'


export const init = createAction('INIT')

export const initialServerFetch = createAction('INITIAL_SERVER_FETCH')

export const setFetching = createAction('SET_FETCHING')

export const setErrorMessage = createAction('SET_ERROR_MESSAGE')

export const setSearchRequest = createAction('SET_SEARCH_REQUEST')

export const search = createAction('SEARCH')