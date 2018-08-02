'use strict';

export default class InvalidParameterException extends Error {
    constructor(...args) {
      super(...args);
      this.name = this.constructor.name;
    }
}
