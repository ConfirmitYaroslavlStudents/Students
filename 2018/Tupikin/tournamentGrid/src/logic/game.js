/* eslint-disable curly */
class Game {
  constructor() {
    this.firstPlayer = null;
    this.secondPlayer = null;

    this.winner = null;
  }

  addPlayer(name, gameNumber) {
    gameNumber = gameNumber || 0;
    if (!this.firstPlayer && gameNumber % 2 === 0)
      this.firstPlayer = name;
    else
      this.secondPlayer = name;
  }

  getFirst() {
    return this.firstPlayer ? this.firstPlayer : '?';
  }

  getSecond() {
    return this.secondPlayer ? this.secondPlayer : '?';
  }

  getWinner() {
    return this.winner ? this.winner : '?';
  }

  getLoser() {
    return this.getWinner() === this.firstPlayer ? this.secondPlayer : this.firstPlayer;
  }

  includes(name) {
    return (this.firstPlayer === name || this.secondPlayer === name) && (name !== null);
  }

  isFull() {
    return this.firstPlayer && this.secondPlayer;
  }

  setWinner(name) {
    this.winner = name;
  }

  winnerExist() {
    return !!this.winner;
  }
}

module.exports = Game;
