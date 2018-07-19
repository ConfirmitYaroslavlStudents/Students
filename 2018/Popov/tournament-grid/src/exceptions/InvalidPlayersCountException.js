'use strict';

export default class InvalidPlayersCountException extends Error {
    constructor(...args) {
      super(...args);
      this.name = this.constructor.name;
    }
}
