/*!
 * expand-front-matter <https://github.com/jonschlinkert/expand-front-matter>
 *
 * Copyright (c) 2015, Jon Schlinkert.
 * Licensed under the MIT License.
 */

'use strict';

var isObject = require('isobject');
var isValid = require('is-valid-app');
var extend = require('extend-shallow');
var merge = require('mixin-deep');
var expand = require('expand');

module.exports = function(app, locals) {
  if (!isObject(app) || (!app.isApp && !app.isCollection)) {
    locals = app;
    app = {cache: {}, options: {}};
  }

  var data = extend({}, app.cache.data, locals);
  var merged = {};
  var opts = {};

  return function fn(file, next) {
    if (!isValid(file, 'expand-front-matter', ['view', 'file'])) {
      if (this.options.expandData === false) {
        return;
      }

      if (this.isCollection && !merged[this._name]) {
        merged[this._name] = {
          data: extend({}, this.cache.data),
          opts: extend({}, this.options)
        };
      } else if (this.isApp && !merged.app) {
        merged.app = {
          opts: extend({}, this.options, opts),
          data: merge({}, this.cache.data, data)
        };
      }
      return fn;
    }

    var type = file.options.collection;
    var appData = merged.app ? merged.app.data : {};
    var typeData = merged[type] ? merged[type].data : {};
    var mergedData = merge({}, appData, typeData, data);

    if (file.options.expandData !== false) {
      file.data = expand(opts)(file.data, mergedData);
    }
  };

  if (typeof next === 'function') {
    next();
  }
};

module.exports.middleware = function(app, locals) {
  if (!isObject(app) || (!app.isApp && !app.isCollection)) {
    locals = app;
    app = {cache: {}, options: {}};
  }

  var data = merge({}, app.cache.data, locals);

  return function(file, next) {
    var opts = extend({}, app.options, file.options);

    if (opts.expandData !== false) {
      file.data = expand(opts)(file.data, data);
    }
    next();
  };
};
