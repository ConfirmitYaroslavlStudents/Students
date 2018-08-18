const readLine = require('readline-sync');
const exception = require('../exceptions.js');
const {Printer} = require('../printer.js');
const {TournamentBase} = require('./tournament-base.js');

const printer = new Printer();

class SingleEliminationTournament extends TournamentBase {

    constructor(configuration) {
        super(configuration);

        this.playersNameLength = 1; //todo will go away after printer refactoring
        this.upperBracket = configuration.tournamentState.upperBracket;

        if (this.upperBracket.length === 0) // check if continue game or start anew
        {
            this.upperBracket = new Array(this.numberOfRounds + 1);
            this.lowerBracket = new Array(this.numberOfRounds);
            this.breakIntoPairs();
        }
    }


    breakIntoPairs() {
        const shuffledPlayers = shuffleArray(this.players);
        this.upperBracket[0] = shuffledPlayers;
    }

    play() {
        printer.init(this.numerOfPlayers, this.numberOfRounds, this.playersNameLength);
        printer.showWinnersBracket(this.upperBracket, this.gridType);

        for (let round = this.currentRound; round < this.numberOfRounds; round++) {
            this.playRound(round);
        }

        console.log();
        console.log('The winner is ' + this.upperBracket[this.numberOfRounds][0].trim() + '!')
    }

    playRound(round) {
        const setOfWinners = this.upperBracket[round];

        this.upperBracket[round + 1] = [];

        for (let game = this.currentGame; game < setOfWinners.length; game = game + 2) {
            const currentPair = [setOfWinners[game], setOfWinners[game + 1]];
            const {winnerIndex, loserIndex} = this.playGame(currentPair);

            this.upperBracket[round + 1][game / 2] = currentPair[winnerIndex];
            printer.showWinnersBracket(this.upperBracket, this.gridType);

            this.onStateChanged({currentRound: round, currentGame: game, upperBracket: this.upperBracket })
        }
        this.currentGame=0;
    }
    playGame(pair) {
        const winnerIndex = this.askForWinner(pair);
        if (winnerIndex < 0)
            process.exit(0);
        let loserIndex = (winnerIndex === 0) ? 1 : 0;
        return {winnerIndex, loserIndex};
    }

    //todo: take from utils
    askForWinner(array) {
        const index = readLine.keyInSelect(array, 'Who won?');
        return index;
    }
}

//todo: take from utils
shuffleArray = (a) => {
    for (let i = a.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [a[i], a[j]] = [a[j], a[i]];
    }
    return a;
}

module.exports = {SingleEliminationTournament};