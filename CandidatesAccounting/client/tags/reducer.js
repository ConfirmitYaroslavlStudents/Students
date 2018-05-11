import createReducer from '../utilities/createReducer'
import * as application from '../applicationActions'
import * as tags from './actions'

const initialState = {
  tags: []
}

export default createReducer(initialState, {
  [application.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState
  }),

  [tags.getTagsSuccess]: (state, {payload}) => ({
    ...state,
    tags: payload.tags
  })
})

export const SELECTORS = {
  TAGS: state => state.tags
}