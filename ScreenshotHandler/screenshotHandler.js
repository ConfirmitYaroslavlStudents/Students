import fs from 'fs'
import path from 'path'
import compareImages from 'resemblejs/compareImages'
import defaultOptions from './default.config.json'

export default class ScreenshotHandler {
  constructor(testController, customOptions) {
    this.t = testController
    this.options = {
      ...defaultOptions,
      ...customOptions
    }

    const screenshotName =
      this.options.screenshotName && this.options.screenshotName !== '' ?
        `scr_${this.getScreenshotNumber()}_${this.options.screenshotName}`
        :
        `scr_${this.getScreenshotNumber()}`

    this.screenshotName = `${screenshotName}.png`
    this.baseScreenshotName = `${screenshotName}_base.png`
    this.diffScreenshotName = `${screenshotName}_diff.png`

    this.browserName = testController.testRun.browserConnection.browserInfo.providerName
    this.testName = this.formatName(testController.testRun.test.name)
    const fixtureFolderName = this.formatName(testController.testRun.test.fixture.name)
    this.testSpecificFolder = path.join(fixtureFolderName, this.browserName, this.testName)
    this.screenshotsPath = testController.testRun.opts.screenshotPath

    this.baseScreenshotRelativePath = path.join(this.testSpecificFolder, this.baseScreenshotName)
    this.baseScreenshotAbsPath = path.join(this.screenshotsPath, this.baseScreenshotRelativePath)

    this.baseAndDiffScreenshotsAreSeparated = this.options.fallenTestFolder && this.options.fallenTestFolder.trim() !== ''

    if (this.baseAndDiffScreenshotsAreSeparated) {
      this.baseScreenshotCopyAbsPath = path.join(this.screenshotsPath, this.options.fallenTestFolder, this.baseScreenshotRelativePath)

      this.screenshotRelativePath = path.join(this.options.fallenTestFolder, this.testSpecificFolder, this.screenshotName)
      this.screenshotAbsPath = path.join(this.screenshotsPath, this.screenshotRelativePath)

      this.diffScreenshotAbsPath =
        path.join(this.screenshotsPath, this.options.fallenTestFolder, this.testSpecificFolder, this.diffScreenshotName)
    } else {
      this.screenshotRelativePath = path.join(this.testSpecificFolder, this.screenshotName)
      this.screenshotAbsPath = path.join(this.screenshotsPath, this.screenshotRelativePath)

      this.diffScreenshotAbsPath = path.join(this.screenshotsPath, this.testSpecificFolder, this.diffScreenshotName)
    }
  }


  handleScreenshot = async (selector) => {
    const baseScreenshotDoesNotExist = !fs.existsSync(this.baseScreenshotAbsPath)

    if (baseScreenshotDoesNotExist) {
      await this.createBaseScreenshot(selector)
    } else {
      const comparisonResult = await this.compareNewScreenshotWithBaseOne(selector)

      const assertionFailedMessage =
        `There is a difference between screenshots (${comparisonResult.misMatchPercentage}%). `
        + `Check ${path.join(this.diffScreenshotAbsPath)}`
      await this.t.expect(comparisonResult.differenceWithinNorm).ok(assertionFailedMessage)
    }
  }

  createBaseScreenshot = async (selector) => {
    await this.t.takeElementScreenshot(selector, this.baseScreenshotRelativePath)
    if (!this.options.thumbnails) {
      this.removeThumbnail(this.baseScreenshotAbsPath)
    }

    this.logBaseScreenshotCreated()
  }

  compareNewScreenshotWithBaseOne = async (selector) => {
    await this.t.takeElementScreenshot(selector, this.screenshotRelativePath)
    if (!this.options.thumbnails) {
      this.removeThumbnail(this.screenshotAbsPath)
    }

    const baseScreenshot = fs.readFileSync(this.baseScreenshotAbsPath)
    const newScreenshot = fs.readFileSync(this.screenshotAbsPath)

    const comparisonResult = await compareImages(baseScreenshot, newScreenshot, this.options)
    comparisonResult.differenceWithinNorm = Number(comparisonResult.misMatchPercentage) <= this.options.maxMisMatchPercentage

    if (comparisonResult.differenceWithinNorm) {
      this.handleComparisonPassed(comparisonResult)
    } else {
      await this.handleComparisonFailed(comparisonResult)
    }

    return comparisonResult
  }

  handleComparisonPassed = (comparisonResult) => {
    this.deleteScreenshot(this.screenshotAbsPath)
    this.deleteScreenshot(this.diffScreenshotAbsPath)
    if (this.baseAndDiffScreenshotsAreSeparated) {
      this.deleteScreenshot(this.baseScreenshotCopyAbsPath)
    }
    this.removeFallenTestFolder()

    this.logComparisonPassed(comparisonResult)
  }

  deleteScreenshot = (screenshotPath) => {
    if (fs.existsSync(screenshotPath)) {
      fs.unlinkSync(screenshotPath)
    }
  }

  handleComparisonFailed = async (comparisonResult) => {
    fs.writeFileSync(this.diffScreenshotAbsPath, comparisonResult.getBuffer())
    if (this.baseAndDiffScreenshotsAreSeparated) {
      fs.copyFileSync(this.baseScreenshotAbsPath, this.baseScreenshotCopyAbsPath)
    }
    this.logComparisonFailed(comparisonResult)
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


  logBaseScreenshotCreated = () => {
    const message = `(${this.browserName}) new base screenshot for ${this.testName} (${this.baseScreenshotName}) is created.`
    console.log(`\x1b[34m${message}\x1b[0m`)
  }

  logComparisonPassed = (comparisonResult) => {
    const message =
      `(${this.browserName}) screenshot comparison for ${this.testName} (${this.screenshotName}) passed: difference `
      + `${comparisonResult.misMatchPercentage}% within the norm (<= ${this.options.maxMisMatchPercentage}%).`

    console.log(`\x1b[32m${message}\x1b[0m`)
  }

  logComparisonFailed = (comparisonResult) => {
    const message =
      `(${this.browserName}) screenshot comparison for ${this.testName} (${this.screenshotName}) failed: difference `
      + `${comparisonResult.misMatchPercentage}% outside the norm (> ${this.options.maxMisMatchPercentage}%).`

    console.log(`\x1b[31m${message}\x1b[0m'`)
  }

  removeThumbnail = (screenshotAbsPath) => {
    const splitedPath = screenshotAbsPath.split('\\')
    let thumbnailsPath = ''
    for (let i = 0; i < splitedPath.length - 1; i++) {
      thumbnailsPath += splitedPath[i] + '\\'
    }
    thumbnailsPath += 'thumbnails\\'
    const screenshotName = splitedPath[splitedPath.length - 1]

    try {
      this.deleteScreenshot(path.join(thumbnailsPath, screenshotName))
      fs.rmdirSync(thumbnailsPath)
    }
    catch (e) {
      console.log(e)
    }
  }

  removeFallenTestFolder = () => {
    let pathToRemove = path.join(this.options.fallenTestFolder, this.testSpecificFolder)
    while (pathToRemove.length > 1) {
      try {
        fs.rmdirSync(path.join(this.screenshotsPath, pathToRemove))
        const lastFolderNameLength = pathToRemove.split('\\').pop().length
        pathToRemove = pathToRemove.slice(0, pathToRemove.length - lastFolderNameLength - 1)
      }
      catch (e) {
        break
      }
    }
  }
}