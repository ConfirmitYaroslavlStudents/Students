import fs from 'fs'
import path from 'path'

export default (fileURL, buffer) => {
  let currentDirectory = path.dirname(fileURL)
  const directoriesToCreate = []

  while (currentDirectory.length > 1) {
    if (fs.existsSync(currentDirectory)) {
      while (directoriesToCreate.length > 0) {
        currentDirectory = path.join(currentDirectory, directoriesToCreate.pop())
        fs.mkdirSync(currentDirectory)
      }
      fs.writeFileSync(fileURL, buffer)
      break
    } else {
      directoriesToCreate.push(path.basename(currentDirectory))
      currentDirectory = path.join(currentDirectory, '..')
    }
  }
}