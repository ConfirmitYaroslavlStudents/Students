'use strict';

import kuler from 'kuler';
import Match from './../Match';

export default class TournamentGridView {
  constructor() {
    this._tournament = null;
    this._rawOutputGrid = [];

    this._rightMargin = 4;
    this._columnOffsetFix = 0;
    this._gridMarginBottom = 1;
    this._namesSeparator = ` / `;
    this._emptyMatchTemplate = new Match({name: '?'}, {name: '?'});
  }

  init(tournament) {
    this._tournament = tournament;
  }

  print() {
    console.clear();
    console.log(kuler('Tournament grid: \n', '#fff'));

    this._tournament.bracket.raw.forEach(bracket => {
      this._columnOffsetFix = 0;
      this._clearGrid((bracket[0] || [0]).length * 2 - 1);

      bracket.forEach(this._fillRoundColumn.bind(this, bracket));

      console.log(this._rawOutputGrid.join('\n'));
      console.log(`\n`.repeat(this._gridMarginBottom));
    })
  }

  _clearGrid(height) {
    this._rawOutputGrid = Array(height).fill('');
  }

  _fillRoundColumn(bracket, round, roundIndex) {
    if (roundIndex !== 0) {
      let
        currentRoundLength = bracket[roundIndex].length,
        previousRoundLength = bracket[roundIndex - 1].length;

      this._columnOffsetFix += currentRoundLength ===  previousRoundLength ? 1 : 0;
    }

    let
      fixedIndex = roundIndex - this._columnOffsetFix,
      columnTopOffset = Math.pow(2, fixedIndex) - 1,
      matchBottomMargin = Math.pow(2, fixedIndex + 1) - 1;

    this._fillEmptyLines(0, columnTopOffset);

    let currentLine = columnTopOffset;
    round.forEach((match, index) => {
      if (index !== 0) {
        this._fillEmptyLines(currentLine, matchBottomMargin);
        currentLine += matchBottomMargin;
      }

      this._rawOutputGrid[currentLine++] += this._getMatchLine(match);
    });

    this._fillEmptyLines(currentLine, columnTopOffset);
  }

  _fillEmptyLines(fromLine, count) {
    for (var i = 0; i < count; i++) {
      this._rawOutputGrid[fromLine + i] += this._getEmptyLine();
    }
  }

  _getEmptyLine() {
    let
      namesLength = this._minPlayerNameLength() * 2,
      separatorLength = this._namesSeparator.length;

    return ' '.padEnd(namesLength + separatorLength + this._rightMargin);
  }

  _getMatchLine(match) {
    match = match || this._emptyMatchTemplate;

    let
      rightMargin = ' '.padEnd(this._rightMargin),
      firstPlayer = this._preparePlayerName(match.players[0].name),
      secondPlayer = this._preparePlayerName(match.players[1].name, false);

    if (match.gameOver) {
      firstPlayer = match.players[0].id === match.winner.id ?
        kuler(firstPlayer, 'green') : kuler(firstPlayer, 'red');

      secondPlayer = match.players[1].id === match.winner.id ?
        kuler(secondPlayer, 'green') : kuler(secondPlayer, 'red');
    }

    return `${firstPlayer}${this._namesSeparator}${secondPlayer}${rightMargin}`;
  }

  _preparePlayerName(name, firstPlayer = true) {
    let
      length = this._minPlayerNameLength(),
      padding = Math.floor((length - name.length) / 2);

    return firstPlayer ? name.padEnd(padding).padStart(length) :
                         name.padStart(padding).padEnd(length);
  }

  _minPlayerNameLength() {
    return this._tournament.playersList.reduce((length, player) => {
      return player.name.length > length ? player.name.length : length;
    }, 0);
  }
}
