const parseScreenshotTestMetadata = (screenshotTestMetadata) => {
  const fallenScreenshots = {}

  let fallenScreenshotIndex = 0
  for (let i = 0; i < screenshotTestMetadata.length; i++) {
    if (!screenshotTestMetadata[i].passed) {
      fallenScreenshots[fallenScreenshotIndex] = {
        ...screenshotTestMetadata[i],
        index: fallenScreenshotIndex,
        unread: true,
        markedToUpdate: false,
        markedAsError: false
      }

      fallenScreenshotIndex++
    }
  }

  return {
    fallenTests: fallenScreenshots,
    testTotalAmount: screenshotTestMetadata.length
  }
}

export default parseScreenshotTestMetadata