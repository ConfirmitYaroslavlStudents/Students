'use strict';

import Match from './Match';

export default class TournamentGrid {
  constructor(playersList) {
    this._currentRoundIndex = 1;
    this._grid = [];

    this._totalRounds = Math.floor(Math.log2(playersList.length));
    this._playersCount = Math.pow(2, this._totalRounds);
    this._playersList = playersList.slice(0, this._playersCount);

    this._initGrid();
  }

  get grid() {
    return this._grid;
  }

  get totalRounds() {
    return this._totalRounds;
  }

  get currentRoundIndex() {
    return this._currentRoundIndex;
  }

  get playersCount() {
    return this._playersCount;
  }

  get winners() {
    return this._grid[this._grid.length - 1];
  }

  get currentMatch() {
    let
      currentRound = this._grid[this._currentRoundIndex - 1],
      currentMatch = currentRound.reduce((current, match) => {
        return match.winner !== null ?  current : current || match;
      }, false);

    if (currentMatch !== false) {
      return currentMatch;
    }

    if (this._currentRoundIndex === this._totalRounds) {
      return false;
    }

    this._initNextRound(currentRound);
    return this.currentMatch;
  }

  _initNextRound(previousRound) {
    let currentRound = this._grid[this._currentRoundIndex];
    this._currentRoundIndex++

    if (this._currentRoundIndex === this._totalRounds) {
      this._initLastRound(previousRound, currentRound);
      return;
    }

    currentRound = currentRound.map((match, index) => {
      let matchIndex = index * 2;
      return new Match(
        previousRound[matchIndex].winner,
        previousRound[matchIndex + 1].winner
      );
    });

    this._grid[this._currentRoundIndex - 1] = currentRound;
  }

  _initLastRound(previousRound, currentRound) {
    currentRound[0] = new Match(previousRound[0].loser, previousRound[1].loser);
    currentRound[1] = new Match(previousRound[0].winner, previousRound[1].winner);
  }

  _initGrid() {
    this._initEmptyGrid();
    this._shufflePlayers();

    this._grid[0] = this._grid[0].map((match, index) => {
      let playerIndex = index * 2;
      return new Match(...this._playersList.slice(playerIndex, playerIndex + 2));
    });
  }

  _initEmptyGrid() {
    for(let i = this._totalRounds - 1; i >= 0; i--) {
      this._grid.push(Array(Math.pow(2, i)).fill(null));
    }

    if (this._totalRounds > 1) {
      this._grid[this._grid.length - 1].push(null);
    }
  }

  _shufflePlayers() {
    let
      players = this._playersList,
      lastIndex = this._playersCount;

    while (lastIndex) {
      let newIndex = Math.floor(Math.random() * lastIndex--);
      [players[lastIndex], players[newIndex]] = [players[newIndex], players[lastIndex]];
    }
  }
}
