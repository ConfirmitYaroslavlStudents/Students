import { createAction } from 'redux-actions'


export const getTags = createAction('GET_TAGS')

export const getTagsSuccess = createAction('GET_TAGS_SUCCESS')

export const searchByTag = createAction('SEARCH_BY_TAG')