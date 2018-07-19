import fs from 'fs'
import path from 'path'

export default class Result {
  constructor(screenshotHandler, handleScreenshotResult) {
    this.testName = screenshotHandler.t.testRun.test.name
    this.browserName = screenshotHandler.browserName
    this.comparisonPerformed = handleScreenshotResult.comparisonPerformed
    this.baseScreenshotURL = path.join(screenshotHandler.screenshotDirectory, screenshotHandler.baseScreenshotURL)
    this.maxMisMatchPercentage = screenshotHandler.options.comparison.maxMisMatchPercentage

    if (handleScreenshotResult.comparisonPerformed) {
      this.misMatchPercentage = Number(handleScreenshotResult.misMatchPercentage)
      this.isSameDimensions = handleScreenshotResult.isSameDimensions
      this.dimensionDifference = handleScreenshotResult.dimensionDifference
      this.diffBounds = handleScreenshotResult.diffBounds
      this.analysisTime = handleScreenshotResult.analysisTime
      this.comparisonPassed = handleScreenshotResult.differenceWithinNorm

      const newScreenshotURL = path.join(screenshotHandler.screenshotDirectory, screenshotHandler.newScreenshotURL)
      if (fs.existsSync(newScreenshotURL)) {
        this.newScreenshotURL = newScreenshotURL
      }

      if (!handleScreenshotResult.differenceWithinNorm) {
        const diffScreenshotURL = path.join(screenshotHandler.screenshotDirectory, screenshotHandler.diffScreenshotURL)
        if (fs.existsSync(diffScreenshotURL)) {
          this.diffScreeshotURL = diffScreenshotURL
        }
      }

      this.getDiffScreenshotBuffer = handleScreenshotResult.getBuffer

      this.handle =
        handleScreenshotResult.differenceWithinNorm ?
          async () => screenshotHandler.handleComparisonPassed()
          :
          async () => screenshotHandler.handleComparisonFailed(handleScreenshotResult)

      this.assert = async () => screenshotHandler.assert(handleScreenshotResult)

      this.log =
        handleScreenshotResult.differenceWithinNorm ?
          () => screenshotHandler.logComparisonPassed(handleScreenshotResult)
          :
          () => screenshotHandler.logComparisonFailed(handleScreenshotResult)
    } else {
      this.handle = () => {}
      this.assert = () => {}
      this.log = () => screenshotHandler.logNewBaseScreenshotCreated()
    }
  }
}