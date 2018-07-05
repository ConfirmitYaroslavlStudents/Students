import fs from 'fs'
import path from 'path'
import compareImages from 'resemblejs/compareImages'
import defaultOptions from './defaultOptions'

class ScreenshotHandler {
  constructor(testController, selector, customOptions) {
    this.t = testController
    this.selector = selector
    this.options = {
      ...defaultOptions,
      ...customOptions
    }

    this.browserName = testController.testRun.browserConnection.browserInfo.providerName
    this.testName = this.formatName(testController.testRun.test.name)

    const fixtureFolderName = this.formatName(testController.testRun.test.fixture.name)
    this.absoluteTestScreenshotsPath = path.join(testController.testRun.opts.screenshotPath, fixtureFolderName, this.browserName)
    this.relativeTestScreenshotsPath = path.join(fixtureFolderName, this.browserName)

    const screenshotNumber = this.getScreenshotNumber()
    this.screenshotName = `${this.testName}-${screenshotNumber}.png`
    this.baseScreenshotName = `${this.testName}-${screenshotNumber}-base.png`
    this.diffScreenshotName = `${this.testName}-${screenshotNumber}-diff.png`

    this.comparisonResult = {}
  }

  formatName = (name) => {
    let formatedName = ''
    let nextSymbolMustBeInUpperCase = false

    for (let i = 0; i < name.length; i++) {
      if (!name[i].match(/^[a-zA-Z]+$/)) {
        nextSymbolMustBeInUpperCase = true
        continue
      }
      if (nextSymbolMustBeInUpperCase) {
        formatedName += name[i].toUpperCase()
        nextSymbolMustBeInUpperCase = false
      } else {
        formatedName += name[i].toLowerCase()
      }
    }

    return formatedName
  }

  getScreenshotNumber = () => {
    const screenshotNumber =
      this.t.ctx[this.testName] && this.t.ctx[this.testName].screenshotNumber ?
        this.t.ctx[this.testName].screenshotNumber + 1
        :
        1

    this.t.ctx[this.testName] = { screenshotNumber }

    return screenshotNumber
  }

  handleScreenshot = async () => {
    const baseScreenshotDoesNotExist = !fs.existsSync(path.join(this.absoluteTestScreenshotsPath, this.baseScreenshotName))

    if (baseScreenshotDoesNotExist) {
      await this.createBaseScreenshot()
    } else {

      await this.compareNewScreenshotWithBaseOne()

      const assertionFailedMessage =
        `There is a difference between screenshots (${this.comparisonResult.misMatchPercentage}%). `
        + `Check ${path.join(this.absoluteTestScreenshotsPath, this.diffScreenshotName)}`
      await this.t.expect(this.comparisonResult.differenceWithinNorm).ok(assertionFailedMessage)
    }
  }

  createBaseScreenshot = async () => {
    await this.t.takeElementScreenshot(this.selector, path.join(this.relativeTestScreenshotsPath, this.baseScreenshotName))
    this.logBaseScreenshotCreated()
  }

  logBaseScreenshotCreated = () => {
    const message = `New base screenshot (${this.baseScreenshotName}) is created (${this.browserName}).`
    console.log(`\x1b[34m${message}\x1b[0m`)
  }

  compareNewScreenshotWithBaseOne = async () => {
    await this.t.takeElementScreenshot(this.selector, path.join(this.relativeTestScreenshotsPath, this.screenshotName))

    const baseScreenshot = fs.readFileSync(path.join(this.absoluteTestScreenshotsPath, this.baseScreenshotName))
    const newScreenshot = fs.readFileSync(path.join(this.absoluteTestScreenshotsPath, this.screenshotName))

    this.comparisonResult = await compareImages(baseScreenshot, newScreenshot, this.options)
    this.comparisonResult.differenceWithinNorm = Number(this.comparisonResult.misMatchPercentage) <= this.options.maxMisMatchPercentage

    if (this.comparisonResult.differenceWithinNorm) {
      this.handleComparisonPassed()
    } else {
      await this.handleComparisonFailed()
    }
  }

  handleComparisonPassed = () => {
    this.deleteScreenshot(path.join(this.absoluteTestScreenshotsPath, this.screenshotName))
    this.deleteScreenshot(path.join(this.absoluteTestScreenshotsPath, 'thumbnails', this.screenshotName))
    this.deleteScreenshot(path.join(this.absoluteTestScreenshotsPath, this.diffScreenshotName))
    this.deleteScreenshot(path.join(this.absoluteTestScreenshotsPath, 'thumbnails', this.diffScreenshotName))
    this.logComparisonPassed()
  }

  deleteScreenshot = (screenshotPath) => {
    if (fs.existsSync(screenshotPath)) {
      fs.unlinkSync(screenshotPath)
    }
  }

  logComparisonPassed = () => {
    const message =
      `Screenshot comparison (${this.browserName}) for ${this.screenshotName} passed: difference `
      + `${this.comparisonResult.misMatchPercentage}% within the norm (<=${this.options.maxMisMatchPercentage}%).`

    console.log(`\x1b[32m${message}\x1b[0m`)
  }

  handleComparisonFailed = async () => {
    fs.writeFileSync(path.join(this.absoluteTestScreenshotsPath, this.diffScreenshotName), this.comparisonResult.getBuffer())
    this.logComparisonFailed()
  }

  logComparisonFailed = () => {
    const message =
      `Screenshot comparison (${this.browserName}) for ${this.screenshotName} failed: difference `
      + `${this.comparisonResult.misMatchPercentage}% outside the norm (>${this.options.maxMisMatchPercentage}%).`

    console.log(`\x1b[31m${message}\x1b[0m'`)
  }
}

const toMatchScreenshot = async (testController, selector, options) => {
  const screenshotHandler = new ScreenshotHandler(testController, selector, options)

  await screenshotHandler.handleScreenshot()

  return testController
}

export default toMatchScreenshot