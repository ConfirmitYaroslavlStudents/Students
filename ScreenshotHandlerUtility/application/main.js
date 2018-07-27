import './css/index.css'
import electron from 'electron'
import React from 'react'
import ReactDOM from 'react-dom'
import reducer from './reducer'
import { Provider } from 'react-redux'
import createMuiTheme from '@material-ui/core/styles/createMuiTheme'
import MuiThemeProvider from '@material-ui/core/styles/MuiThemeProvider'
import createPalette from '@material-ui/core/styles/createPalette'
import getScreenshotMetadata from './utilities/getScreenshotMetadata'
import AppView from './layout/appview'
import createStore from './utilities/createStore'

import {theme as ct} from 'confirmit-themes';
ct.useTheme(ct.themeNames.material);

const screenshotMetadataFileURL = electron.remote.getGlobal('screenshotMetadataFileURL')

const screenshotMetadata = getScreenshotMetadata(screenshotMetadataFileURL)
const initialState = {
  fallenTests: screenshotMetadata.fallenTests,
  testTotalAmount: screenshotMetadata.testTotalAmount
}

const store = createStore(reducer, initialState)

const theme = createMuiTheme({
  palette: createPalette({
    primary: {
      main: '#29B6F6',
      contrastText: '#fff'
    },
    secondary: {
      main: '#fff',
      contrastText: '#333'
    },
  }),
  typography: {
    fontFamily: ['Roboto', 'Helvetica', 'Arial', 'sans-serif'],
    fontSize: 16,
    fontWeightMedium: 500
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