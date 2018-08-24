'use strict';

export default class InvalidBracketsData extends Error {
    constructor(...args) {
      super(...args);
      this.name = this.constructor.name;
    }
}
