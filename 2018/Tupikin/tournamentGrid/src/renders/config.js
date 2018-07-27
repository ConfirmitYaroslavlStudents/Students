/* eslint-disable curly */
const vl = '║';

const dl = '╗';
const ul = '╝';

const dr = '╔';
const ur = '╚';

let maxNameLength;

function setNames(nameArray) {
  if (maxNameLength)
    return;
  maxNameLength = 0;
  for (const name of nameArray)
    maxNameLength = Math.max(maxNameLength, name.length);

  module.exports.emptyString = ' '.repeat(maxNameLength + 2);
  module.exports.verticalRight = ' '.repeat(maxNameLength + 1) + vl;
  module.exports.verticalLeft = vl + ' '.repeat(maxNameLength + 1);
}

function addNameToLeft(name) {
  return name + ' '.repeat(maxNameLength - name.length + 1);
}

function addNameToRight(name) {
  return ' '.repeat(maxNameLength - name.length + 1) + name;
}

module.exports = {
  dl, ul, dr, ur,
  setNames,
  addNameToLeft,
  addNameToRight
};
