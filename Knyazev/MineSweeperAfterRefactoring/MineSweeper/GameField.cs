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

		public List<Cell> OpenCell(int rowIndex, int columnIndex)
		{
			List<Cell> result = new List<Cell>();
			OpenCells(result, rowIndex, columnIndex);
			return result;
		}

		private void OpenCells(List<Cell> openedCells, int rowIndex, int columnIndex)
		{
			openedCells.Add(new Cell(rowIndex, columnIndex));

			if (!Field[rowIndex, columnIndex].IsOpened && !Field[rowIndex, columnIndex].IsMarked)
			{
				if (!Field[rowIndex, columnIndex].IsMine)
				{
					var openCellsMethod = new OpenCellsMathod(Field, openedCells);
					openCellsMethod.CellCalculation(new Cell(rowIndex, columnIndex));
				}
				else
					State = GameState.PlayerLose;
			}
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
			for (int i = 0; i < _settings.GameFieldHeight; ++i)
				for (int j = 0; j < _settings.GameFieldWidth; ++j)
				{
					var openCellsMethod = new SetValueMathod(Field);
					openCellsMethod.CellCalculation(new Cell(i, j));
				}

		}

		class OpenCellsMathod : CellAroundMethod
		{
			private List<Cell> _openedCells;

			public OpenCellsMathod(GameCell[,] field, List<Cell> openedCells)
			{
				Field = field;
				_openedCells = openedCells;
			}

			protected override void UnconditionalAction(Cell currentCell)
			{
				((GameCell[,])Field)[currentCell.Row, currentCell.Column].IsOpened = true;
				_openedCells.Add(currentCell);
			}

			protected override bool NeedCheckNeighbors(Cell currentCell)
			{
				if (((GameCell[,])Field)[currentCell.Row, currentCell.Column].Value == 0)
					return true;
				else
					return false;
			}

			protected override bool Condition(Cell currentCell, Cell neighbourCell)
			{
				if (!((GameCell[,])Field)[neighbourCell.Row, neighbourCell.Column].IsOpened && !((GameCell[,])Field)[neighbourCell.Row, neighbourCell.Column].IsMine
					&& !((GameCell[,])Field)[neighbourCell.Row, neighbourCell.Column].IsMarked)
					return true;
				else
					return false;
			}

			protected override void ConditionalAction(Cell currentCell, Cell neighbourCell)
			{
				CellCalculation(neighbourCell);
			}
		}

		class SetValueMathod : CellAroundMethod
		{
			public SetValueMathod(GameCell[,] field)
			{
				Field = field;
			}
			
			protected override void UnconditionalAction(Cell currentCell) { }

			protected override bool NeedCheckNeighbors(Cell currentCell) 
			{
				if (!((GameCell[,])Field)[currentCell.Row, currentCell.Column].IsMine)
					return true;
				else
					return false;
			}

			protected override bool Condition(Cell currentCell, Cell neighbourCell)
			{
				if (((GameCell[,])Field)[neighbourCell.Row, neighbourCell.Column].IsMine)
					return true;
				else
					return false;
			}

			protected override void ConditionalAction(Cell currentCell, Cell neighbourCell)
			{
				++((GameCell[,])Field)[currentCell.Row, currentCell.Column].Value;
			}
		}
	}
}
