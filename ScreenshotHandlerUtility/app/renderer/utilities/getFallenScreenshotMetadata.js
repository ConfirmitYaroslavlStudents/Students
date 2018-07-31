import fs from 'fs'

const getFallenScreenshotMetadata = (testMetadataURL) => {
  const testMetadata = JSON.parse(fs.readFileSync(testMetadataURL))

  const fallenScreenshots = {}
  let fallenScreenshotIndex = 0
  for (let i = 0; i < testMetadata.length; i++) {
    if (!testMetadata[i].passed) {
      testMetadata[i].index = fallenScreenshotIndex
      testMetadata[i].unread = true
      testMetadata[i].markedToUpdate = false
      testMetadata[i].markedAsError = false
      fallenScreenshots[fallenScreenshotIndex] = testMetadata[i]
      fallenScreenshotIndex++
    }
  }

  return {
    fallenScreenshots: fallenScreenshots,
    screenshotTotalAmount: testMetadata.length
  }
}

export default getFallenScreenshotMetadata