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

			SetMines();

			SetGameFieldElementsValues();
		}

		private void SetMines()
		{
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
		}

		public List<Cell> OpenCells(int rowIndex, int columnIndex)
		{
			var openCellsMethod = new OpenCellsMathod(Field);

			if (!Field[rowIndex, columnIndex].IsOpened && !Field[rowIndex, columnIndex].IsMarked)
			{
				if (!Field[rowIndex, columnIndex].IsMine)
					openCellsMethod.CellCalculation(new Cell(rowIndex, columnIndex));
				else
					State = GameState.PlayerLose;
			}

			return openCellsMethod.OpenedCells;
		}

		public Cell RemarkCell(int rowIndex, int columnIndex)
		{
			Field[rowIndex, columnIndex].IsMarked = !Field[rowIndex, columnIndex].IsMarked;
			return new Cell(rowIndex, columnIndex);
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
			var setValuesMathod = new SetValuesMathod(Field);

			for (int i = 0; i < _settings.GameFieldHeight; ++i)
				for (int j = 0; j < _settings.GameFieldWidth; ++j)
					setValuesMathod.CellCalculation(new Cell(i, j));
		}
	}
}
