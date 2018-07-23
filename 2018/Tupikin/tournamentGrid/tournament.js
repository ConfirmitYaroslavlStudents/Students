/* eslint-disable curly */
const clear = require('clear');
const readName = require('readline-sync').question;
const { Grid } = require('./src/grid');
const { PlayersCountException } = require('./src/exceptions');

class Tournament {
  constructor(playersCount) {
    this.playersCount = playersCount || 4;
    this.grid = null;
  }

  startTournament() {
    while (!this.grid.isFinal) {
      this.grid.show();
      this.grid.addWinner(readName());
    }
    this.grid.show();
  }

  enterNames() {
    clear();

    const numberOfMatches = Math.log2(this.playersCount);
    if (numberOfMatches !== Math.ceil(numberOfMatches))
      throw PlayersCountException('Players count is wrong. Enter the number of players multiple of a power of two');

    const players = [];
    console.log('Enter the name of player');
    while (players.length < this.playersCount) {
      const name = readName();
      if (!players.includes(name))
        players.push(name);
    }
    this.grid = new Grid(players, numberOfMatches);
  }
}

const playersCount = process.argv.length === 3 ? process.argv[2] : undefined;

const tournament = new Tournament(playersCount);
tournament.enterNames();
tournament.startTournament();
