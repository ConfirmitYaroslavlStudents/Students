import createReducer from './utilities/createReducer'
import * as actions from './actions'

const initialState = {
  fallenTests: {},
  testTotalCount: 0,
  markedToUpdateCount: 0
}

export default createReducer({}, {
  [actions.init]: (state, {payload}) => {
    return {
      ...state,
      ...initialState,
      ...payload.initialState
    }
  },

  [actions.markToUpdate]: (state, {payload}) => {
    return {
      ...state,
      fallenTests: {
        ...state.fallenTests,
        [payload.test.index]: {
          ...state.fallenTests[payload.test.index],
          markedToUpdate: true
        }
      },
      markedToUpdateCount: state.markedToUpdateCount + 1
    }
  },

  [actions.unmarkToUpdate]: (state, {payload}) => {
    return {
      ...state,
      fallenTests: {
        ...state.fallenTests,
        [payload.test.index]: {
          ...state.fallenTests[payload.test.index],
          markedToUpdate: false
        }
      },
      markedToUpdateCount: state.markedToUpdateCount - 1
    }
  }
})