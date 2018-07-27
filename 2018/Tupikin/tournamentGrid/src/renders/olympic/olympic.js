/* eslint-disable curly */
const config = require('./../config');

function olympic(state) {
  const grid = [];
  const sessions = state.sessions;

  config.setNames(state.names);

  for (let g = 0; g < sessions.length; g++) {
    grid[g] = [];
    const offset = g ? g * 2 - 1 : 0;
    for (let i = 0; i < offset; i++)
      grid[g].push(config.emptyString);
    for (const game of sessions[g]) {
      grid[g].push(config.addNameToLeft(game.getFirst()) + config.dl);
      for (let i = 0; i < offset * 2 + 1; i++)
        grid[g].push(config.verticalRight);
      grid[g].push(config.addNameToLeft(game.getSecond()) + config.ul);
      for (let i = 0; i < offset * 2 + 1; i++)
        grid[g].push(config.emptyString);
    }
  }

  const winner = sessions.length;
  grid[winner] = [];

  for (let i = 0; i < state.names.length - 1; i++)
    grid[winner].push(config.emptyString);
  grid[winner].push(sessions[winner - 1][0].getWinner());
  for (let i = 0; i < state.names.length - 1; i++)
    grid[winner].push(config.emptyString);

  grid[0] = grid[0].slice(0, grid[0].length - 1);

  return grid;
}

module.exports = olympic;
