/* eslint-disable curly */
class Game {
  constructor(nameLength) {
    this.firstPlayer = `${' '.repeat(nameLength)}?`;
    this.secondPlayer = `${' '.repeat(nameLength)}?`;
    this.winner = '?';
  }

  includes(name) {
    return (this.firstPlayer === name || this.secondPlayer === name) &&
           (this.firstPlayer.trim() !== '?' && this.secondPlayer.trim() !== '?');
  }

  newPlayer(playerName, gameNumber) {
    if (gameNumber) {
      if (gameNumber % 2 === 0)
        this.firstPlayer = playerName;
      else
        this.secondPlayer = playerName;
      return;
    }
    if (this.firstPlayer.trim() === '?')
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
