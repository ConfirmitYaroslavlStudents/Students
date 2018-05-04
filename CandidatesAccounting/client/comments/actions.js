import { createAction } from 'redux-actions'


export const openCommentPage = createAction('OPEN_COMMENT_PAGE')

export const openCommentPageSuccess = createAction('OPEN_COMMENT_PAGE_SUCCESS')

export const addComment = createAction('ADD_COMMENT')

export const addCommentSuccess = createAction('ADD_COMMENT_SUCCESS')

export const deleteComment = createAction('DELETE_COMMENT')

export const deleteCommentSuccess = createAction('DELETE_COMMENT_SUCCESS')