const olympic = require('./../olympic/olympic');

function losers(state) {
  const mainGrid = olympic(state);

  return mainGrid;
}

module.exports = losers;
