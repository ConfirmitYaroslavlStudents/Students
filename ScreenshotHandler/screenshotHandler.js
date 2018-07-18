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

    this.browserName = this.t.testRun.browserConnection.browserInfo.providerName
    this.testName = this.formatName(this.t.testRun.test.name)
    this.testCtx = Symbol.for(this.testName)

    this.setScreenshotNames()

    this.setPaths()
    this.getMetadata()
  }

  setScreenshotNames = () => {
    this.screenshotNumber =
      this.t.ctx[this.testCtx] && this.t.ctx[this.testCtx].screenshotNumber ?
        this.t.ctx[this.testCtx].screenshotNumber + 1
        :
        1

    this.t.ctx[this.testCtx] = { screenshotNumber: this.screenshotNumber }

    const screenshotName =
      this.options.screenshotName && this.options.screenshotName !== '' ?
        `scr_${this.screenshotNumber}_${this.options.screenshotName}`
        :
        `scr_${this.screenshotNumber}`

    this.newScreenshotName = `${screenshotName}_new.png`
    this.baseScreenshotName = `${screenshotName}_base.png`
    this.diffScreenshotName = `${screenshotName}_diff.png`
  }

  setPaths = () => {
    this.screenshotDirectory = this.t.testRun.opts.screenshotPath
    this.fixtureName = this.formatName(this.t.testRun.test.fixture.name)
    this.testDirectory = path.join(this.fixtureName, this.browserName, this.testName)

    this.baseScreenshotURL = path.join(this.testDirectory, this.baseScreenshotName)

    this.newScreenshotURL = path.join(this.testDirectory, this.newScreenshotName)

    this.diffScreenshotURL = path.join(this.testDirectory, this.diffScreenshotName)

    if (this.options.output.fallenTestSaveStrategy === fallenTestSaveStrategies.separate) {
      this.fallenTestFolder = 'fallenTests'
      this.diffScreenshotURL = path.join(this.fallenTestFolder, this.testDirectory, this.diffScreenshotName)
    }

    this.metadataURL = path.join(this.screenshotDirectory, this.options.metadataURL)
  }

  getMetadata = () => {
    this.metadata =
      fs.existsSync(this.metadataURL) ?
        JSON.parse(fs.readFileSync(this.metadataURL))
        :
        {}

    this.currentTestMetadata =
      this.metadata[this.fixtureName]
      && this.metadata[this.fixtureName][this.browserName]
      && this.metadata[this.fixtureName][this.browserName][this.testName]
      && this.metadata[this.fixtureName][this.browserName][this.testName][this.screenshotNumber] ?
        this.metadata[this.fixtureName][this.browserName][this.testName][this.screenshotNumber]
        :
        null

    if (!this.currentTestMetadata) {
      this.metadata = {
        ...this.metadata,
        [this.fixtureName]: {
          ...this.metadata[this.fixtureName],
          [this.browserName]: {
            ...this.metadata[this.fixtureName][this.browserName],
            [this.testName]: {
              ...this.metadata[this.fixtureName][this.browserName][this.testName],
              [this.screenshotNumber]: {
                passed: true,
                baseScreenshotURL: path.join(this.screenshotDirectory, this.baseScreenshotURL)
              }
            }
          }
        }
      }
      this.currentTestMetadata = this.metadata[this.fixtureName][this.browserName][this.testName][this.screenshotNumber]
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
      this.currentTestMetadata.newScreenshotURL = path.join(this.screenshotDirectory, this.newScreenshotURL)

      handleScreenshotResult = await this.compareNewScreenshotWithBaseOne()
      this.currentTestMetadata.passed = handleScreenshotResult.differenceWithinNorm
    } else {
      await this.takeScreenshot(selector, this.baseScreenshotURL)
      this.currentTestMetadata.baseScreenshotURL = path.join(this.screenshotDirectory, this.baseScreenshotURL)
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

  updateMetadata = () => {
    createFile(this.metadataURL, JSON.stringify(this.metadata))
  }

  handleComparisonPassed = () => {
    deleteFile(path.join(this.screenshotDirectory, this.newScreenshotURL))
    delete this.currentTestMetadata.newScreenshotURL

    deleteFile(path.join(this.screenshotDirectory, this.diffScreenshotURL))
    delete this.currentTestMetadata.diffScreenshotURL

    delete this.currentTestMetadata.misMatchPercentage
    delete this.currentTestMetadata.maxMisMatchPercentage

    switch (this.options.output.fallenTestSaveStrategy) {
      case fallenTestSaveStrategies.separate:
        deleteFile(path.join(this.screenshotDirectory, this.fallenTestFolder, this.baseScreenshotURL))
        deleteFile(path.join(this.screenshotDirectory, this.fallenTestFolder, this.newScreenshotURL))
        deleteFolders(this.screenshotDirectory, path.join(this.fallenTestFolder, this.testDirectory))
    }
  }

  handleComparisonFailed = (comparisonResult) => {
    const diffScreenshotAbsPath = path.join(this.screenshotDirectory, this.diffScreenshotURL)
    createFile(diffScreenshotAbsPath, comparisonResult.getBuffer())
    this.currentTestMetadata.diffScreenshotURL = diffScreenshotAbsPath

    this.currentTestMetadata.misMatchPercentage = Number(comparisonResult.misMatchPercentage)
    this.currentTestMetadata.maxMisMatchPercentage = this.options.comparison.maxMisMatchPercentage

    switch (this.options.output.fallenTestSaveStrategy) {
      case fallenTestSaveStrategies.separate:
        fs.copyFileSync(
          path.join(this.screenshotDirectory, this.baseScreenshotURL),
          path.join(this.screenshotDirectory, this.fallenTestFolder, this.baseScreenshotURL))

        const newScreenshotNewURL = path.join(this.fallenTestFolder, this.newScreenshotURL)
        const newScreenshotAbsPath = path.join(this.screenshotDirectory, newScreenshotNewURL)
        fs.renameSync(path.join(this.screenshotDirectory, this.newScreenshotURL), newScreenshotAbsPath)
        this.newScreenshotURL = newScreenshotNewURL
        this.currentTestMetadata.newScreenshotURL = newScreenshotAbsPath
    }
  }

  assert = async (comparisonResult) => {
    const assertionFailedMessage =
      `There is a difference between screenshots (${comparisonResult.misMatchPercentage}%). `
      + `Check ${path.join(this.screenshotDirectory, this.diffScreenshotURL)}`

    try {
      await this.t.expect(comparisonResult.differenceWithinNorm).ok(assertionFailedMessage)
    }
    catch (e) {
      this.updateMetadata()
      throw e
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

  logNewBaseScreenshotCreated = () => {
    const message = `(${this.browserName}) new base screenshot for ${this.testName} (${this.baseScreenshotName}) is created.`
    console.log(`\x1b[34m${message}\x1b[0m`)
  }
}