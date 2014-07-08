using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper
{
	class GameSettings
	{
		public int GameFieldHeight { get; private set; }
		public int GameFieldWidth { get; private set; }
		public int MinesNumber { get; private set; }

		public GameSettings(int gameFieldHeight, int gameFieldWidth, int minesNumber)
		{
			GameFieldHeight = gameFieldHeight;
			GameFieldWidth = gameFieldWidth;
			MinesNumber = minesNumber;
		}
	}
}
