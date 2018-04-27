import '../css/commonStyles.css'
import '../css/selectizeStyles.css'
import 'typeface-roboto'
import 'react-quill/dist/quill.snow.css'
import React from 'react'
import ReactDOM from 'react-dom'
import reducer from '../reducers/rootReducer'
import { Provider } from 'react-redux'
import { Router } from 'react-router'
import { Route } from 'react-router-dom'
import createBrowserHistory from 'history/createBrowserHistory'
import createMuiTheme from 'material-ui/styles/createMuiTheme'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import createPalette from 'material-ui/styles/createPalette'
import { indigo } from 'material-ui/colors'
import AppView from '../components/layout/appview'
import configureStore from '../stores/createStore'
import { getInitialStateFromServer } from '../actions/applicationActions'

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

const store = configureStore(reducer, username, history)

/*________________________________________________________________________*/

renderApp(AppView)

store.dispatch(getInitialStateFromServer())

if (module.hot) {
  module.hot.accept('../components/layout/appview', () => {
    const nextApp = require('../components/layout/appview')
    renderApp(nextApp)
  })
}