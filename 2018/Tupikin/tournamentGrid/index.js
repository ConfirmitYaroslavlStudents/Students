const { argv } = require('optimist');
const TournamentManager = require('./src/logic/tournamentManager');

const tournament = new TournamentManager(argv.players, argv.grid, argv.losers);
tournament.startTournament();
