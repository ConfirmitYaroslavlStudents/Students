import '../css/commonStyles.css';
import '../css/selectizeStyles.css';
import React from 'react';
import ReactDOM from 'react-dom';
import {createStore, applyMiddleware} from 'redux';
import createSagaMiddleware from 'redux-saga';
import {Provider} from 'react-redux';
import reducer from '../components/common/reducer';
import rootSaga from '../components/common/sagas'
import {BrowserRouter, Route} from 'react-router-dom';
import createMuiTheme from 'material-ui/styles/createMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import createPalette from 'material-ui/styles/createPalette';
import {indigo} from 'material-ui/colors';
import AppView from '../components/layout/appview';
import {getAllCandidates} from '../components/candidates/candidateService';
import {getTags} from '../components/tags/tagService';

const sagaMiddleware = createSagaMiddleware();

const store = createStore(
  reducer,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__(),
  applyMiddleware(sagaMiddleware)
);
sagaMiddleware.run(rootSaga);

const theme = createMuiTheme({
  palette: createPalette({
    primary: indigo,
  })
});

getAllCandidates()
  .then((candidates) => {
    getTags()
      .then((tags) => {
        store.dispatch({
            type: "SET_INITIAL_STATE",
            state: {
              userName: 'DmitryB',
              candidates: candidates,
              tags: tags,
              errorMessage: ''
            }
          }
        );

        ReactDOM.render(
          <MuiThemeProvider theme={theme}>
            <Provider store={store}>
              <BrowserRouter>
                <Route path="/" component={AppView}/>
              </BrowserRouter>
            </Provider>
          </MuiThemeProvider>,
          document.getElementById('root')
        );
      });
  });