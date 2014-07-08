using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
	class OpenCellsMathod : CellAroundMethod
	{
		private GameCell[,] _field;
		public List<Cell> OpenedCells { get; private set; }

		public OpenCellsMathod(GameCell[,] field) 
			: base(field.GetLength(0), field.GetLength(1))
		{
			_field = field;
			OpenedCells = new List<Cell>();
		}

		protected override void UnconditionalAction(Cell currentCell)
		{
			_field[currentCell.Row, currentCell.Column].IsOpened = true;
			OpenedCells.Add(currentCell);
		}

		protected override bool NeedCheckNeighbors(Cell currentCell)
		{
			if (_field[currentCell.Row, currentCell.Column].Value == 0)
				return true;
			else
				return false;
		}

		protected override bool Condition(Cell currentCell, Cell neighbourCell)
		{
			if (!_field[neighbourCell.Row, neighbourCell.Column].IsOpened && !_field[neighbourCell.Row, neighbourCell.Column].IsMine
				&& !_field[neighbourCell.Row, neighbourCell.Column].IsMarked)
				return true;
			else
				return false;
		}

		protected override void ConditionalAction(Cell currentCell, Cell neighbourCell)
		{
			CellCalculation(neighbourCell);
		}
	}
}
