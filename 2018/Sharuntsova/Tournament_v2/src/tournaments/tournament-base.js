var EventEmitter = require('events');


class TournamentBase extends EventEmitter {

    constructor(configuration) {
        super();

        this.players = configuration.players;

        this.numberOfPlayers = configuration.players.length;
        this.numberOfRounds = Math.log2(this.numberOfPlayers);;

        this.gridType = configuration.gridType;
        this.currentRound = configuration.tournamentState.currentRound;
        this.currentGame = configuration.tournamentState.currentGame;

    }

    onStateChanged(newState) {
        //console.log("X1" + JSON.stringify(newState));
        this.emit('state-changed', newState);
    }
}


module.exports = {
    TournamentBase,
}