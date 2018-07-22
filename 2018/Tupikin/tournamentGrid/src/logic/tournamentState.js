/* eslint-disable curly */
const Game = require('./game');
const { shuffle, copy } = require('../utils/utils');

class TournamentState {
  constructor(playersNames) {
    this.names = playersNames;

    this.sessions = this._initSession();
    this.losersSessions = this._initLosersSession();

    this.gameOver = false;
  }

  addWinner(name) {
    for (let i = this.sessions.length - 1; i >= 0; i--) {
      for (let j = this.sessions[i].length - 1; j >= 0; j--) {
        const game = this.sessions[i][j];
        const loser = this.losersSessions[i];

        if (game.includes(name) && game.isFull() && !game.winnerExist()) {
          game.setWinner(name);
          loser[Math.floor(j / 2)].addPlayer(game.getLoser());
          this.isLastGame(i);
          if (!this.gameOver)
            this.sessions[i + 1][Math.floor(j / 2)].addPlayer(name, j);
          // todo check losers sets
          return;
        }
      }
    }

    
    // Сюда попадем если только не найден победитель в основной игре
    // Поэтому ищем победителя в сетке проигравших
  }

  isLastGame(pointer) {
    this.gameOver = pointer === this.sessions.length - 1;
    return this.gameOver;
  }

  _initSession() {
    const sessions = [];
    const sessionsCount = Math.log2(this.names.length);

    this.names = shuffle(this.names);

    for (let i = 0; i < sessionsCount; i++) {
      sessions[i] = [];
      const gamesInThisSession = this.names.length / (2 ** (i + 1));
      for (let j = 0; j < gamesInThisSession; j++)
        sessions[i][j] = new Game();
    }

    for (let i = 0; i < this.names.length; i += 2) {
      const game = sessions[0][i / 2];
      game.addPlayer(this.names[i]);
      game.addPlayer(this.names[i + 1]);
    }

    return sessions;
  }

  _initLosersSession() {
    const sessions = copy(this.sessions);
    sessions[0] = sessions[0].slice(0, sessions[0].length / 2);

    for (let i = 0; i < sessions.length; i++)
      for (let j = 0; j < sessions[i].length; j++)
        sessions[i][j] = new Game();

    return sessions;
  }
}

module.exports = TournamentState;
