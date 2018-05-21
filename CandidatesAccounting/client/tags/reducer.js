import createReducer from '../utilities/createReducer'
import * as application from '../applicationActions'
import * as tags from './actions'
import * as candidate from '../candidates/actions'
import mergeTags from '../utilities/mergeTags'

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
  }),

  [candidate.addCandidateSuccess]: (state, {payload}) => ({
    ...state,
    tags: mergeTags(state.tags, payload.candidate.tags)
  }),

  [candidate.updateCandidateSuccess]: (state, {payload}) => ({
    ...state,
    tags: mergeTags(state.tags, payload.candidate.tags)
  })
})

export const SELECTORS = {
  TAGS: state => state.tags
}