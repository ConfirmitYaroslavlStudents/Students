/* eslint-disable no-param-reassign */
function getNodeStructure(names, count, pow) {
  const actualGameSet = [];
  for (let i = 0; i < count; i += 2) {
    actualGameSet.push({
      text: {
        name: 'Undefined'
      },
      HTMLclass: 'first-draw',
      children: [
        {
          text: {
            name: names[i]
          },
          HTMLclass: 'first-draw'
        },
        {
          text: {
            name: names[i + 1]
          },
          HTMLclass: 'first-draw'
        }
      ]
    });
  }

  console.log(pow);
  console.log(actualGameSet);

  return actualGameSet;
}

function include(children, name) {
  for (const child of children) {
    if (child.text.name === name) {return true;}
  }
  return false;
}

function addWinner(game, name) {
  if (!game.children) {
    return game;
  }
  if (include(game.children, name) && game.text.name === 'Undefined') {
    game.text.name = name;
    return game;
  }
  for (const g of game.children) {
    addWinner(g, name);
  }
  return game;
}

export function isPowerOfTwo(number) {
  const n = parseInt(number, 10);
  return Math.log2(n) % 1 === 0 && n !== 1;
}

export function generateTree(names) {
  const count = names.length;
  const pow = Math.log2(count);

  console.log(names);

  return {
    chart: {
      container: '#chart',
      levelSeparation: 20,
      siblingSeparation: 15,
      subTeeSeparation: 15,
      rootOrientation: 'EAST',

      node: {
        HTMLclass: 'tennis-draw',
        drawLineThrough: true
      },
      connectors: {
        type: 'straight',
        style: {
          'stroke-width': 2,
          stroke: '#ccc'
        }
      }
    },

    nodeStructure: getNodeStructure(names, count, pow)[0]
  };
}

export function addWinnerInTree(tree, winnerName) {
  tree.nodeStructure = addWinner(tree.nodeStructure, winnerName);
  return tree;
}
