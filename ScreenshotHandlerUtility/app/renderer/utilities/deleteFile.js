import fs from 'fs'

const deleteFile = (fileURL) => {
  if (fs.existsSync(fileURL)) {
    fs.unlinkSync(fileURL)
  }
}

export default deleteFile