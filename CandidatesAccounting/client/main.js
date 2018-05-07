import './css/index.css'
import 'typeface-roboto'
import React from 'react'
import ReactDOM from 'react-dom'
import reducer from './rootReducer'
import { Provider } from 'react-redux'
import { Router } from 'react-router'
import { Route } from 'react-router-dom'
import createBrowserHistory from 'history/createBrowserHistory'
import createMuiTheme from 'material-ui/styles/createMuiTheme'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import createPalette from 'material-ui/styles/createPalette'
import { indigo } from 'material-ui/colors'
import AppView from './layout/appview'
import createStore from './utilities/createStore'
import { getInitialStateFromServer } from './applicationActions'

const username = window['APP_CONFIG'].username

const theme = createMuiTheme({
  palette: createPalette({
    primary: indigo,
  }),
  typography: {
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

store.dispatch(getInitialStateFromServer())

if (module.hot) {
  module.hot.accept('./layout/appview', () => {
    const nextApp = require('./layout/appview')
    renderApp(nextApp)
  })
}