/* eslint-disable curly */
const PlayersCountException = require('../exceptions/PlayersCountException');

function copy(o) {
  let output,
    v,
    key;
  output = Array.isArray(o) ? [] : {};
  for (key in o) {
    v = o[key];
    output[key] = (typeof v === 'object') ? copy(v) : v;
  }
  return output;
}

function shuffle(array) {
  for (let i = array.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1));
    [array[i], array[j]] = [array[j], array[i]];
  }
  return array;
}

function playersCountValidation(count) {
  if (Math.log2(count) % 1 === 0)
    return count;
  throw PlayersCountException('Players count must be a power of two');
}

module.exports = { shuffle, playersCountValidation, copy };
