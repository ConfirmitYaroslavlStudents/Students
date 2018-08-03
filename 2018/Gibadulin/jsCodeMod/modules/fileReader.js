module.exports = function (pathToFile) {
  const fs = require('fs');
  return fs.readFileSync(pathToFile, 'utf8');
};
