import fs from 'fs'
import path from 'path'
import compareImages from 'resemblejs/compareImages'
import { fallenTestSaveStrategies } from './index'
import createFile from './utilities/createFile'
import deleteFile from './utilities/deleteFile'
import deleteFolders from './utilities/deleteFolders'
import Result from './result'

export default class ScreenshotHandler {
  constructor(testController, options) {
    this.t = testController

    this.options = options

    this.comparerOptions = {
      scaleToSameSize: this.options.comparison.scaleToSameSize,
      ignore: this.options.comparison.ignore,
      output: {
        ...this.options.output.difference
      }
    }

    this.setScreenshotNames()
    this.setPaths()
  }

  setScreenshotNames = () => {
    const screenshotName =
      this.options.screenshotName && this.options.screenshotName !== '' ?
        `scr_${this.getScreenshotNumber()}_${this.options.screenshotName}`
        :
        `scr_${this.getScreenshotNumber()}`

    this.newScreenshotName = `${screenshotName}_new.png`
    this.baseScreenshotName = `${screenshotName}_base.png`
    this.diffScreenshotName = `${screenshotName}_diff.png`
  }

  getScreenshotNumber = () => {
    const testCtx = Symbol.for(this.testName)

    const screenshotNumber =
      this.t.ctx[testCtx] && this.t.ctx[testCtx].screenshotNumber ?
        this.t.ctx[testCtx].screenshotNumber + 1
        :
        1

    this.t.ctx[testCtx] = { screenshotNumber }

    return screenshotNumber
  }

  setPaths = () => {
    this.browserName = this.t.testRun.browserConnection.browserInfo.providerName
    this.testName = this.formatName(this.t.testRun.test.name)

    this.screenshotDirectory = this.t.testRun.opts.screenshotPath
    const fixtureFolderName = this.formatName(this.t.testRun.test.fixture.name)
    this.testDirectory = path.join(fixtureFolderName, this.browserName, this.testName)

    this.baseScreenshotURL = path.join(this.testDirectory, this.baseScreenshotName)

    this.newScreenshotURL = path.join(this.testDirectory, this.newScreenshotName)

    this.diffScreenshotURL = path.join(this.testDirectory, this.diffScreenshotName)

    if (this.options.output.fallenTestSaveStrategy === fallenTestSaveStrategies.separate) {
      this.fallenTestFolder = 'fallenTests'
      this.diffScreenshotURL = path.join(this.fallenTestFolder, this.testDirectory, this.diffScreenshotName)
    }
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

  handleScreenshot = async (selector) => {
    let handleScreenshotResult = { comparisonPerformed: false }

    const baseScreenshotExist = fs.existsSync(path.join(this.screenshotDirectory, this.baseScreenshotURL))

    if (baseScreenshotExist) {
      await this.takeScreenshot(selector, this.newScreenshotURL)
      handleScreenshotResult = await this.compareNewScreenshotWithBaseOne()
    } else {
      await this.takeScreenshot(selector, this.baseScreenshotURL)
    }

    return new Result(this, handleScreenshotResult)
  }

  takeScreenshot = async (selector, screenshotURL) => {
    await this.t.takeElementScreenshot(selector, screenshotURL)
    if (!this.options.output.createThumbnails) {
      const screenshotAbsPath = path.join(this.screenshotDirectory, screenshotURL)
      const thumbnailsPath = path.join(path.dirname(screenshotAbsPath), 'thumbnails')
      const screenshotName = path.basename(screenshotAbsPath)
      try {
        deleteFile(path.join(thumbnailsPath, screenshotName))
        fs.rmdirSync(thumbnailsPath)
      }
      catch (e) { }
    }
  }

  compareNewScreenshotWithBaseOne = async () => {
    const baseScreenshot = fs.readFileSync(path.join(this.screenshotDirectory, this.baseScreenshotURL))
    const newScreenshot = fs.readFileSync(path.join(this.screenshotDirectory, this.newScreenshotURL))

    const comparisonResult = await compareImages(baseScreenshot, newScreenshot, this.comparerOptions)
    comparisonResult.comparisonPerformed = true
    comparisonResult.differenceWithinNorm = (Number(comparisonResult.misMatchPercentage) <= this.options.comparison.maxMisMatchPercentage)

    return comparisonResult
  }

  handleComparisonPassed = () => {
    deleteFile(path.join(this.screenshotDirectory, this.newScreenshotURL))
    deleteFile(path.join(this.screenshotDirectory, this.diffScreenshotURL))

    switch (this.options.output.fallenTestSaveStrategy) {
      case fallenTestSaveStrategies.separate:
        deleteFile(path.join(this.screenshotDirectory, this.fallenTestFolder, this.baseScreenshotURL))
        deleteFile(path.join(this.screenshotDirectory, this.fallenTestFolder, this.newScreenshotURL))
        deleteFolders(this.screenshotDirectory, path.join(this.fallenTestFolder, this.testDirectory))
    }
  }

  handleComparisonFailed = async (comparisonResult) => {
    createFile(path.join(this.screenshotDirectory, this.diffScreenshotURL), comparisonResult.getBuffer())

    switch (this.options.output.fallenTestSaveStrategy) {
      case fallenTestSaveStrategies.separate:
        fs.copyFileSync(
          path.join(this.screenshotDirectory, this.baseScreenshotURL),
          path.join(this.screenshotDirectory, this.fallenTestFolder, this.baseScreenshotURL))

        const newScreenshotNewURL = path.join(this.screenshotDirectory, this.fallenTestFolder, this.newScreenshotURL)
        fs.renameSync(path.join(this.screenshotDirectory, this.newScreenshotURL), newScreenshotNewURL)
        this.newScreenshotURL = newScreenshotNewURL
    }
  }

  assert = async (comparisonResult) => {
    const assertionFailedMessage =
      `There is a difference between screenshots (${comparisonResult.misMatchPercentage}%). `
      + `Check ${path.join(this.screenshotDirectory, this.diffScreenshotURL)}`

    await this.t.expect(comparisonResult.differenceWithinNorm).ok(assertionFailedMessage)
  }

  logComparisonPassed = (comparisonResult) => {
    const message =
      `(${this.browserName}) screenshot comparison for ${this.testName} (${this.newScreenshotName}) passed: difference `
      + `${comparisonResult.misMatchPercentage}% within the norm (<= ${this.options.comparison.maxMisMatchPercentage}%).`

    console.log(`\x1b[32m${message}\x1b[0m`)
  }

  logComparisonFailed = (comparisonResult) => {
    const message =
      `(${this.browserName}) screenshot comparison for ${this.testName} (${this.newScreenshotName}) failed: difference `
      + `${comparisonResult.misMatchPercentage}% outside the norm (> ${this.options.comparison.maxMisMatchPercentage}%).`

    console.log(`\x1b[31m${message}\x1b[0m'`)
  }

  logNewBaseScreenshotCreated = () => {
    const message = `(${this.browserName}) new base screenshot for ${this.testName} (${this.baseScreenshotName}) is created.`
    console.log(`\x1b[34m${message}\x1b[0m`)
  }
}