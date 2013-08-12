using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
	class SetValuesMathod : CellAroundMethod
	{
		private GameCell[,] _field;

		public SetValuesMathod(GameCell[,] field)
			: base(field.GetLength(0), field.GetLength(1))
		{
			_field = field;
		}

		protected override void UnconditionalAction(Cell currentCell) { }

		protected override bool NeedCheckNeighbors(Cell currentCell)
		{
			if (!_field[currentCell.Row, currentCell.Column].IsMine)
				return true;
			else
				return false;
		}

		protected override bool Condition(Cell currentCell, Cell neighbourCell)
		{
			if (_field[neighbourCell.Row, neighbourCell.Column].IsMine)
				return true;
			else
				return false;
		}

		protected override void ConditionalAction(Cell currentCell, Cell neighbourCell)
		{
			++_field[currentCell.Row, currentCell.Column].Value;
		}
	}
}
