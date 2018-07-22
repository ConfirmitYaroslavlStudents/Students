const { shuffleArray, isPowerOfTwo } = require('./utils.js');
const { Championship, States } = require('./championship.js');

const championship = new Championship();

var readline = require('readline');
var rl = readline.createInterface(process.stdin, process.stdout);

console.log('Enter participants: ');

rl.prompt();

rl.on('line', function (line) {

    if (championship.state === States.Registration) {
        participants = line.split(',').map(x => x.trim());

        if (!isPowerOfTwo(participants.length)) {
            console.log("Incorrect number of participants");
        } else {

            championship.init(participants);
            console.log("Paticipants:" + championship.participants);           
            championship.breakIntoPair();
            console.log("Pairs:" + JSON.stringify(championship.getPairs()));
            console.log("Write who won in each pair in this format: first,second,...");
            championship.goToState(States.SetWinners);
        }

    } else if (championship.state === States.SetWinners) {
        const winners = line.split(',').map(x => x.trim()); //[first, second, first, ...]

        if (winners.length != championship.pairs.length) {
            console.log("Winners input is incorrect!");
            rl.prompt();
            return;
        }

        if (winners.filter(winner => { return (winner != 'first' && winner != 'second') }).length > 0) {
            console.log("You misspelled first/second! You should type first/second!");
            console.log(winners);
            rl.prompt();
            return;
        }

        championship.leaveWinners(winners)

        if (championship.participants.length === 1) {
            console.log("Winner:" + championship.participants[0]);
            rl.close();
        }
        else {
            console.log("Paticipants:" + championship.participants);
            championship.breakIntoPair();
            console.log("Pairs:" + JSON.stringify(championship.getPairs()));
            console.log("Write who won in each pair in this format: first,second,...");
        }
    }

    if (line === "exit") rl.close();

    rl.prompt();
}).on('close', function () {
    process.exit(0);
});