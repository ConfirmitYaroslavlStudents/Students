import './css/index.css'
import electron from 'electron'
import React from 'react'
import ReactDOM from 'react-dom'
import reducer from './reducer'
import { Provider } from 'react-redux'
import { theme } from 'confirmit-themes'
import getFallenScreenshotMetadata from './utilities/getFallenScreenshotMetadata'
import AppView from './layout/appview'
import createStore from './utilities/createStore'

theme.useTheme(theme.themeNames.material)

const screenshotMetadataFileURL = electron.remote.getGlobal('screenshotMetadataFileURL')

const screenshotMetadata = getFallenScreenshotMetadata(screenshotMetadataFileURL)
const initialState = {
  fallenTests: screenshotMetadata.fallenScreenshots,
  screenshotTotalAmount: screenshotMetadata.screenshotTotalAmount
}

const store = createStore(reducer, initialState)

ReactDOM.render(
  <Provider store={store}>
    <AppView />
  </Provider>,
  document.getElementById('root')
)