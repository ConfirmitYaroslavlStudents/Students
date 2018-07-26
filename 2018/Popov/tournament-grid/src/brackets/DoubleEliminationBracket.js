'use strict';

import Match from './../Match';
import SingleEliminationBracket from './SingleEliminationBracket';

export default class DoubleEliminationBracket extends SingleEliminationBracket {
  constructor() {
    super();

    this._lBracket = null;
    this._wBracket = null;
    this._finalBracket = null;
    this._wBracketCurrentPosition = 1;
  }

  get raw() {
    return [
      this._wBracket || this._raw,
      this._finalBracket || [],
      this._lBracket || []
    ];
  }

  get gameOver() {
    return this._finalBracket !== null &&
           this._finalBracket[0][0].gameOver;
  }

  _selectNextMatch() {
    let currentRound = this._raw[this.currentRoundIndex - 1];
    if (currentRound.length !== 1) {
      super._selectNextMatch();
      return
    }

    if (this._lBracket !== null) {
      if (this._finalBracket === null) {
        this._initFinalRounds();
      }
      return;
    }

    this._currentRoundIndex = 0;
    this._wBracket = this._raw.slice();

    this._generateLosersBracket();
    this._raw = this._lBracket;
  }

  _initFinalRounds() {
    this._finalBracket =[[new Match(
      this._wBracket[this._wBracket.length - 1][0].winner,
      this._lBracket[this._lBracket.length - 1][0].winner
    )]];

    this._currentRoundIndex = 0;
    this._raw = this._finalBracket;
  }

  _initNextRound() {
    if (this._wBracket !== null) {
      let previousRound = this._lBracket[this._currentRoundIndex];
      this._lBracket.push(Array(previousRound.length / 2).fill(null));
    }

    if (this._wBracket !== null && !(this._currentRoundIndex % 2)) {
      let wRound = this._wBracket[this._wBracketCurrentPosition++];

      for (let i = 0; i < wRound.length; i += 2) {
        this._lBracket[this._currentRoundIndex + 1].push(
          new Match(wRound[i].loser, wRound[i + 1].loser)
        );
      }
    }

    super._initNextRound();
  }

  _generateLosersBracket() {
    this._lBracket = [[]];
    for (let i = 0; i < this._wBracket[0].length; i += 2) {
      this._lBracket[0].push(
        new Match(this._wBracket[0][i].loser, this._wBracket[0][i + 1].loser)
      );
    }
  }
}
