/* eslint-disable curly */
const clear = require('clear');
const colors = require('colors');
const render = require('./../renders/renders');

class Grid {
  constructor(gridType) {
    this.gridType = gridType;
    this.render = this._renderType();
  }

  show(state) {
    clear();
    console.log('The tournament table at the moment:\n'.bgCyan);

    const grid = this.render(state);

    for (let x = 0; x < grid[0].length; x++) {
      for (let y = 0; y < grid.length; y++)
        if (grid[y][x])
          process.stdout.write(grid[y][x]);
      process.stdout.write('\n');
    }
  }

  _renderType() {
    return render[`${this.gridType}`];
  }
}

module.exports = Grid;
