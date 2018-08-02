'use strict';

import Player from './Player';
import TournamentGrid from './TournamentGrid';
import TournamentGridView from './TournamentGridView';

import InvalidPlayersCountException from './exceptions/InvalidPlayersCountException';

export default class Tournament {
  constructor() {
    this._playersList = [];
    this._playersCount = 0;
    this._tournamentGrid = null;
    this._tournamentGridView = null;
  }

  get grid() {
    return this._tournamentGrid;
  }

  get view() {
    return this._tournamentGridView;
  }

  get playersList() {
    return this._playersList;
  }

  get playersCount() {
    return this._playersCount;
  }

  addPlayer(name) {
    let player = new Player(this._playersCount, name);

    this._playersCount++;
    this._playersList.push(player);
  }

  isValidPlayersCount() {
    return Math.log2(this._playersCount) % 1 === 0;
  }

  run() {
    if (!this.isValidPlayersCount()) {
      throw new InvalidPlayersCountException('Invalid players count.');
    }

    this._tournamentGridView = new TournamentGridView(this);
    this._tournamentGrid = new TournamentGrid(this._playersList);
  }
}
