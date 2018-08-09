import createSagaMiddleware from 'redux-saga';
import reducer from './reducers';
import saga from './sagas';
import { applyMiddleware, compose, createStore } from 'redux';

const sagaMiddleware = createSagaMiddleware();

const initialState = {};

const createStoreWithMiddleware = compose(
  applyMiddleware(sagaMiddleware)
)(createStore);

console.info(`${(new Date()).toLocaleString()} Creating Store`);
const store = createStoreWithMiddleware(reducer, initialState,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

sagaMiddleware.run(saga);

export default store;
