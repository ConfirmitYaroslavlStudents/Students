'use strict';

import kuler from 'kuler';
import clear from 'clear';

import Match from './../Match';
import DefaultGridView from './DefaultGridView';

export default class FIFAGridView  extends DefaultGridView  {
  print() {
    if (this._tournament.playersCount <= 4) {
      super.print();
      return;
    }

    clear();
    console.log(kuler('Tournament grid: \n', '#fff'));

    this._tournament.bracket.raw.forEach((bracket, index) => {
      this._columnOffsetFix = 0;
      this._clearGrid((bracket[0] || [0]).length * 2 - 1);

      if (index === 0) {
        this._prepareLeftPart(bracket);
        this._prepareCenterPart(bracket);
        this._prepareRightPart(bracket);
      }
      else { // TODO
        this._clearGrid((bracket[0] || [0]).length * 4 - 1);
        bracket.forEach(this._fillRoundColumn.bind(this, bracket));
      }

      console.log(this._rawOutputGrid.join('\n'));
      console.log(`\n`.repeat(this._gridMarginBottom));
    })
  }

  _clearGrid(height) {
    if (this._tournament.playersCount <= 4) {
      super._clearGrid(height);
      return;
    }
    super._clearGrid((height + 1) / 2 - 1);
  }

  _prepareLeftPart(bracket) {
    let roundsCount = bracket.length;
    if (roundsCount > 0) {
      bracket.slice(0, roundsCount - 1).forEach((round, index) => {
        this._fillRoundColumn(bracket, round.slice(0, round.length / 2), index);
      });
    }
  }

  _prepareCenterPart(bracket) {
    let roundsCount = bracket.length;
    if (roundsCount > 0) {
      this._fillRoundColumn(bracket, bracket[roundsCount - 1], Math.floor(roundsCount / 2));
    }
  }

  _prepareRightPart(bracket) {
    let roundsCount = bracket.length;
    if (roundsCount > 0) {
      bracket.slice(0, roundsCount - 1).reverse().forEach((round, index) => {
        this._fillRoundColumn(
          bracket, round.slice(round.length / 2), Math.floor(roundsCount / 2) - index
        );
      });
    }
  }
}
