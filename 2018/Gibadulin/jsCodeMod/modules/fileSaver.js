module.exports = function (pathToFile, fileSource) {
  const fs = require('fs');
  return fs.writeFileSync(pathToFile, fileSource, 'utf8');
};
