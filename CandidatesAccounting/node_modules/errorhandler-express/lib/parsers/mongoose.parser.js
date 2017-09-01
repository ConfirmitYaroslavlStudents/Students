const lodash = require('lodash');
const errorHandler = require('../../');
const mongoose = require('mongoose');
// const debug = require('debug')('kubide:errorhandler:parser:mongoose');


errorHandler.addParser((err) => {
  if (!(err instanceof mongoose.Error.ValidationError)) {
    return err;
  }

  const parsedError = {
    code: 'mongoose.validation.error',
    status: 400,
    message: err.message,
    detail: [],
  };

  lodash.each(err.errors, (error, index) => {
    parsedError.detail.push(err.errors[index]);
  });

  return parsedError;
});
