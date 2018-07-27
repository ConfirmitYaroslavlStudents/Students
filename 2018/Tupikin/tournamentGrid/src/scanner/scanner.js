/* eslint-disable curly */
const colors = require('colors');
const { question } = require('readline-sync');

function read() {
  return question();
}

function readNames(namesCount) {
  console.log('Please enter names (through the enter key)'.bgCyan);
  const names = [];

  while (names.length < namesCount) {
    const name = read();
    if (!names.includes(name))
      names.push(name);
    else
      console.log('You can only enter unique names'.red.underline);
  }

  return names;
}

module.exports = { readNames, read };
