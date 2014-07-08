using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
	struct Cell
	{
		public int Row, Column;

		public Cell(int row, int column)
		{
			Row = row;
			Column = column;
		}
	}
}
