const readLine = require('readline-sync');
const {GridTypes, TournamentTypes} = require('./src/tournament-types')
const {Engine} = require('./src/tournament-engine');
const {ConfigurationManager} = require('./src/config-manager')
const {ConfigBuilder} = require('./src/config-builder')
const tournamentFactory = require('./src/tournament-factory.js')


const configurationManager = new ConfigurationManager();

let config;


if (configurationManager.exists()) {

    if (readLine.keyInYN('A previous game has been found. Do you want to continue?')) {
        config = configurationManager.load();
    }
    else
    {
        config = new ConfigBuilder().build();
    }
}
else
{
    config = new ConfigBuilder().build();
}

const tournament = tournamentFactory.createTournament(config);

tournament.addListener('state-changed', (newState) => {
    //console.log("X2" + JSON.stringify(newState));
    configurationManager.save(Object.assign({}, config, {tournamentState: Object.assign({}, config.tournamentState, newState)}));
})

tournament.play();




return;



