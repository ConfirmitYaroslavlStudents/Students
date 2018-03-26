import '../css/commonStyles.css';
import '../css/selectizeStyles.css';
import React from 'react';
import ReactDOM from 'react-dom';
import {
  createStore,
  applyMiddleware
} from 'redux';
import createSagaMiddleware from 'redux-saga';
import { Provider } from 'react-redux';
import reducer from '../redux/reducer';
import rootSaga from '../redux/sagas'
import {
  BrowserRouter,
  Route
} from 'react-router-dom';
import createMuiTheme from 'material-ui/styles/createMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import createPalette from 'material-ui/styles/createPalette';
import { indigo } from 'material-ui/colors';
import AppView from '../components/layout/appview';
import { getUsername } from '../api/authorizationService';
import { getInitialState } from '../api/commonService';
import Spinner from '../components/common/UIComponentDecorators/spinner';

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
          <Route path='/' component={app}/>
        </BrowserRouter>
      </Provider>
    </MuiThemeProvider>,
    document.getElementById('root')
  );
}

renderApp(() => {
  return (
    <div style={{textAlign: 'center', position: 'fixed', top: '20%', width: '100%'}}>
      <Spinner big/>
    </div>
  );
});

let candidateStatus = '';
let args = {};

let splitedURL = (window.location.pathname + window.location.search).split('?');
let path = splitedURL[0];
let splitedPath = path.split('/');
switch (splitedPath[1]) {
  case 'interviewees':
    candidateStatus = 'Interviewee';
    break;
  case 'students':
    candidateStatus = 'Student';
    break;
  case 'trainees':
    candidateStatus = 'Trainee';
    break;
}
let URLargs = splitedURL[1];
if (URLargs) {
  let argsArray = URLargs.split('&');
  argsArray.forEach((arg) => {
    let splited = arg.split('=');
    args[splited[0]] = splited[1];
  });
}

getUsername().then((username) => {
  getInitialState(username, args.take ? Number(args.take) : 15, args.skip ? Number(args.skip) : 0, candidateStatus,
    args.sort ? args.sort : '', args.sortDir ? args.sortDir : 'desc', args.q ? decodeURIComponent(args.q) : '')
    .then((initialState) => {
      store.dispatch({
          type: 'SET_STATE',
          state: {
            applicationStatus: 'ok',
            pageTitle: 'Candidate Accounting',
            errorMessage: '',
            username: username,
            searchRequest: args.q ? decodeURIComponent(args.q) : '',
            candidateStatus: candidateStatus,
            offset: args.skip ? Number(args.skip) : 0,
            candidatesPerPage: args.take ? Number(args.take) : 15,
            totalCount: initialState.total,
            sortingField: args.sort ? args.sort : '',
            sortingDirection: args.sortDir ? args.sortDir : 'desc',
            candidates: initialState.candidates,
            tags: initialState.tags,
            notifications: initialState.notifications
          }
        }
      );
      renderApp(AppView);
    });
  });

if (module.hot) {
  module.hot.accept('../components/layout/appview', () => {
    const nextApp = require('../components/layout/appview');
    renderApp(nextApp);
  })
}