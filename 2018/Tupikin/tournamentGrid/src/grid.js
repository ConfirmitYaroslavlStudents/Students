/* eslint-disable curly */
const clear = require('clear');
const { Game } = require('./game');

class Grid {
  constructor(players, numberOfMatches) {
    this.players = players;
    this.numberOfMatches = numberOfMatches;
    this.namesLength = 0;
    this.games = [];
    this.isFinal = false;

    this.__alignNamesLength();
    this.__shuffleNames();
    this.__fillGames();
  }

  show() {
    clear();
    console.log('The tournament table at the moment:\n');

    if (!this.isFinal) console.log('\nEnter name of winner');
  }

  addWinner(winner) {
    for (let i = this.games.length - 1; i >= 0; i--) {
      for (let j = this.games[i].length - 1; j >= 0; j--) {
        const game = this.games[i][j];
        if (game.includes(winner) && !game.winnerExist()) {
          game.setWinner(winner);
          this.isFinal = this.isLastGame(i);
          if (this.isFinal) break;
          this.games[i + 1][Math.floor(j / 2)].newPlayer(winner);
          break;
        }
      }
    }
  }

  isLastGame(pointer) {
    return pointer === this.games.length - 1;
  }

  __alignNamesLength() {
    for (const name of this.players)
      this.namesLength = Math.max(this.namesLength, name.length);

    for (let name of this.players)
      name += ' '.repeat(this.namesLength - name.length);
  }

  __shuffleNames() {
    for (let i = this.players.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [this.players[i], this.players[j]] = [this.players[j], this.players[i]];
    }
  }

  __fillGames() {
    for (let i = 0; i < this.numberOfMatches; i++) {
      this.games[i] = [];
      const gamesOfThisSeason = this.players.length / (2 * (i + 1));
      for (let j = 0; j < gamesOfThisSeason; j++)
        this.games[i].push(new Game());
    }

    for (let i = 0; i < this.players.length; i += 2) {
      const game = this.games[0][i / 2];
      game.newPlayer(this.players[i]);
      game.newPlayer(this.players[i + 1]);
    }
  }
}

module.exports.Grid = Grid;
