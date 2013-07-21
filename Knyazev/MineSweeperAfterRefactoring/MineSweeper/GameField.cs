using System;
using System.Collections.Generic;
using System.Drawing;

namespace MineSweeper
{
	enum GameState { GameInProcess, PlayerLose, PlayerWin }

	class Game
	{
		private GameSettings _settings;
		public GameState State { get; private set; }
		public GameCell[,] Field { get; private set; }

		public Game(GameSettings gameSettings)
		{
			_settings = gameSettings;
			Field = new GameCell[_settings.GameFieldHeight, _settings.GameFieldWidth];

			Random randomGenerator = new Random();
			for (int i = 0; i < _settings.MinesNumber; i++)
			{
				while (true)
				{
					int row = randomGenerator.Next(_settings.GameFieldHeight);
					int column = randomGenerator.Next(_settings.GameFieldWidth);
					if (!Field[row, column].IsMine)
					{
						Field[row, column].IsMine = true;
						break;
					}
				}
			}

			SetGameFieldElementsValues();
		}

		public List<Point> OpenCell(int rowIndex, int columnIndex)
		{
			List<Point> result = new List<Point>();
			OpenCells(result, rowIndex, columnIndex);
			return result;
		}

		private void OpenCells(List<Point> changedCells, int rowIndex, int columnIndex)
		{
			changedCells.Add(new Point(rowIndex, columnIndex));

			if (!Field[rowIndex, columnIndex].IsOpened && !Field[rowIndex, columnIndex].IsMarked)
			{
				if (!Field[rowIndex, columnIndex].IsMine)
				{
					Field[rowIndex, columnIndex].IsOpened = true;

					if (Field[rowIndex, columnIndex].Value == 0)
					{
						if (rowIndex > 0)
						{
							if (columnIndex > 0)
								OpenCells(changedCells, rowIndex - 1, columnIndex - 1);

							OpenCells(changedCells, rowIndex - 1, columnIndex);

							if (columnIndex + 1 < _settings.GameFieldWidth)
								OpenCells(changedCells, rowIndex - 1, columnIndex + 1);
						}

						if (columnIndex > 0)
							OpenCells(changedCells, rowIndex, columnIndex - 1);

						if (columnIndex + 1 < _settings.GameFieldWidth)
							OpenCells(changedCells, rowIndex, columnIndex + 1);

						if (rowIndex + 1 < _settings.GameFieldHeight)
						{
							if (columnIndex > 0)
								OpenCells(changedCells, rowIndex + 1, columnIndex - 1);

							OpenCells(changedCells, rowIndex + 1, columnIndex);

							if (columnIndex + 1 < _settings.GameFieldWidth)
								OpenCells(changedCells, rowIndex + 1, columnIndex + 1);
						}
					}
				}
				else
					State = GameState.PlayerLose;
			}
		}

		public Point RemarkCell(int rowIndex, int columnIndex)
		{
			Field[rowIndex, columnIndex].IsMarked = !Field[rowIndex, columnIndex].IsMarked;
			return new Point(rowIndex, columnIndex);
		}

		public bool PlayerWins()
		{
			int openCellsCount = 0;
			for (int i = 0; i < _settings.GameFieldHeight; ++i)
				for (int j = 0; j < _settings.GameFieldWidth; ++j)
					if (Field[i, j].IsOpened)
					++openCellsCount;

			if (openCellsCount == (_settings.GameFieldWidth * _settings.GameFieldHeight - _settings.MinesNumber))
			{
				State = GameState.PlayerWin;
				return true;
			}

			return false;
		}

		public void EndGame()
		{
			for (int i = 0; i < _settings.GameFieldHeight; ++i)
				for (int j = 0; j < _settings.GameFieldWidth; ++j)
					Field[i, j].IsOpened = true;
		}

		private void SetGameFieldElementsValues()
		{
			for (int i = 0; i < _settings.GameFieldHeight; ++i)
				for (int j = 0; j < _settings.GameFieldWidth; ++j)
					if (!Field[i, j].IsMine)
					{
						if (i > 0)
						{
							if (j > 0 && Field[i - 1, j - 1].IsMine)
								++Field[i, j].Value;
							if (Field[i - 1, j].IsMine)
								++Field[i, j].Value;
							if (j + 1 < _settings.GameFieldWidth && Field[i - 1, j + 1].IsMine)
								++Field[i, j].Value;
						}

						if (j > 0 && Field[i, j - 1].IsMine)
							++Field[i, j].Value;
						if (j + 1 < _settings.GameFieldWidth && Field[i, j + 1].IsMine)
							++Field[i, j].Value;

						if (i + 1 < _settings.GameFieldHeight)
						{
							if (j > 0 && Field[i + 1, j - 1].IsMine)
								++Field[i, j].Value;
							if (Field[i + 1, j].IsMine)
								++Field[i, j].Value;
							if (j + 1 < _settings.GameFieldWidth && Field[i + 1, j + 1].IsMine)
								++Field[i, j].Value;
						}
					}
		}
	}
}
