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

        this.upperBracket = new Array(numberOfRounds);
    }

    showWinnersBracket(inputWinnersBracket, gridType) {
        this.upperBracket =  inputWinnersBracket.slice();
        this.upperBracket = printer.suplementBracket(this.upperBracket, this.numberOfRounds, this.playerNameLength);
        printer.print(gridType, this.upperBracket, this.numberOfRounds, this.numberOfPlayers, this.playerNameLength);
    }

    updateWinnersBracket(round, game, winner) {
        this.upperBracket[round][game] = winner;
        printer.print(this.upperBracket, this.numberOfRounds, this.playerNameLength);
    }

}

module.exports={Printer}