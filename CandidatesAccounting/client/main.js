import './css/index.css'
import 'typeface-roboto'
import React from 'react'
import ReactDOM from 'react-dom'
import reducer from './rootReducer'
import { Provider } from 'react-redux'
import Router from 'react-router/Router'
import Route from 'react-router/Route'
import createBrowserHistory from 'history/createBrowserHistory'
import createMuiTheme from '@material-ui/core/styles/createMuiTheme'
import MuiThemeProvider from '@material-ui/core/styles/MuiThemeProvider'
import createPalette from '@material-ui/core/styles/createPalette'
import indigo from '@material-ui/core/colors/indigo'
import deepOrange from '@material-ui/core/colors/deepOrange'
import AppView from './layout/appview'
import createStore from './utilities/createStore'
import { getInitialStateFromServer } from './applicationActions'

const username = window['APP_CONFIG'].username

const theme = createMuiTheme({
  palette: createPalette({
    primary: indigo,
    secondary: deepOrange
  }),
  typography: {
    fontFamily: ['Roboto', 'Helvetica', 'Arial', 'sans-serif'],
    fontSize: 16,
    fontWeightMedium: 400
  }
})

const history = createBrowserHistory()

const store = createStore(reducer, username, history)

function renderApp(app) {
  ReactDOM.render(
    <MuiThemeProvider theme={theme}>
      <Provider store={store}>
        <Router history={history}>
          <Route path='/' component={app}/>
        </Router>
      </Provider>
    </MuiThemeProvider>,
    document.getElementById('root')
  )
}

/*________________________________________________________________________*/

renderApp(AppView)

if (username !== '') {
  store.dispatch(getInitialStateFromServer())
}

if (module.hot) {
  module.hot.accept('./layout/appview', () => {
    const nextApp = require('./layout/appview').default
    renderApp(nextApp)
  })
}