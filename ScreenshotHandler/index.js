import ScreenshotHandler from './screenshotHandler'

const toMatchScreenshot = async (testController, selector, options) => {
  const screenshotHandler = new ScreenshotHandler(testController, options)
  await screenshotHandler.handleScreenshot(selector)

  return testController
}

export default toMatchScreenshot