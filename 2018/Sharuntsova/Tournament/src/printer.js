const printer = require('./print_utils.js');
const {TypesOfPrinting} = require('./printingTypes.js');

class Printer {

    constructor() {
        this.numberOfPlayers;
        this.numberOfRounds;
        this.playerNameLength;
    }

    init(numberOfPlayers, numberOfRounds, playerNameLength){
        this.numberOfPlayers = numberOfPlayers;
        this.numberOfRounds = numberOfRounds;
        this.playerNameLength = playerNameLength;

        this.winnersBracket = new Array(numberOfRounds);
    }

    showWinnersBracket(inputWinnersBracket, typeOfDispaly) {
        this.winnersBracket =  inputWinnersBracket.slice();
        this.winnersBracket = printer.suplementBracket(this.winnersBracket, this.numberOfRounds, this.playerNameLength);
        printer.print(typeOfDispaly, this.winnersBracket, this.numberOfRounds, this.numberOfPlayers, this.playerNameLength);
    }

    updateWinnersBracket(round, game, winner) {
        this.winnersBracket[round][game] = winner;
        printer.print(this.winnersBracket, this.numberOfRounds, this.playerNameLength);
    }

}

module.exports={Printer}