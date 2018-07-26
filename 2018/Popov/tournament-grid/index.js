'use strict';

import chalk from 'chalk';
import inquirer from 'inquirer';

import Tournament from './src/Tournament';

let tournament = new Tournament();

inquirer
  .prompt([
    { type: 'input', name: 1, message: 'Enter #1 player name:'},
    { type: 'input', name: 2, message: 'Enter #2 player name:'},
    { type: 'input', name: 3, message: 'Enter #3 player name:'},
    { type: 'input', name: 4, message: 'Enter #4 player name:'},
    { type: 'input', name: 5, message: 'Enter #5 player name:'},
    { type: 'input', name: 6, message: 'Enter #6 player name:'},
    { type: 'input', name: 7, message: 'Enter #7 player name:'},
    { type: 'input', name: 8, message: 'Enter #8 player name:'}
  ])
  .then(players => {
    for (let id in players) {
      tournament.addPlayer(players[id]);
      console.log(players[id]);
    }
    tournament.run();
    gameLoop();
  });

  function gameLoop() {
    tournament.view.print();

    if (tournament.grid.currentMatch === false) {
      return endGame();
    }

    let
      currentMatch = tournament.grid.currentMatch,
      currentPair = currentMatch.players;

    console.log('\n');

    inquirer
      .prompt([{
        type: 'rawlist',
        message: 'Who has won?',
        name: 'winner',
        choices: [currentPair[0].name, currentPair[1].name]
      }])
      .then(input => {
        if (currentPair[0].name === input.winner) {
          currentMatch.setWinnerById(0);
          tournament.view.print();
        }
        else {
          currentMatch.setWinnerById(1);
          tournament.view.print();
        }
        gameLoop();
      });
  }

function endGame() {
  let winners = tournament.grid.winners;

  console.log(`\n\nFirst place: ${chalk.bold.green(winners[1].winner.name)}`);
  console.log(`Second place: ${chalk.bold.whiteBright(winners[1].loser.name)}`);
  console.log(`Third place: ${chalk.bold.yellow(winners[0].winner.name)}`);
}
