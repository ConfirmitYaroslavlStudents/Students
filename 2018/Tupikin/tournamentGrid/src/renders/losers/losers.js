const olympic = require('./../olympic/olympic');

function losers(state) {
  const mainGrid = olympic(state);

  console.log(state.losersSessions);

  return mainGrid;
}

module.exports = losers;
