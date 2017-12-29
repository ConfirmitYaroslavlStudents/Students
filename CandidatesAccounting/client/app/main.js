import '../css/commonStyles.css';
import '../css/selectizeStyles.css';
import React from 'react';
import ReactDOM from 'react-dom';
import {createStore, applyMiddleware} from 'redux';
import createSagaMiddleware from 'redux-saga';
import {Provider} from 'react-redux';
import reducer from '../redux/reducer';
import rootSaga from '../redux/sagas'
import {BrowserRouter, Route} from 'react-router-dom';
import createMuiTheme from 'material-ui/styles/createMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import createPalette from 'material-ui/styles/createPalette';
import {indigo} from 'material-ui/colors';
import AppView from '../components/layout/appview';
import {getInitialState} from '../api/commonService';

function configureStore(initialState) {
  const sagaMiddleware = createSagaMiddleware();
  const store = createStore(reducer, initialState, applyMiddleware(sagaMiddleware));
  let sagaRun = sagaMiddleware.run(function* () {
    yield rootSaga();
  });
  if (module.hot) {
    module.hot.accept('../redux/reducer', () => {
      const nextReducer = require('../redux/reducer').default;
      store.replaceReducer(nextReducer);
    });
    module.hot.accept('../redux/sagas', () => {
      const newRootSaga = require('../redux/sagas').default;
      sagaRun.cancel();
      sagaRun.done.then(() => {
        sagaRun = sagaMiddleware.run(function* replaceSaga() {
          yield newRootSaga();
        })
      })
    })
  }
  return store;
}

const store = configureStore(window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());

const theme = createMuiTheme({
  palette: createPalette({
    primary: indigo,
  })
});

function renderApp(app) {
  ReactDOM.render(
    <MuiThemeProvider theme={theme}>
      <Provider store={store}>
        <BrowserRouter>
          <Route path="/" component={app}/>
        </BrowserRouter>
      </Provider>
    </MuiThemeProvider>,
    document.getElementById('root')
  );
}

getInitialState()
  .then((result) => {
    store.dispatch({
        type: "SET_INITIAL_STATE",
        state: {
          userName: '',
          candidates: result.candidates,
          tags: result.tags,
          pageTitle: 'Candidate Accounting',
          searchRequest: '',
          errorMessage: ''
        }
      }
    );
    renderApp(AppView);
  });

if (module.hot) {
  module.hot.accept('../components/layout/appview', () => {
    const nextApp = require('../components/layout/appview');
    renderApp(nextApp);
  })
}