import { createActions } from 'redux-actions';

const identity = x => x;

export const loadingActions = createActions({
  SHOW_LOADING: identity,
  HIDE_LOADING: identity
});

export const namesActions = createActions({
  ADD_NAME: identity,
  ADD_NAME_SUCCESS: identity,

  DELETE_NAME: identity,
  DELETE_NAME_SUCCESS: identity
});
