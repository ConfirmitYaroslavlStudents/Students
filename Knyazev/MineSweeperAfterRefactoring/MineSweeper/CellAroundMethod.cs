using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
	abstract class CellAroundMethod
	{
		protected int Height, Width;

		public CellAroundMethod(int height, int width)
		{
			Height = height;
			Width = width;
		}

		public void CellCalculation(Cell currentCell)
		{
			UnconditionalAction(currentCell);

			if (NeedCheckNeighbors(currentCell))
			{
				Cell neighbourCell;

				if (currentCell.Row > 0)
				{
					neighbourCell.Row = currentCell.Row - 1;

					if (currentCell.Column > 0)
					{
						neighbourCell.Column = currentCell.Column - 1;
						if (Condition(currentCell, neighbourCell))
							ConditionalAction(currentCell, neighbourCell);
					}

					neighbourCell.Column = currentCell.Column;
					if (Condition(currentCell, neighbourCell))
						ConditionalAction(currentCell, neighbourCell);

					if (currentCell.Column + 1 < Width)
					{
						neighbourCell.Column = currentCell.Column + 1;
						if (Condition(currentCell, neighbourCell))
							ConditionalAction(currentCell, neighbourCell);
					}
				}

				neighbourCell.Row = currentCell.Row;

				if (currentCell.Column > 0)
				{
					neighbourCell.Column = currentCell.Column - 1;
					if (Condition(currentCell, neighbourCell))
						ConditionalAction(currentCell, neighbourCell);
				}
				if (currentCell.Column + 1 < Width)
				{
					neighbourCell.Column = currentCell.Column + 1;
					if (Condition(currentCell, neighbourCell))
						ConditionalAction(currentCell, neighbourCell);
				}

				if (currentCell.Row + 1 < Height)
				{
					neighbourCell.Row = currentCell.Row + 1;

					if (currentCell.Column > 0)
					{
						neighbourCell.Column = currentCell.Column - 1;
						if (Condition(currentCell, neighbourCell))
							ConditionalAction(currentCell, neighbourCell);
					}

					neighbourCell.Column = currentCell.Column;
					if (Condition(currentCell, neighbourCell))
						ConditionalAction(currentCell, neighbourCell);

					if (currentCell.Column + 1 < Width)
					{
						neighbourCell.Column = currentCell.Column + 1;
						if (Condition(currentCell, neighbourCell))
							ConditionalAction(currentCell, neighbourCell);
					}
				}
			}
		}

		protected abstract void UnconditionalAction(Cell currentCell);
		protected abstract bool NeedCheckNeighbors(Cell currentCell);
		protected abstract bool Condition(Cell currentCell, Cell neighbourCell);
		protected abstract void ConditionalAction(Cell currentCell, Cell neighbourCell);
	}
}
