import { combineReducers } from 'redux';
import { handleActions } from 'redux-actions';

const isLoading = handleActions({
  SHOW_LOADING: () => true,
  HIDE_LOADING: () => false
}, false);

const dataInitState = {
  names: [],
  gridType: undefined
};

const data = handleActions({
  ADD_NAME_SUCCESS: (state, { payload: { names } }) =>
    Object.assign({}, state, { names }),

  DELETE_NAME_SUCCESS: (state, { payload: { names } }) =>
    Object.assign({}, state, { names }),

  ADD_GRID_TYPE_SUCCESS: (state, { payload: { type } }) =>
    Object.assign({}, state, { type })
}, dataInitState);

export default combineReducers({
  data,
  app: combineReducers({
    isLoading
  })
});
