import path from 'path'
import defaultOptions from './default.config.js'
import ScreenshotHandler from './screenshotHandler'

export const comparisonIgnoreRules = {
  nothing: 'nothing',
  antialiasing: 'antialiasing',
  less: 'less',
  colors: 'colors',
  alpha: 'alpha'
}

export const differenceDisplayMode = {
  flat: 'flat',
  movement: 'movement',
  flatWithDifferenceIntensity: 'flatDifferenceIntensity',
  movementWithDifferenceIntensity: 'movementDifferenceIntensity',
  differenceOnly: 'diffOnly'
}

export const fallenTestSaveStrategies = {
  testFolder: 'testFolder',
  separate: 'separate'
}

const toMatchScreenshot = async (testController, selector, options) => {
  const userOptions =
    typeof options === 'object' ?
      options
      :
      typeof options === 'string' ?
        { screenshotName: options }
        :
        {}

  const screenshotHandlerOptions = {
    ...defaultOptions,
    ...getUserGeneralOptions(testController.testRun.test.testFile.filename),
    ...userOptions
  }

  const screenshotHandler = new ScreenshotHandler(testController, screenshotHandlerOptions)

  const callback =
    screenshotHandlerOptions.handleResult ?
      screenshotHandlerOptions.handleResult
      :
      async (result) => {
        result.handle()
        await result.assert()
        result.log()
      }

  return screenshotHandler.handleScreenshot(selector).then(callback).then(() => { screenshotHandler.updateMetadata() })
}

const getUserGeneralOptions = (initialConfigSearchPath) => {
  let options = {}

  let rootDirectory = path.dirname(require.main.filename)
  while (path.basename(rootDirectory) !== 'node_modules') {
    rootDirectory = path.join(rootDirectory, '..')
  }
  const stopSearchDirectory = path.join(rootDirectory, '..', '..')

  let searchDirectory = path.dirname(initialConfigSearchPath)
  while (searchDirectory.length > 1 && searchDirectory !== stopSearchDirectory) {
    try {
      const userGeneralOptions = require(path.join(searchDirectory, '.toMatchScreenshot.config.js'))
      if (userGeneralOptions) {
        options = userGeneralOptions
        break
      }
    } catch (e) { }
    searchDirectory = path.join(searchDirectory, '..')
  }

  return options
}

export default toMatchScreenshot