import { combineReducers } from 'redux';
import { handleActions } from 'redux-actions';

const isLoading = handleActions({
  SHOW_LOADING: () => true,
  HIDE_LOADING: () => false
}, false);

const playerInitState = {
  names: [],
  count: 0
};

const players = handleActions({
  ADD_NAME: (state, { payload: { name } }) => {
    const { names } = state;
    names.push(name);
  },

  DELETE_NAME: (state, { payload: { name } }) => {
    const { names } = state;
    if (names.includes(name)) {
      names.splice(names.indexOf(name), 1);
    }
  }
}, playerInitState);

export default combineReducers({
  players,
  app: combineReducers({
    isLoading
  })
});
