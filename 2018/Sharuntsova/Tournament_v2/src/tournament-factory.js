const {TournamentTypes} = require('./tournament-types.js');
const {SingleEliminationTournament} = require('./tournaments/single-elimination-tournament');

module.exports = {
    createTournament: function (config) {
        switch (config.tournamentType) {
            case TournamentTypes.SingleElimination:
                return new SingleEliminationTournament(config);
                break;
            case TournamentTypes.DoubleElimination:
                throw new Error("Not supported type(")
                //return new DoubleElimination(config)
                break;
            default:
                throw  new Error("Not supported type")
        }
    }
}