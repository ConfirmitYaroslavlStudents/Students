'use strict';

import InvalidParameterException from './exceptions/InvalidParameterException';

export default class Player {
  constructor(id, name = '') {
    if (!Number.isInteger(id)) {
      throw new InvalidParameterException('Invalid player id.');
    }

    if (String(name).length === 0) {
      throw new InvalidParameterException('Invalid player name.');
    }

    this._id = id;
    this._name = name;
  }

  get id() {
    return this._id;
  }

  get name() {
    return this._name;
  }
}
