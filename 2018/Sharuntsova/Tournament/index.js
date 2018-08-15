const {Tournament} = require('./src/tournament.js');
const readLine = require('readline-sync');

let numberOfPlayers = NaN;
while (isNaN(numberOfPlayers)){
    numberOfPlayers = readLine.question('Input the number of players: ');
}
const tournament = new Tournament(numberOfPlayers);
tournament.init();
tournament.play();


