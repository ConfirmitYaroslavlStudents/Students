'use strict';

import fs from 'fs';

import Player from './Player';
import ViewsTypes from './views/ViewsTypes';
import BracketsTypes from './brackets/BracketsTypes';

import InvalidPlayersCountException from './exceptions/InvalidPlayersCountException';

const STATE_FILE_PATH = `${__dirname}/../state.json`;

export default class Tournament {
  constructor(bracketType, viewType) {
    this._gameOn = false;

    this._totalRounds = 0;
    this._playersList = [];

    this._viewType = viewType || ViewsTypes[Object.keys(ViewsTypes)[0]];
    this._bracketType = bracketType || BracketsTypes[Object.keys(BracketsTypes)[0]];
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
    return this.playersCount > 1 &&
           Math.log2(this.playersCount) % 1 === 0;
  }

  static hasSavedState() {
    try {
      fs.accessSync(STATE_FILE_PATH, fs.constants.R_OK);
      return true;
    } catch (e) {}

    return false;
  }

  saveCurrentSate() {
    if (this._tournamentBracket === null || !this._gameOn) {
      return;
    }

    const gameState = {
      viewType: this._viewType,
      bracketType: this._bracketType,
      bracket: this._tournamentBracket.state,
      players: this._playersList.map(player => {
        return {id: player.id, name: player.name};
      }),
    };

    fs.writeFileSync(STATE_FILE_PATH, JSON.stringify(gameState, null, '  '));
  }

  init() {
    if (!this.isValidPlayersCount()) {
      throw new InvalidPlayersCountException('Invalid players count.');
    }

    this._gameOn = true;
    this._totalRounds = Math.floor(Math.log2(this.playersCount));

    this._tournamentGridView = new ViewsTypes[this._viewType];
    this._tournamentBracket = new BracketsTypes[this._bracketType];

    this._shufflePlayers();
    this._tournamentBracket.init(this);
    this._tournamentGridView.init(this);
  }

  initFromSavedState() {
    if (!Tournament.hasSavedState()) {
      return;
    }

    const state = JSON.parse(fs.readFileSync(STATE_FILE_PATH));

    this._gameOn = true;
    this._viewType = state.viewType;
    this._bracketType = state.bracketType;

    this._tournamentGridView = new ViewsTypes[this._viewType];
    this._tournamentBracket = new BracketsTypes[this._bracketType];

    state.players.forEach(player =>
      this._playersList.push(new Player(player.id, player.name))
    );
    this._totalRounds = Math.floor(Math.log2(this.playersCount));

    this._tournamentBracket.init(this, state.bracket);
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
