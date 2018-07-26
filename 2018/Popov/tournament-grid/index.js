'use strict';

import kuler from 'kuler';
import inquirer from 'inquirer';

import Tournament from './src/Tournament';

import FIFAGridView from './src/views/FIFAGridView';
import DefaultGridView from './src/views/DefaultGridView';

import SingleEliminationBracket from './src/brackets/SingleEliminationBracket';
import DoubleEliminationBracket from './src/brackets/DoubleEliminationBracket';

async function main() {
  const
    playersCount = await getPlayersCount(),
    tournament = new Tournament(await getBracket(), await getBracketView());

  const players = await getPlayers(playersCount);

  for (let id in players) {
    tournament.addPlayer(players[id]);
  }

  tournament.init();

  while (!tournament.gameOver) {
    tournament.view.print();

    let winner = await getWinner(tournament.bracket.currentMatch.players);
    tournament.bracket.currentMatchWinner = winner;
  }

  tournament.view.print();
  console.log('First place: ' + kuler(tournament.bracket.firstPlace.name, '#ffcb6b'));
  console.log('Second place: ' + kuler(tournament.bracket.secondPlace.name, 'white'));
}

async function getBracket() {
  let {bracketType} = await inquirer.prompt([{
    type: 'rawlist', message: 'Select bracket type',
    name: 'bracketType', choices: ['Single Elimination', 'Double Elimination']
  }]);

  switch (bracketType) {
    case 'Double Elimination':
      return new DoubleEliminationBracket();
    default:
      return new SingleEliminationBracket();
  }
}

async function getBracketView() {
  let {bracketType} = await inquirer.prompt([{
    type: 'rawlist', message: 'Select bracket view',
    name: 'bracketType', choices: ['Default', 'FIFA 2018']
  }]);

  switch (bracketType) {
    case 'FIFA 2018':
      return new FIFAGridView();
    default:
      return new DefaultGridView();
  }
}

async function getPlayersCount() {
  let {playersCount} = await inquirer.prompt([{
    type: 'input', message: 'Input players count',
    name: 'playersCount', validate: function(input) {
      if (isNaN(parseInt(input))) {
        return 'You need to provide a number';
      }
      if (Math.log2(parseInt(input)) % 1 !== 0) {
        return 'Invalid players count';
      }
      return true;
    }
  }]);
  return parseInt(playersCount);
}

async function getPlayers(count) {
  let questions = Array(count).fill(null);
  questions = questions.map((question, index) => {
    return {
      type: 'input',
      name: index + 1,
      message: `Enter #${index + 1} player name:`,
      validate: (input) => !input.length ? 'Invalid player name' : true
    }
  });
  return await inquirer.prompt(questions);
}

async function getWinner(players) {
  let {winnerName} = await inquirer.prompt([{
    type: 'rawlist',
    message: 'Who has won?',
    name: 'winnerName',
    choices: [players[0].name, players[1].name]
  }]);
  return players[0].name === winnerName ? players[0] :players[1];
}

main();
