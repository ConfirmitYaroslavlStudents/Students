using System;
using System.Drawing;

namespace MineSweeper
{
	enum GameState { GameInProcess, PlayerLose, PlayerWin }

	class GameField
	{
		private GameCell[,] _gameField;
		private MainMineSweeperForm _gameForm;

		public GameState State { get; private set; }

		public GameField(MainMineSweeperForm gameForm)
		{
			_gameForm = gameForm;
			_gameField = new GameCell[_gameForm.VerticalGameButtonsNumber, _gameForm.HorizontalGameButtonsNumber];

			Random randomGenerator = new Random();
			for (int i = 0; i < _gameForm.MinesNumber; i++)
			{
				while (true)
				{
					int row = randomGenerator.Next(_gameForm.VerticalGameButtonsNumber);
					int column = randomGenerator.Next(_gameForm.HorizontalGameButtonsNumber);
					if (!_gameField[row, column].IsMine)
					{
						_gameField[row, column].IsMine = true;
						break;
					}
				}
			}

			SetGameFieldElementsValues();
		}

		public void OpenCell(int rowIndex, int columnIndex)
		{
			if (!_gameField[rowIndex, columnIndex].IsOpened && !_gameField[rowIndex, columnIndex].IsMarked)
			{
				if (!_gameField[rowIndex, columnIndex].IsMine)
				{
					_gameField[rowIndex, columnIndex].IsOpened = true;
					int buttonIndex = rowIndex * _gameForm.VerticalGameButtonsNumber + columnIndex;
					_gameForm.GameButtons[buttonIndex].Enabled = false;
					_gameForm.GameButtons[buttonIndex].BackColor = Color.Silver;

					if (_gameField[rowIndex, columnIndex].Value == 0)
					{
						if (rowIndex > 0)
						{
							if (columnIndex > 0)
								OpenCell(rowIndex - 1, columnIndex - 1);

							OpenCell(rowIndex - 1, columnIndex);

							if (columnIndex + 1 < _gameForm.HorizontalGameButtonsNumber)
								OpenCell(rowIndex - 1, columnIndex + 1);
						}

						if (columnIndex > 0)
							OpenCell(rowIndex, columnIndex - 1);

						if (columnIndex + 1 < _gameForm.HorizontalGameButtonsNumber)
							OpenCell(rowIndex, columnIndex + 1);

						if (rowIndex + 1 < _gameForm.VerticalGameButtonsNumber)
						{
							if (columnIndex > 0)
								OpenCell(rowIndex + 1, columnIndex - 1);

							OpenCell(rowIndex + 1, columnIndex);

							if (columnIndex + 1 < _gameForm.HorizontalGameButtonsNumber)
								OpenCell(rowIndex + 1, columnIndex + 1);
						}
					}
				}
				else
					State = GameState.PlayerLose;
			}
		}
		public void RemarkCell(int rowIndex, int columnIndex)
		{
			_gameField[rowIndex, columnIndex].IsMarked = !_gameField[rowIndex, columnIndex].IsMarked;
		}
		public void PlayerWinCheck()
		{
			int openCellsCount = 0;
			for (int i = 0; i < _gameForm.VerticalGameButtonsNumber; ++i)
				for (int j = 0; j < _gameForm.HorizontalGameButtonsNumber; ++j)
					if (_gameField[i, j].IsOpened)
					++openCellsCount;

			if (openCellsCount == (_gameForm.HorizontalGameButtonsNumber * _gameForm.VerticalGameButtonsNumber - _gameForm.MinesNumber))
				State = GameState.PlayerWin;
		}
		public void EndGame()
		{
			for (int i = 0; i < _gameForm.VerticalGameButtonsNumber; ++i)
				for (int j = 0; j < _gameForm.HorizontalGameButtonsNumber; ++j)
				{
					_gameField[i, j].IsOpened = true;
					int buttonIndex = i * _gameForm.HorizontalGameButtonsNumber + j;
					_gameForm.GameButtons[buttonIndex].Enabled = false;
					_gameForm.GameButtons[buttonIndex].BackColor = Color.Silver;
				}
		}

		private void SetGameFieldElementsValues()
		{
			for (int i = 0; i < _gameForm.VerticalGameButtonsNumber; ++i)
				for (int j = 0; j < _gameForm.HorizontalGameButtonsNumber; ++j)
					if (!_gameField[i, j].IsMine)
					{
						if (i > 0)
						{
							if (j > 0 && _gameField[i - 1, j - 1].IsMine)
								++_gameField[i, j].Value;
							if (_gameField[i - 1, j].IsMine)
								++_gameField[i, j].Value;
							if (j + 1 < _gameForm.HorizontalGameButtonsNumber && _gameField[i - 1, j + 1].IsMine)
								++_gameField[i, j].Value;
						}

						if (j > 0 && _gameField[i, j - 1].IsMine)
							++_gameField[i, j].Value;
						if (j + 1 < _gameForm.HorizontalGameButtonsNumber && _gameField[i, j + 1].IsMine)
							++_gameField[i, j].Value;

						if (i + 1 < _gameForm.VerticalGameButtonsNumber)
						{
							if (j > 0 && _gameField[i + 1, j - 1].IsMine)
								++_gameField[i, j].Value;
							if (_gameField[i + 1, j].IsMine)
								++_gameField[i, j].Value;
							if (j + 1 < _gameForm.HorizontalGameButtonsNumber && _gameField[i + 1, j + 1].IsMine)
								++_gameField[i, j].Value;
						}
					}
		}

		public GameCell this[int index1, int index2]
		{
			get { return _gameField[index1, index2]; }
		}
	}
}
