import { handleActions } from 'redux-actions'
import * as application from '../applicationActions'
import * as tags from './actions'
import * as candidate from '../candidates/actions'
import mergeTags from '../utilities/mergeTags'

const initialState = {
  tags: []
}

const reducer = handleActions(
  {
    [application.initSuccess]: (state) => ({
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
  },
  initialState
)

export const SELECTORS = {
  TAGS: state => state.tags
}

export default reducer