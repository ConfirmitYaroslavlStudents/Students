function IncorrectNumberOfPlayersException(message){
    this.message = message || 'Incorrect number of players. It should a power of 2.';
    this.name = 'IncorrectNumberOfPlayersException';
    this.stack = (new Error()).stack;
}

module.exports = {IncorrectNumberOfPlayersException}