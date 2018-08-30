import * as go from 'gojs';


const makeLevel = (levelGroups, currentLevel, totalGroups, players) => {
// eslint-disable-next-line no-param-reassign
  currentLevel--;
  const len = levelGroups.length;
  const parentKeys = [];
  let parentNumber = 0;
  let p = '';
  for (let i = 0; i < len; i++) {
    if (parentNumber === 0) {
      p = `${currentLevel}-${parentKeys.length}`;
      parentKeys.push(p);
    }

    if (players !== null) {
      const p1 = players[i * 2];
      const p2 = players[(i * 2) + 1];
      totalGroups.push({
        key: levelGroups[i], parent: p, player1: p1, player2: p2, parentNumber
      });
    } else {
      totalGroups.push({ key: levelGroups[i], parent: p, parentNumber });
    }

    parentNumber++;
    if (parentNumber > 1) parentNumber = 0;
  }

  // after the first created level there are no player names
  if (currentLevel >= 0) makeLevel(parentKeys, currentLevel, totalGroups, null);
};

const createPairs = players => {
  if (players.length % 2 !== 0) players.push('(empty)');
  const startingGroups = players.length / 2;
  const currentLevel = Math.ceil(Math.log(startingGroups) / Math.log(2));
  const levelGroups = [];
  for (let i = 0; i < startingGroups; i++) {
    levelGroups.push(`${currentLevel}-${i}`);
  }
  const totalGroups = [];
  makeLevel(levelGroups, currentLevel, totalGroups, players);
  return totalGroups;
};

export const makeModel = players => {
  return new go.TreeModel(createPairs(players));
};
