import '../css/commonStyles.css'
import '../css/selectizeStyles.css'
import 'typeface-roboto'
import 'react-quill/dist/quill.snow.css'
import React from 'react'
import ReactDOM from 'react-dom'
import reducer from '../reducers/reducer'
import { Provider } from 'react-redux'
import { BrowserRouter, Route } from 'react-router-dom'
import createMuiTheme from 'material-ui/styles/createMuiTheme'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import createPalette from 'material-ui/styles/createPalette'
import { indigo } from 'material-ui/colors'
import AppView from '../components/layout/appview'
import { getInitialState } from '../api/commonService'
import getStateArgsFromURL from '../utilities/getStateArgsFromURL'
import configureStore from '../stores/createStore'
import { setState } from '../actions/actions'

const username = window['APP_CONFIG'].username

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

const store = configureStore(reducer, stateArgs)

/*________________________________________________________________________*/

renderApp(AppView)

const state = store.getState()

getInitialState(
  username,
  state.candidatesPerPage,
  state.offset,
  state.candidateStatus,
  state.sortingField,
  state.sortingDirection,
  state.searchRequest)
  .then(initialState => {
    store.dispatch(setState({
          applicationStatus: 'ok',
          totalCount: initialState.total,
          candidates: initialState.candidates,
          tags: initialState.tags,
          notifications: initialState.notifications
        }
      )
    )
  })

if (module.hot) {
  module.hot.accept('../components/layout/appview', () => {
    const nextApp = require('../components/layout/appview')
    renderApp(nextApp)
  })
}