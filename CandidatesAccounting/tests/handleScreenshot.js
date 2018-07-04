import fs from 'fs'
import compareImages from 'resemblejs/compareImages'

const defaultOptions = {
  maxMisMatchPersentage: 2,
  output: {
    errorColor: {
      red: 255,
      green: 0,
      blue: 0
    },
    errorType: 'movement',
    transparency: 0.85,
    largeImageThreshold: 1200,
    useCrossOrigin: false,
    outputDiff: true
  },
  scaleToSameSize: true,
  ignore: 'antialiasing'
}

export default async function handleScreenshot(t, selector, options) {
  const comparisonOptions = {
    ...defaultOptions,
    ...options
  }

  const browserName = t.testRun.browserConnection.browserInfo.providerName
  const testName = formatName(t.testRun.test.name)
  const fixtureFolderName = formatName(t.testRun.test.fixture.name)

  const testScreenshotsPath = t.testRun.opts.screenshotPath + '\\' + fixtureFolderName + '\\' + browserName + '\\'

  const screenshotNumber = getScreenshotNumber(t, testName)
  const newScreenshotName = testName + '-' + screenshotNumber + '.png'
  const baseScreenshotName = testName + '-' + screenshotNumber + '-base.png'
  const diffScreenshotName = testName + '-' + screenshotNumber + '-diff.png'

  let screenshotsAreDifferent = false
  let misMatchPercentage = 0

  const baseScreenshotExist = fs.existsSync(testScreenshotsPath + baseScreenshotName)
  if (baseScreenshotExist) {
    await t.takeElementScreenshot(selector, fixtureFolderName + '\\' + browserName + '\\' + newScreenshotName)

    const comparisonResult =
      await compareScreenshotWithBaseOne(
        testScreenshotsPath + baseScreenshotName,
        testScreenshotsPath + newScreenshotName,
        comparisonOptions
      )

    logComparisonResult(comparisonResult, browserName, newScreenshotName, comparisonOptions)

    misMatchPercentage = Number(comparisonResult.misMatchPercentage)

    if (misMatchPercentage <= comparisonOptions.maxMisMatchPersentage) {
      deleteScreenshot(testScreenshotsPath + newScreenshotName)
      deleteScreenshot(testScreenshotsPath + diffScreenshotName)
    } else {
      screenshotsAreDifferent = true
      writeDifferenceScreenshot(testScreenshotsPath + diffScreenshotName, comparisonResult.getBuffer())
    }
  } else {
    await t.takeElementScreenshot(selector, fixtureFolderName + '\\' + browserName + '\\' + baseScreenshotName)
    logBaseScreenshotCreation(browserName, baseScreenshotName)
  }

  const assertiondFailMessage = 'There is a difference between screenshots (' + misMatchPercentage + '%). Check ' + testScreenshotsPath + diffScreenshotName
  await t.expect(screenshotsAreDifferent).notOk(assertiondFailMessage)

  return t
}


function formatName(name) {
  let testFolderName = ''
  let spaceRemoved = false

  for (let i = 0; i < name.length; i++) {
    if (name[i] === ' ') {
      spaceRemoved = true
      continue
    }
    if (spaceRemoved) {
      testFolderName += name[i].toUpperCase()
      spaceRemoved = false
    } else {
      testFolderName += name[i].toLowerCase()
    }
  }

  return testFolderName
}


function getScreenshotNumber(t, testName) {
  let screenshotNumber = 1
  if (!t.ctx[testName] || !t.ctx[testName].screenshotNumber) {
    t.ctx[testName] = { screenshotNumber: 1 }
  } else {
    screenshotNumber++
    t.ctx[testName].screenshotNumber = screenshotNumber
  }
  return screenshotNumber
}


async function compareScreenshotWithBaseOne(baseScreenshotPath, newScreenshotPath, options) {
  const baseScreenshot = fs.readFileSync(baseScreenshotPath)
  const newScreenshot = fs.readFileSync(newScreenshotPath)

  return await compareImages(baseScreenshot, newScreenshot, options)
}


function deleteScreenshot(path) {
  if (fs.existsSync(path)) {
    fs.unlinkSync(path)
  }
}


function writeDifferenceScreenshot(path, buffer) {
  fs.writeFileSync(path, buffer)
}


function logComparisonResult(comparisonResult, browserName, screenshotName, options) {
  const passed = comparisonResult.misMatchPercentage <= options.maxMisMatchPersentage
  let consoleStyle = passed ? '\x1b[32m%s\x1b[0m' : '\x1b[31m%s\x1b[0m'

  let message = 'Screenshot comparison (' + browserName + ') ' + (passed ? 'passed' : 'failed') + ': '
  message += screenshotName + '. '
  message += 'Difference ' + comparisonResult.misMatchPercentage + '% '
  message += passed ? 'within the norm (<=' + options.maxMisMatchPersentage + '%). ' : 'outside the norm (>' + options.maxMisMatchPersentage + '%). '

  console.log(consoleStyle, message)
}


function logBaseScreenshotCreation(browserName, baseScreenshotName) {
  let consoleStyle = '\x1b[34m%s\x1b[0m'
  let message = 'New base screenshot is created (' + browserName + '): ' + baseScreenshotName + '. '
  console.log(consoleStyle, message)
}