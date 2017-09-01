const errorHandler = require('../../');
// const debug = require('debug')('kubide:errorhandler:parser:mongoose');


errorHandler.addParser((err, conf) => {
  if (!(err instanceof Error)) {
    return err;
  }

  const parsedError = conf.errorhandler.http.internalError;
  parsedError.message = err.message;
  parsedError.stack = err.stack;

  return parsedError;
});
