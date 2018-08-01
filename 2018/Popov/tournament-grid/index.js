'use strict';

import Tournament from './src/Tournament';
import ConsoleActions from './src/ConsoleActions';

main();

async function main() {
  let
    tournament = null,
    loadLatsGame = false;

  if (Tournament.hasSavedState()) {
    loadLatsGame = await ConsoleActions.loadLastGame();
  }

  if (loadLatsGame) {
    tournament = new Tournament();
    tournament.initFromSavedState();
  }
  else {
    tournament = await createNewGame();
    tournament.init();
  }

  while (!tournament.gameOver) {
    tournament.view.print();

    let winner = await ConsoleActions.getWinner(tournament.bracket.currentMatch.players);
    tournament.bracket.currentMatchWinner = winner;
  }

  tournament.view.print();
  ConsoleActions.printWinners(
    tournament.bracket.firstPlace.name,
    tournament.bracket.secondPlace.name
  );
}

async function createNewGame() {
  const tournament = new Tournament(
    await ConsoleActions.getBracket(),
    await ConsoleActions.getBracketView()
  );

  const players = await ConsoleActions.getPlayers(
    await ConsoleActions.getPlayersCount()
  );

  for (let id in players) {
    tournament.addPlayer(players[id]);
  }

  return tournament;
}
