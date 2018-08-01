'use strict';

import kuler from 'kuler';
import inquirer from 'inquirer';

import ViewsTypes from './views/ViewsTypes';
import BracketsTypes from './brackets/BracketsTypes';

export default class ConsoleActions {
  static async loadLastGame() {
    let {load} = await inquirer.prompt([{
      type: 'rawlist',
      name: 'load',
      message: 'Load last game?',
      choices: ['yes', 'no']
    }]);

    return load === 'yes';
  }

  static async getBracket() {
    let {bracketType} = await inquirer.prompt([{
      type: 'rawlist',
      name: 'bracketType',
      message: 'Select bracket type',
      choices: Object.keys(BracketsTypes)
    }]);

    return bracketType;
  }

  static async getBracketView() {
    let {viewType} = await inquirer.prompt([{
      type: 'rawlist',
      name: 'viewType',
      message: 'Select bracket view',
      choices: Object.keys(ViewsTypes)
    }]);

    return viewType;
  }

  static async getPlayersCount() {
    let {playersCount} = await inquirer.prompt([{
      type: 'input',
      name: 'playersCount',
      message: 'Input players count',
      validate: function(input) {
        const count = parseInt(input);

        if (isNaN(count)) {
          return 'You need to provide a number';
        }

        if (count < 2 || Math.log2(count) % 1 !== 0) {
          return 'Invalid players count';
        }

        return true;
      }
    }]);
    return parseInt(playersCount);
  }

  static async getPlayers(count) {
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

  static async getWinner(players) {
    let {winnerName} = await inquirer.prompt([{
      type: 'rawlist',
      name: 'winnerName',
      message: 'Who has won?',
      choices: [players[0].name, players[1].name]
    }]);

    return players[0].name === winnerName ? players[0] :players[1];
  }

  static printWinners(firstPlace, secondPlace) {
    console.log('First place: ' + kuler(firstPlace, '#ffcb6b'));
    console.log('Second place: ' + kuler(secondPlace, 'white'));
  }
}
