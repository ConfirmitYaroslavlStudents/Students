const lodash = require('lodash');
const errorHandler = require('../../');
// const debug = require('debug')('kubide:errorhandler:parser:config');


errorHandler.addParser((err, conf) => {
  if (typeof err !== 'string') {
    return err;
  }

  const ret = lodash.get(conf.errorhandler, err, conf.errorhandler.default);

  ret.code = err;
  return ret;
});
