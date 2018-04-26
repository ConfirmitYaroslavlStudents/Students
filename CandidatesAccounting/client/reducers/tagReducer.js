import createReducer from './createReducer'
import * as A from '../actions/tagActions'

export default createReducer({}, {
  [A.getTagsSuccess]: (state, {payload}) => ({
    ...state,
    tags: payload.tags
  }),
})