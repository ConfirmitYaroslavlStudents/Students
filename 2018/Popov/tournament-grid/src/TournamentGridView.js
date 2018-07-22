'use strict';

import Match from './Match';

export default class TournamentGridView {
  constructor(tournament) {
    this._rawOutputGrid = [];

    this._matchBoxHeight = 6;
    this._columnRightMargin = 5;

    this._tournament = tournament;
    this._emptyMatchTemplate = new Match({name: '?'}, {name: '?'});
  }

  print() {
    let
      roundTopOffset = 0,
      verticalColumnOffset = 0;

    this._clearGrid();
    this._tournament.grid.grid.forEach((round, roundIndex) => {
      roundTopOffset = roundIndex === 1 ? 3 : 2 * roundTopOffset;
      verticalColumnOffset += roundTopOffset;

      this._prepareRoundColumnView(round, roundIndex, verticalColumnOffset);
    });

    console.clear();
    console.log('Tournament grid:');
    console.log(this._rawOutputGrid.join('\n'));
  }

  _clearGrid() {
    let
      matchBoxWidth = this._minPlayerNameLength() + 4,
      emptyColumnWidth = matchBoxWidth + this._columnRightMargin,
      emptyLineWidth = emptyColumnWidth * this._tournament.grid.totalRounds,
      emptyLineTemplate = ' '.padEnd(emptyLineWidth);

    this._rawOutputGrid = Array(this._gridHeight()).fill(emptyLineTemplate);
  }

  _minPlayerNameLength() {
    return this._tournament.playersList.reduce((length, player) => {
      return player.name.length > length ? player.name.length : length;
    }, 0);
  }

  _gridHeight() {
    return (this._tournament.grid.playersCount / 2 * this._matchBoxHeight);
  }

  _prepareMatchBoxView(currentMatch) {
    let matchBox = currentMatch || this._emptyMatchTemplate;
    return matchBox.toString(this._minPlayerNameLength()).split('\n');
  }

  _prepareRoundColumnView(currentRound, roundIndex, columnTopOffset) {
    let roundMatchesOffset = 0;

    if (roundIndex < this._tournament.grid.totalRounds - 1) {
      roundMatchesOffset = Math.floor((
        (this._gridHeight() - 1 - columnTopOffset * 2) - (this._matchBoxHeight - 1) * currentRound.length
      ) / (currentRound.length - 1));
    }

    currentRound.forEach(match => {
      this._prepareMatchBoxView(match).forEach(playerLine => {
          let line = this._rawOutputGrid[++columnTopOffset % this._gridHeight()];
          let start = (this._columnRightMargin + this._minPlayerNameLength() + 4) * roundIndex;
          this._rawOutputGrid[columnTopOffset] = line.slice(0, start) + playerLine + line.slice(start);
      });
      columnTopOffset += roundMatchesOffset;
    });
  }
}
