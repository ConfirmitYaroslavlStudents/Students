/* eslint-disable curly */
const clear = require('clear');
const { Game } = require('./game');

const cornerDown = '╗';
const verticalLine = '║';
const cornerUp = '╝';

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
    const grid = this.__fillGrid();
    grid[0] = grid[0].slice(0, grid[0].length - 1);
    for (let x = 0; x < grid[0].length; x++) {
      for (let y = 0; y < grid.length; y++)
        process.stdout.write(grid[y][x]);
      process.stdout.write('\n');
    }
    if (!this.isFinal)
      console.log('\nEnter name of winner');
    else
      console.log(`\nWinner: ${this.games[this.games.length - 1][0].winner.trim()}!`);
  }

  addWinner(winner) {
    winner += ' '.repeat(this.namesLength - winner.length + 1);
    for (let i = this.games.length - 1; i >= 0; i--) {
      for (let j = this.games[i].length - 1; j >= 0; j--) {
        const game = this.games[i][j];
        if (game.includes(winner) && !game.winnerExist()) {
          game.setWinner(winner);
          this.isFinal = this.isLastGame(i);
          if (this.isFinal) break;
          this.games[i + 1][Math.floor(j / 2)].newPlayer(winner, j);
          break;
        }
      }
    }
  }

  isLastGame(pointer) {
    return pointer === this.games.length - 1;
  }

  __fillGrid() {
    const grid = [];
    const emptyString = ' '.repeat(this.namesLength + 2);
    const verticalString = ' '.repeat(this.namesLength + 1) + verticalLine;

    for (let g = 0; g < this.games.length; g++) {
      grid[g] = [];
      const offset = g ? g * 2 - 1 : 0;
      for (let i = 0; i < offset; i++)
        grid[g].push(emptyString);
      for (const game of this.games[g]) {
        grid[g].push(game.firstPlayer + cornerDown);
        for (let i = 0; i < offset * 2 + 1; i++)
          grid[g].push(verticalString);
        grid[g].push(game.secondPlayer + cornerUp);
        for (let i = 0; i < offset * 2 + 1; i++)
          grid[g].push(emptyString);
      }
    }

    const winner = this.games.length;
    grid[winner] = [];

    for (let i = 0; i < this.players.length - 1; i++)
      grid[winner].push(emptyString);
    grid[winner].push(this.games[winner - 1][0].winner);
    for (let i = 0; i < this.players.length - 1; i++)
      grid[winner].push(emptyString);

    return grid;
  }

  __alignNamesLength() {
    for (const name of this.players)
      this.namesLength = Math.max(this.namesLength, name.length);

    for (let i = 0; i < this.players.length; i++)
      this.players[i] += ' '.repeat(this.namesLength - this.players[i].length + 1);
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
        this.games[i].push(new Game(this.namesLength));
    }

    for (let i = 0; i < this.players.length; i += 2) {
      const game = this.games[0][i / 2];
      game.newPlayer(this.players[i]);
      game.newPlayer(this.players[i + 1]);
    }
  }
}

module.exports.Grid = Grid;
