const clear = require('clear');
const { readNames, read } = require('../scanner/scanner');
const TournamentState = require('./tournamentState');
const Grid = require('./grid');
const { playersCountValidation } = require('../utils/utils');
require('colors');

const defaultPlayersCount = 4;

class TournamentManager {
  constructor(playersCount = defaultPlayersCount, gridType = 'olympic') {
    this.playersCount = playersCountValidation(playersCount);
    this.gridType = gridType.toLowerCase();
  }

  startTournament() {
    clear();
    const grid = new Grid(this.gridType);
    const tournamentState = new TournamentState(readNames(this.playersCount));

    while (!tournamentState.gameOver || (this.gridType === 'losers' && !tournamentState.losersOver)) {
      grid.show(tournamentState);

      console.log('\nPlease enter the winner\'s name in any relevant game'.bgCyan);
      tournamentState.addWinner(read());
    }

    const winner = tournamentState.sessions[tournamentState.sessions.length - 1][0].getWinner();
    const loser = tournamentState.losersSessions[tournamentState.losersSessions.length - 1][0].getWinner();

    if (this.gridType === 'losers') {
      clear();
      console.log(`${winner.red} vs ${loser.red}`.bgCyan);
      let absoluteWinner = null;
      console.log('Who won the final battle?');
      while (absoluteWinner !== winner && absoluteWinner !== loser) {
        absoluteWinner = read();
      }

      console.log(`Congratulations ${absoluteWinner.red}! He/she is the absolute champion!`.bgCyan);
    } else {
      grid.show(tournamentState);
      console.log(`\nWinner: ${winner.red}!`.bgCyan);
    }
  }
}


module.exports = TournamentManager;
