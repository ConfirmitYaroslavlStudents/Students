const clear = require('clear');
const colors = require('colors');
const { readNames, read } = require('../scanner/scanner');
const TournamentState = require('./tournamentState');
const Grid = require('./grid');
const { playersCountValidation } = require('../utils/utils');

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

    while (!tournamentState.gameOver) {
      grid.show(tournamentState);

      console.log(tournamentState.sessions);
      console.log();
      console.log(tournamentState.losersSessions);

      console.log('\nPlease enter the winner\'s name in any relevant game'.bgCyan);
      tournamentState.addWinner(read());
    }

    const winner = tournamentState.sessions[tournamentState.sessions.length - 1][0].getWinner();

    grid.show(tournamentState);
    console.log(`\nWinner: ${winner.red}!`.bgCyan);
  }
}


module.exports = TournamentManager;
