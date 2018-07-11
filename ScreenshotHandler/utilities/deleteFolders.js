import fs from 'fs'
import path from 'path'

export default (absPath, relativePathToDelete) => {
  while (relativePathToDelete.length > 1) {
    try {
      fs.rmdirSync(path.join(absPath, relativePathToDelete))
      relativePathToDelete = path.join(relativePathToDelete, '..')
    }
    catch (e) {
      break
    }
  }
}