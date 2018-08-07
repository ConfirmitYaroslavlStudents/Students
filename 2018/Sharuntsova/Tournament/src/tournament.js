const readLine = require('readline-sync');
const exception = require('./exceptions.js');
const { Printer } = require('./printer.js');
const {TypesOfPrinting} = require('./printingTypes.js');
    
const printer = new Printer();

class Tournament {

    constructor(numerOfPlayers) {
        this.numerOfPlayers = numerOfPlayers;
        this.playersNameLength = 1;
    }

    init() {
        this.numberOfRounds = Math.log2(this.numerOfPlayers);
        if (this.numberOfRounds !== Math.ceil(this.numberOfRounds))
            throw exception.IncorrectNumberOfPlayersException();
        this.players = [];
        this.inputPlayersNames();
        this.completePlayersName();

        this.winnersBracket = new Array(this.numberOfRounds + 1);
        this.losersBracket = new Array(this.numberOfRounds);

        this.breakIntoPairs();

        const printingType = ['Olympics', 'DoubleBracket'];
        const index=this.chooseTypeOfDisplay(printingType);
        switch(index){
            case 'Olympics':
                this.typeOfPrinting = TypesOfPrinting.Olympics;
                break;
            case 'DoubleBracket':
                this.typeOfPrinting = TypesOfPrinting.DoubleBracket;
                break;
            default:
                process.exit(0);
        }         
        
    }

    inputPlayersNames() {
        console.log('Input the names of players:');
        let i = 1;
        while (i <= this.numerOfPlayers) {
            const name = readLine.question('Name of ' + i + ' player: ');
            if (!this.players.includes(name)) {
                this.players.push(name);
                i++;
            }
            else console.log('This name is already used. Try again.');
        }
    }

    completePlayersName() {
        this.players.forEach(player => {
            this.playersNameLength = Math.max(this.playersNameLength, player.length);
        })
        for (let i = 0; i < this.players.length; i++) {
            while (this.players[i].length < this.playersNameLength)
                this.players[i] = this.players[i] + ' ';
        }
    }

    breakIntoPairs() {
        const shuffledPlayers = shuffleArray(this.players);
        this.winnersBracket[0] = shuffledPlayers;
    }

    chooseTypeOfDisplay(printingType) {
        const index = readLine.keyInSelect(printingType, 'Choose type of display of bracket: ');
        if (index < 0)
            process.exit(0);
        return printingType[index];
    }

    play() {
        printer.init(this.numerOfPlayers, this.numberOfRounds, this.playersNameLength); //nameLenght!!
        printer.showWinnersBracket(this.winnersBracket, this.typeOfPrinting);
        for (let round = 0; round < this.numberOfRounds; round++) {
            this.playRound(round);
        }
        console.log();
        console.log('The winner is ' + this.winnersBracket[this.numberOfRounds][0].trim() + '!')
    }

    playRound(round) {
        const setOfWinners = this.winnersBracket[round];

        this.winnersBracket[round + 1] = [];

        for (let i = 0; i < setOfWinners.length; i = i + 2) {
            const currentPair = [setOfWinners[i], setOfWinners[i + 1]];
            const { winnerIndex, loserIndex } = this.playGame(currentPair);

            this.winnersBracket[round + 1][i / 2] = currentPair[winnerIndex];
            printer.showWinnersBracket(this.winnersBracket, this.typeOfPrinting);
        }
    }

    playGame(pair) {
        const winnerIndex = this.askForWinner(pair);
        if (winnerIndex < 0)
            process.exit(0);
        let loserIndex = (winnerIndex === 0) ? 1 : 0;
        return { winnerIndex, loserIndex };
    }

    askForWinner(array) {
        const index = readLine.keyInSelect(array, 'Who won?');
        return index;
    }
}

shuffleArray = (a) => {
    for (let i = a.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [a[i], a[j]] = [a[j], a[i]];
    }
    return a;
}

module.exports = { Tournament };