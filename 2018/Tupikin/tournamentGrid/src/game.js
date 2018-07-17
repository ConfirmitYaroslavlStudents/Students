/* eslint-disable curly */
class Game {
  constructor(firstPlayer, secondPlayer) {
    this.firstPlayer = firstPlayer || '?';
    this.secondPlayer = secondPlayer || '?';
    this.winner = '?';
  }

  includes(name) {
    return (this.firstPlayer === name || this.secondPlayer === name) &&
           (this.firstPlayer !== '?' && this.secondPlayer !== '?');
  }

  newPlayer(playerName) {
    if (this.firstPlayer === '?')
      this.firstPlayer = playerName;
    else
      this.secondPlayer = playerName;
  }

  setWinner(winner) {
    this.winner = winner;
  }

  winnerExist() {
    return this.winner !== '?';
  }
}

module.exports.Game = Game;
