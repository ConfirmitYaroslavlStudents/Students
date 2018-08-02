const updateMetadata = (metadata, testToUpdate) => {
  for (let i = 0; i < metadata.length; i++) {
    const test = metadata[i]
    if (
      test.fixtureName === testToUpdate.fixtureName
      && test.browserName === testToUpdate.browserName
      && test.testName === testToUpdate.testName
      && test.number === testToUpdate.number
    ) {
      test.passed = true

      delete test.newScreenshotURL
      delete test.diffScreenshotURL
      delete test.misMatchPercentage
      delete test.maxMisMatchPercentage

      break
    }
  }

  return metadata
}

export default updateMetadata