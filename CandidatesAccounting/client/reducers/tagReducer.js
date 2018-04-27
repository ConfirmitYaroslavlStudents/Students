import createReducer from './createReducer'
import A from '../actions'

const initialState = {
  tags: []
}

export default createReducer(initialState, {
  [A.initSuccess]: (state, {payload}) => ({
    ...state,
    ...initialState
  }),

  [A.getTagsSuccess]: (state, {payload}) => ({
    ...state,
    tags: payload.tags
  }),
})