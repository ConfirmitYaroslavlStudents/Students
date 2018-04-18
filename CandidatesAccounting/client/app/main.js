import '../css/commonStyles.css'
import '../css/selectizeStyles.css'
import 'typeface-roboto'
import 'react-quill/dist/quill.snow.css'
import React from 'react'
import ReactDOM from 'react-dom'
import { createStore, applyMiddleware, compose } from 'redux'
import createSagaMiddleware from 'redux-saga'
import { Provider } from 'react-redux'
import reducer from '../reducers/reducer'
import rootSaga from '../sagas/sagas'
import { BrowserRouter, Route } from 'react-router-dom'
import createMuiTheme from 'material-ui/styles/createMuiTheme'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import createPalette from 'material-ui/styles/createPalette'
import { indigo } from 'material-ui/colors'
import AppView from '../components/layout/appview'
import { getInitialState } from '../api/commonService'
import getStateArgsFromURL from '../utilities/getStateArgsFromURL'

const username = window['APP_CONFIG'].username

function configureStore(initialState) {
  const composeMiddlewares = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose
  const sagaMiddleware = createSagaMiddleware()
  let middlewares = applyMiddleware(sagaMiddleware)

  if (module.hot) {
    middlewares = composeMiddlewares(applyMiddleware(sagaMiddleware))
  }

  const store = createStore(reducer, initialState, middlewares)

  let sagaRun = sagaMiddleware.run(function* () {
    yield rootSaga()
  })

  if (module.hot) {
    module.hot.accept('../reducers/reducer', () => {
      const nextReducer = require('../reducers/reducer').default
      store.replaceReducer(nextReducer)
    })
    module.hot.accept('../sagas/sagas', () => {
      const newRootSaga = require('../sagas/sagas').default
      sagaRun.cancel()
      sagaRun.done.then(() => {
        sagaRun = sagaMiddleware.run(function* replaceSaga() {
          yield newRootSaga()
        })
      })
    })
  }
  return store
}

const theme = createMuiTheme({
  palette: createPalette({
    primary: indigo,
  }),
  typography: {
    fontWeightMedium: 400
  }
})

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
  )
}

const stateArgs = getStateArgsFromURL(window.location.pathname + window.location.search)

const store = configureStore({
  applicationStatus: 'loading',
  pageTitle: 'Candidate Accounting',
  errorMessage: '',
  searchRequest: stateArgs.searchRequest,

  authorizationStatus: username.trim() === '' ? 'not-authorized' : 'authorized',
  username,

  offset: stateArgs.offset,
  candidatesPerPage: stateArgs.candidatesPerPage,
  totalCount: 0,
  sortingField: stateArgs.sortingField,
  sortingDirection: stateArgs.sortingDirection,

  candidateStatus: stateArgs.tableType,
  candidates: {},

  tags: [],

  notifications: {}
})

/*________________________________________________________________________*/

renderApp(AppView)

getInitialState(
  username,
  stateArgs.candidatesPerPage,
  stateArgs.offset,
  stateArgs.tableType,
  stateArgs.sortingField,
  stateArgs.sortingDirection,
  stateArgs.searchRequest)
  .then((initialState) => {
    store.dispatch({
        type: 'SET_STATE',
        state: {
          applicationStatus: 'ok',
          totalCount: initialState.total,
          candidates: initialState.candidates,
          tags: initialState.tags,
          notifications: initialState.notifications
        }
      }
    )
  })

if (module.hot) {
  module.hot.accept('../components/layout/appview', () => {
    const nextApp = require('../components/layout/appview')
    renderApp(nextApp)
  })
}