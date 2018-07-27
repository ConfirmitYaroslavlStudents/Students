import fs from 'fs'

const getScreenshotMetadata = (testMetadataURL) => {
  const tests = JSON.parse(fs.readFileSync(testMetadataURL))

  const fallenTests = {}
  let fallenTestIndex = 0
  for (let i = 0; i < tests.length; i++) {
    if (!tests[i].passed) {
      tests[i].index = fallenTestIndex
      tests[i].unread = true
      tests[i].markedToUpdate = false
      fallenTests[fallenTestIndex] = tests[i]
      fallenTestIndex++
    }
  }

  return {
    fallenTests,
    testTotalAmount: tests.length
  }
}

export default getScreenshotMetadata