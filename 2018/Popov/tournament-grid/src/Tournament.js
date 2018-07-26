'use strict';

import Player from './Player';

import InvalidPlayersCountException from './exceptions/InvalidPlayersCountException';

export default class Tournament {
  constructor(bracket, view) {
    this._gameOn = false;

    this._totalRounds = 0;
    this._playersList = [];

    this._tournamentBracket = bracket;
    this._tournamentGridView = view;
  }

  get gameOn() {
    return this._gameOn;
  }

  get gameOver() {
    return this._tournamentBracket.gameOver;
  }

  get bracket() {
    return this._tournamentBracket;
  }

  get view() {
    return this._tournamentGridView;
  }

  get totalRounds() {
    return this._totalRounds;
  }

  get playersList() {
    return this._playersList;
  }

  get playersCount() {
    return this._playersList.length;
  }

  addPlayer(name) {
    this._playersList.push(new Player(this.playersCount, name));
  }

  isValidPlayersCount() {
    return Math.log2(this.playersCount) % 1 === 0;
  }

  init() {
    if (!this.isValidPlayersCount()) {
      throw new InvalidPlayersCountException('Invalid players count.');
    }

    this._gameOn = true;
    this._totalRounds = Math.floor(Math.log2(this.playersCount));

    this._shufflePlayers();
    this._tournamentBracket.init(this);
    this._tournamentGridView.init(this);
  }

  _shufflePlayers() {
    let
      players = this._playersList,
      lastIndex = this._playersList.length;

    while (lastIndex) {
      let newIndex = Math.floor(Math.random() * lastIndex--);
      [players[lastIndex], players[newIndex]] = [players[newIndex], players[lastIndex]];
    }
  }
}
