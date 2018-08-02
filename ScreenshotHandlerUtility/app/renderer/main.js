import './css/index.css'
import electron from 'electron'
import React from 'react'
import ReactDOM from 'react-dom'
import fs from 'fs'
import reducer from './reducer'
import { Provider } from 'react-redux'
import { theme } from 'confirmit-themes'
import parseScreenshotTestMetadata from './utilities/parseScreenshotTestMetadata'
import AppView from './layout/appview'
import createStore from './utilities/createStore'

theme.useTheme(theme.themeNames.material)

const screenshotTestMetadataURL = electron.remote.getGlobal('screenshotTestMetadataURL')

const screenshotTestMetadata = JSON.parse(fs.readFileSync(screenshotTestMetadataURL))

const parsedScreenshotTestMetadata = parseScreenshotTestMetadata(screenshotTestMetadata)

const initialState = {
  metadataURL: screenshotTestMetadataURL,
  metadata: screenshotTestMetadata,
  fallenTests: parsedScreenshotTestMetadata.fallenTests,
  testTotalAmount: parsedScreenshotTestMetadata.testTotalAmount
}

const store = createStore(reducer, initialState)

ReactDOM.render(
  <Provider store={store}>
    <AppView />
  </Provider>,
  document.getElementById('root')
)