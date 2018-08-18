const readLine = require('readline-sync');
const utils = require('./utils');
const {TournamentTypes, GridTypes} = require('./tournament-types')

class ConfigBuilder {

    build() {

        const tournamentType = this.chooseTournamentType();
        const gridType = (tournamentType === TournamentTypes.DoubleElimination) ?
            GridTypes.OlympicsWithLosers :
            this.chooseGridType();

        const players = this.inputPlayersNames();

        return {
            gridType,
            tournamentType,
            players,
            tournamentState: {
                currentRound: 0,
                currentGame: 0,
                upperBracket: []
            }
        }
    }

    chooseTournamentType() {
        const tournamentType = utils.makeSelectRequest('Choose type of tournament: ', ['Single-elimination', 'Double-elimination']);

        switch (tournamentType) {
            case 'Single-elimination':
                return TournamentTypes.SingleElimination;

            case 'Double-elimination':
                return TournamentTypes.DoubleElimination;
        }
    }

    chooseGridType() {
        const typeOfGrid = utils.makeSelectRequest('Choose type of grid:', ['Olympics', 'Double-bracket']);
        switch (typeOfGrid) {
            case 'Olympics':
                return GridTypes.Olympics;
            case 'Double-bracket':
                return GridTypes.DoubleBracket;
        }
    }

    inputPlayersNames() {
        let numberOfPlayers = NaN;
        while (isNaN(numberOfPlayers)) {
            numberOfPlayers = readLine.question('Input the number of players: ');
        }

        const numberOfRounds = Math.log2(numberOfPlayers);

        if (numberOfRounds !== Math.ceil(numberOfRounds))
            throw new Error("IncorrectNumberOfPlayers");

        const players = [];

        console.log('Input the names of players:');
        let i = 1;
        while (i <= numberOfPlayers) {
            const name = readLine.question('Name of ' + i + ' player: ');
            if (!players.includes(name)) {
                players.push(name);
                i++;
            }
            else console.log('This name is already used. Try again.');
        }

        let playersNameLength = 0;
        players.forEach(player => {
            playersNameLength = Math.max(playersNameLength, player.length);
        })
        for (let i = 0; i < players.length; i++) {
            while (players[i].length < playersNameLength)
                players[i] = players[i] + ' ';
        }

        return players;
    }

}

module.exports = {ConfigBuilder}


