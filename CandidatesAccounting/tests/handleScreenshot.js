import fs from 'fs'
import resemble from 'resemblejs'

export default async function compareScreenshots(baseScreenshotPath, newScreenshotPath, differencePath){
  return new Promise((resolve, reject) => {
    if (fs.existsSync(baseScreenshotPath)) {
      resemble(baseScreenshotPath).compareTo(newScreenshotPath).onComplete(data => {
        console.log(data)
        if (!data || data.error) {
          reject()
        }

        if (Number(data.misMatchPercentage) > 0) {
          fs.writeFile(differencePath, data.getBuffer())
          resolve(false)
        } else {
          fs.unlink(newScreenshotPath)
          fs.unlink(differencePath)
          resolve(true)
        }
      })
    } else {
      fs.renameSync(newScreenshotPath, baseScreenshotPath)
      resolve()
    }
  })
}
