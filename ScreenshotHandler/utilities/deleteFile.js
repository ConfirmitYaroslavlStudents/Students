import fs from 'fs'

export default (fileURL) => {
  if (fs.existsSync(fileURL)) {
    fs.unlinkSync(fileURL)
  }
}