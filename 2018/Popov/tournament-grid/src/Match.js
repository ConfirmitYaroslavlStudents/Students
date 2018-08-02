'use strict';

import style from 'ansi-styles';
import Player from './Player';

import InvalidParameterException from './exceptions/InvalidParameterException';

export default class Match {
  constructor(...players) {
    if (players.length !== 2) {
      throw new InvalidParameterException(`Invalid players count: ${players.length}.`);
    }

    this._loser = null;
    this._winner = null;
    this._players = players;
  }

  get loser() {
    return this._loser;
  }

  get winner() {
    return this._winner;
  }

  get players() {
    return this._players;
  }

  setWinnerById(id) {
    if (id >= this._players.length) {
      throw new InvalidParameterException(`Invalid player id.`);
    }

    this._winner = this._players[id];
    this._loser = this._players[1 - id];
  }

  toString(lengthAdjust) {
    lengthAdjust = this._players.reduce((length, player) => {
      return player.name.length > length ? player.name.length : length;
    }, lengthAdjust);

    let
      boxTextLine = `+ ` + '-'.repeat(lengthAdjust) + ` +`,
      firstPlayerName = this._players[0].name.padEnd(lengthAdjust, ` `),
      secondPlayerName = this._players[1].name.padEnd(lengthAdjust, ` `);

    return `${boxTextLine}\n| ${firstPlayerName} |\n` +
           `${boxTextLine}\n| ${secondPlayerName} |\n${boxTextLine}`;
  }
}
