'use strict';

import Match from './../Match';
import InvalidBracketsData from './../exceptions/InvalidBracketsData';

export default class SingleEliminationBracket {
  constructor() {
    this._raw = [];
    this._tournament = null;
    this._currentRoundIndex = 0;
  }

  get raw() {
    return [this._raw];
  }

  get gameOver() {
    return this._raw[this._raw.length - 1][0] !== null &&
           this._raw[this._raw.length - 1][0].gameOver;
  }

  get firstPlace() {
    return this._raw[this._raw.length - 1][0].winner;
  }

  get secondPlace() {
    return this._raw[this._raw.length - 1][0].loser;
  }

  get currentRoundIndex() {
    return this._currentRoundIndex + 1;
  }

  get currentMatch() {
    let roundIndex = this._currentRoundIndex;

    for (let i = 0; i < this._raw[roundIndex].length; i++) {
      if (!this._raw[roundIndex][i].gameOver) {
        return this._raw[roundIndex][i];
      }
    }

    return null;
  }

  set currentMatchWinner(player) {
    if (this.currentMatch === null) {
      return;
    }

    this.currentMatch.setWinnerById(player.id);
    this._selectNextMatch();

    this._tournament.saveCurrentSate();
  }

  get state() {
    return {
      raw: this._raw.map(round =>
        round.map(match => match ? match.state : null)
      ),
      currentRoundIndex: this._currentRoundIndex
    };
  }

  init(tournament, state) {
    this._tournament = tournament;

    if (state) {
      this._loadCurrentState(state);
      return;
    }

    this._initEmptyBracket();
    this._fillFirstRound();
  }

  _loadCurrentState(state) {
    const
      hasRawFileld = state.hasOwnProperty('raw'),
      hasCurrentRoundIndexFileld = state.hasOwnProperty('currentRoundIndex');

    if (!hasRawFileld || !hasCurrentRoundIndexFileld) {
      throw new InvalidBracketsData('Invalid state structure!');
    }

    if (!Array.isArray(state.raw)) {
      throw new InvalidBracketsData('Invalid raw structure!');
    }

    this._raw = this._formBracketFromData(state.raw);
    this._currentRoundIndex = state.currentRoundIndex;
  }

  _formBracketFromData(data) {
    return data.map(round => round.map(match => {
      if (match === null) {
        return match;
      }

      const winnerId = match.winner;

      match = new Match(...this._tournament.playersList.filter(player =>
        match.players.includes(player.id)
      ));

      if (winnerId !== null) {
        match.setWinnerById(winnerId);
      }

      return match;
    }));
  }


  _selectNextMatch() {
    let
      currentRound = this._raw[this._currentRoundIndex],
      currentMatch = currentRound.reduce((current, match) => {
        return match.gameOver ?  current : current || match;
      }, false);

    if (!currentRound[currentRound.length - 1].gameOver) {
      return;
    }

    if (currentRound.length !== 1) {
      this._initNextRound();
    }
  }

  _initNextRound() {
    let
      previousRound = this._raw[this._currentRoundIndex++],
      currentRound = this._raw[this._currentRoundIndex]

    currentRound = currentRound.map((match, index) => {
      return match || new Match(
        previousRound[index * 2].winner,
        previousRound[index * 2 + 1].winner
      );
    });

    this._raw[this._currentRoundIndex] = currentRound;
  }

  _initEmptyBracket() {
    for (let i = this._tournament.totalRounds - 1; i >= 0; i--) {
      this._raw.push(Array(Math.pow(2, i)).fill(null));
    }
  }

  _fillFirstRound() {
    let playersList = this._tournament.playersList;

    this._raw[0] = this._raw[0].map((match, index) => {
      let playerIndex = index * 2;
      return new Match(...playersList.slice(playerIndex, playerIndex + 2));
    });
  }
}
