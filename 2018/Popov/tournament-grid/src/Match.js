'use strict';

import kuler from 'kuler';
import Player from './Player';

import InvalidParameterException from './exceptions/InvalidParameterException';

export default class Match {
  constructor(...players) {
    if (players.length !== 2) {
      throw new InvalidParameterException(`Invalid players count: ${players.length}.`);
    }

    this._winner = null;
    this._losers = [null];
    this._players = players;
  }

  get loser() {
    return this._losers[0];
  }

  get winner() {
    return this._winner;
  }

  get gameOver() {
    return this._winner !== null;
  }

  get players() {
    return this._players;
  }

  get state() {
    return {
      winner: this._winner ? this._winner.id : null,
      players: this._players.map(player => player.id)
    }
  }

  setWinnerById(playerId) {
    let winner = this._players.find(player => player.id === playerId);

    if (winner === undefined) {
      throw new InvalidParameterException(`Invalid player id.`);
    }

    this._winner = winner;
    this._losers = this._players.filter(player => player.id !== playerId);
  }
}
