const States = {
    Registration: 1,
    SetWinners: 2,
}

class Championship {

    constructor() {
        this.participants = [];
        this.pairs = [];
        this.state = States.Registration
    }

    init(participants) {

        this.participants = participants;
    }

    goToState(state) {
        this.state = state;
    }

    leaveWinners(winners) {
        // winners = ['first', 'second', 'second', ...]        
        this.participants = this.pairs.map((pair, index) => { return pair[winners[index]] });
    }

    breakIntoPair() {

        this.pairs = [];
        const shuffled = shuffleArray(this.participants);

        for (let i = 0; i < shuffled.length; i = i + 2) {
            this.pairs.push({ first: shuffled[i], second: shuffled[i + 1] });
        }
    }
    
    getPairs() {
        return this.pairs;
    }
}

module.exports = {
    Championship,
    States
}