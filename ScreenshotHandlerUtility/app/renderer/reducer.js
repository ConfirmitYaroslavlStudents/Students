import createReducer from './utilities/createReducer'
import * as actions from './actions'

const initialState = {
  fallenTests: {},
  screenshotTotalAmount: 0,
  markedToUpdateAmount: 0,
  currentTestIndex: 0
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
          markedToUpdate: true,
          markedAsError: false
        }
      }
    }
  },

  [actions.markAsError]: (state, {payload}) => {
    return {
      ...state,
      fallenTests: {
        ...state.fallenTests,
        [payload.test.index]: {
          ...state.fallenTests[payload.test.index],
          markedToUpdate: false,
          markedAsError: true
        }
      }
    }
  },

  [actions.setCurrentTestIndex]: (state, {payload}) => {
    return {
      ...state,
      currentTestIndex: payload.index
    }
  },

  [actions.removeUnread]: (state, {payload}) => {
    return {
      ...state,
      fallenTests: {
        ...state.fallenTests,
        [payload.test.index]: {
          ...state.fallenTests[payload.test.index],
          unread: false
        }
      }
    }
  }
})