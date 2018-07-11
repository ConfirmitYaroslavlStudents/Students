import fs from 'fs'
import path from 'path'
import compareImages from 'resemblejs/compareImages'
import defaultOptions from './default.config.json'
import { fallenTestSaveStrategies } from './index'
import createFile from './utilities/createFile'
import deleteFile from './utilities/deleteFile'
import deleteFolders from './utilities/deleteFolders'

export default class ScreenshotHandler {
  constructor(testController, userOptions) {
    this.t = testController

    this.comparisonResultHandling = true
    this.logging = true
    this.asserting = true

    this.setOptions(defaultOptions, userOptions)
    this.setScreenshotNames()
    this.setPaths()
  }

  setOptions = (defaultOptions, userOptions) => {
    this.options = {
      ...defaultOptions,
      ...userOptions
    }

    this.comparerOptions = {
      scaleToSameSize: this.options.comparison.scaleToSameSize,
      ignore: this.options.comparison.ignore,
      output: {
        ...this.options.output.difference
      }
    }
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

  setPaths = () => {
    this.browserName = this.t.testRun.browserConnection.browserInfo.providerName
    this.testName = this.formatName(this.t.testRun.test.name)

    this.screenshotDirectory = this.t.testRun.opts.screenshotPath
    const fixtureFolderName = this.formatName(this.t.testRun.test.fixture.name)
    this.testDirectory = path.join(fixtureFolderName, this.browserName, this.testName)

    this.baseScreenshotURL = path.join(this.testDirectory, this.baseScreenshotName)

    this.screenshotURL = path.join(this.testDirectory, this.newScreenshotName)

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

  handleScreenshot = async (selector) => {
    let comparisonResult = { comparisonPerformed: false }

    const baseScreenshotExist = fs.existsSync(path.join(this.screenshotDirectory, this.baseScreenshotURL))

    if (baseScreenshotExist) {
      comparisonResult = await this.compareNewScreenshotWithBaseOne(selector)

      if (this.comparisonResultHandling) {
        await this.handleComparisonResult(comparisonResult)
      }
      if (this.logging) {
        this.logComparisonResult(comparisonResult)
      }
      if (this.asserting) {
        await this.assert(comparisonResult)
      }
    } else {
      await this.createBaseScreenshot(selector)
      this.logBaseScreenshotCreated()
    }

    return this.formatResult(comparisonResult)
  }

  compareNewScreenshotWithBaseOne = async (selector) => {
    await this.t.takeElementScreenshot(selector, this.screenshotURL)
    if (!this.options.output.createThumbnails) {
      this.removeThumbnailFor(path.join(this.screenshotDirectory, this.screenshotURL))
    }

    const baseScreenshot = fs.readFileSync(path.join(this.screenshotDirectory, this.baseScreenshotURL))
    const newScreenshot = fs.readFileSync(path.join(this.screenshotDirectory, this.screenshotURL))

    const comparisonResult = await compareImages(baseScreenshot, newScreenshot, this.comparerOptions)
    comparisonResult.comparisonPerformed = true
    comparisonResult.differenceWithinNorm = (Number(comparisonResult.misMatchPercentage) <= this.options.comparison.maxMisMatchPercentage)

    return comparisonResult
  }

  handleComparisonResult = async (comparisonResult) => {
    if (comparisonResult.differenceWithinNorm) {
      this.handleComparisonPassed()
    } else {
      await this.handleComparisonFailed(comparisonResult)
    }
  }

  handleComparisonPassed = () => {
    deleteFile(path.join(this.screenshotDirectory, this.screenshotURL))
    deleteFile(path.join(this.screenshotDirectory, this.diffScreenshotURL))

    switch (this.options.output.fallenTestSaveStrategy) {
      case fallenTestSaveStrategies.separate:
        deleteFile(path.join(this.screenshotDirectory, this.fallenTestFolder, this.baseScreenshotURL))
        deleteFile(path.join(this.screenshotDirectory, this.fallenTestFolder, this.screenshotURL))
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

        const screenshotNewURL = path.join(this.screenshotDirectory, this.fallenTestFolder, this.screenshotURL)
        fs.renameSync(path.join(this.screenshotDirectory, this.screenshotURL), screenshotNewURL)
        this.screenshotURL = screenshotNewURL
    }
  }

  createBaseScreenshot = async (selector) => {
    await this.t.takeElementScreenshot(selector, this.baseScreenshotURL)
    if (!this.options.output.createThumbnails) {
      this.removeThumbnailFor(path.join(this.screenshotDirectory, this.baseScreenshotURL))
    }
  }

  logBaseScreenshotCreated = () => {
    const message = `(${this.browserName}) new base screenshot for ${this.testName} (${this.baseScreenshotName}) is created.`
    console.log(`\x1b[34m${message}\x1b[0m`)
  }

  logComparisonResult = (comparisonResult) => {
    if(comparisonResult.differenceWithinNorm) {
      this.logComparisonPassed(comparisonResult)
    } else {
      this.logComparisonFailed(comparisonResult)
    }
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

  assert = async (comparisonResult) => {
    const assertionFailedMessage =
      `There is a difference between screenshots (${comparisonResult.misMatchPercentage}%). `
      + `Check ${path.join(this.screenshotDirectory, this.diffScreenshotURL)}`

    await this.t.expect(comparisonResult.differenceWithinNorm).ok(assertionFailedMessage)
  }

  formatResult = (comparisonResult) => {
    const result = {
      testName: this.t.testRun.test.name,
      browserName: this.browserName,
      newBaseScreenshotWasCreated: false,
      baseScreenshotURL: path.join(this.screenshotDirectory, this.baseScreenshotURL),
      maxMisMatchPercentage: this.options.comparison.maxMisMatchPercentage,
      comparisonPerformed: comparisonResult.comparisonPerformed
    }

    if (result.comparisonPerformed) {
      result.misMatchPercentage = Number(comparisonResult.misMatchPercentage)
      result.isSameDimensions = comparisonResult.isSameDimensions
      result.dimensionDifference = comparisonResult.dimensionDifference
      result.diffBounds = comparisonResult.diffBounds
      result.analysisTime = comparisonResult.analysisTime
      result.comparisonPassed = comparisonResult.differenceWithinNorm

      const newScreenshotURL = path.join(this.screenshotDirectory, this.screenshotURL)
      if (fs.existsSync(newScreenshotURL)) {
        result.newScreenshotURL = newScreenshotURL
      }

      if (!comparisonResult.differenceWithinNorm) {
        result.diffScreeshotURL = path.join(this.screenshotDirectory, this.diffScreenshotURL)
      }

      if (!this.comparisonResultHandling) {
        delete result.diffScreeshotURL
        result.getDiffScreenshotBuffer = comparisonResult.getBuffer
        result.handleResult = () => { return this.handleComparisonResult(comparisonResult) }
      }

      if (!this.logging) {
        result.logResult = () => { this.logComparisonResult(comparisonResult) }
      }

      if (!this.asserting) {
        result.assert = () => { return this.assert(comparisonResult) }
      }
    } else {
      result.newBaseScreenshotWasCreated = true
      result.comparisonPerformed = false
    }

    return result
  }

  removeThumbnailFor = (screenshotAbsPath) => {
    const thumbnailsPath = path.join(path.dirname(screenshotAbsPath), 'thumbnails')
    const screenshotName = path.basename(screenshotAbsPath)

    try {
      deleteFile(path.join(thumbnailsPath, screenshotName))
      fs.rmdirSync(thumbnailsPath)
    }
    catch (e) {
      console.log(e)
    }
  }
}