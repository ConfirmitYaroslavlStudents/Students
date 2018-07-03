import fs from 'fs'
import resemble from 'resemblejs'

resemble.outputSettings({
  errorColor: {
    red: 255,
    green: 0,
    blue: 0
  },
  errorType: 'movement',
  largeImageThreshold: 1200,
  useCrossOrigin: false,
})

export default function handleScreenshot(screenshotDirectory, newScreenshotFileName, maxMisMatchPercentage) {
  const newScreenshotPath = screenshotDirectory + newScreenshotFileName
  const baseScreenshotPath = screenshotDirectory + newScreenshotFileName.split('.')[0] + '-base.png'
  const diffScreenshotPath = screenshotDirectory + newScreenshotFileName.split('.')[0] + '-diff.png'

  return new Promise((resolve, reject) => {
    if (fs.existsSync(baseScreenshotPath)) {

      // comparation with base screenshot
      resemble(baseScreenshotPath).compareTo(newScreenshotPath).onComplete(data => {
        if (!data || data.error) {
          reject()
        }
        if (Number(data.misMatchPercentage) > maxMisMatchPercentage) {

          // differences output creation
          fs.writeFileSync(diffScreenshotPath, data.getBuffer())
          resolve(false)
        } else {

          // no needed files removal
          fs.unlinkSync(newScreenshotPath)
          if (fs.existsSync(diffScreenshotPath)) {
            fs.unlinkSync(diffScreenshotPath)
          }
          resolve(true)
        }
      })

    } else {

      // base screenshot creation
      fs.renameSync(newScreenshotPath, baseScreenshotPath)
      resolve(true)
    }
  })
}