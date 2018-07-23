import './css/index.css'
//import 'typeface-roboto'
import electron from 'electron'
import fs from 'fs'
import React from 'react'
import ReactDOM from 'react-dom'
import reducer from './reducer'
import { Provider } from 'react-redux'
import createMuiTheme from '@material-ui/core/styles/createMuiTheme'
import MuiThemeProvider from '@material-ui/core/styles/MuiThemeProvider'
import createPalette from '@material-ui/core/styles/createPalette'
import AppView from './layout/appview'
import createStore from './utilities/createStore'

const screenshotMetadataFileURL = electron.remote.getGlobal('screenshotMetadataFileURL')
const tests = JSON.parse(fs.readFileSync(screenshotMetadataFileURL))

const fallenTests = {}
let fallenTestIndex = 0
for (let i = 0; i < tests.length; i++) {
  if (!tests[i].passed) {
    tests[i].index = fallenTestIndex
    fallenTests[fallenTestIndex] = tests[i]
    fallenTestIndex++
  }
}

const store = createStore(reducer, { fallenTests, testTotalCount: tests.length })

const theme = createMuiTheme({
  palette: createPalette({
    primary: {
      main: '#29B6F6',
      contrastText: '#ffffff'
    },
    secondary: {
      main: '#ffffff',
      contrastText: '#444444'
    },
  }),
  typography: {
    fontFamily: ['Roboto', 'Helvetica', 'Arial', 'sans-serif'],
    fontSize: 16,
    fontWeightMedium: 400
  }
})

ReactDOM.render(
  <MuiThemeProvider theme={theme}>
    <Provider store={store}>
      <AppView />
    </Provider>
  </MuiThemeProvider>,
  document.getElementById('root')
)