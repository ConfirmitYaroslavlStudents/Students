import path from 'path'
import ScreenshotHandler from './screenshotHandler'

export const comparisonIgnoreRules = {
  nothing: "nothing",
  antialiasing: "antialiasing",
  less: "less",
  colors: "colors",
  alpha: "alpha"
}

export const differenceDisplayMode = {
  flat: "flat",
  movement: "movement",
  flatWithDifferenceIntensity: "flatDifferenceIntensity",
  movementWithDifferenceIntensity: "movementDifferenceIntensity",
  differenceOnly: "diffOnly"
}

export const fallenTestSaveStrategies = {
  testFolder: 'testFolder',
  separate: 'separate'
}

const toMatchScreenshot = async (testController, selector, options) => {
  const userOptions = {
    ...getUserGeneralOptions(testController.testRun.test.testFile.filename),
    ...options
  }

  const screenshotHandler = new ScreenshotHandler(testController, userOptions)

  return await screenshotHandler.handleScreenshot(selector)
}

const getUserGeneralOptions = (initialConfigSearchPath) => {
  let options = {}

  let rootDirectory = path.dirname(require.main.filename)
  while (path.basename(rootDirectory) !== 'node_modules') {
    rootDirectory = path.join(rootDirectory, '..')
  }
  const stopSearchDirectory = path.join(rootDirectory, '..', '..')

  let searchDirectory = path.dirname(initialConfigSearchPath)
  while (searchDirectory.length > 1 && path.basename(searchDirectory) !== path.basename(stopSearchDirectory)) {
    try {
      const userGeneralOptions = require(path.join(searchDirectory, '.matchScreenshot.config.json'))
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