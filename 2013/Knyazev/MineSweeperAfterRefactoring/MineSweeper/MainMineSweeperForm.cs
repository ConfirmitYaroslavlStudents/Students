using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MainMineSweeperForm : Form
    {
		private readonly Point StartingFormPosition = new Point(20, 50);
		private const int GameButtonSize = 25;
		
		private Button[] _gameButtons;
		private Game _game;
		private GameSettings _gameSettings;

        public MainMineSweeperForm()
        {
            InitializeComponent();
			NewGame();
        }

		private void ClearForm()
		{
			if (_gameButtons != null)
				foreach (Button button in _gameButtons)
					this.Controls.Remove(button);
		}

		private Button MakeNewGameButton(Point location)
		{
			Button button = new Button();
			button.Text = "";
			button.Location = new Point(location.X, location.Y);
			button.Size = new Size(GameButtonSize, GameButtonSize);
			button.BackColor = Color.White;
			button.MouseDown += GameButtonMouseClick;
			return button;
		}

		private void InitializeButtons()
		{
			Point location = new Point(StartingFormPosition.X, StartingFormPosition.Y);
			for (int i = 0; i < _gameSettings.GameFieldHeight; ++i)
			{
				for (int j = 0; j < _gameSettings.GameFieldWidth; ++j)
				{
					int buttonIndex = i * _gameSettings.GameFieldWidth + j;
					_gameButtons[buttonIndex] = MakeNewGameButton(location);
					this.Controls.Add(_gameButtons[buttonIndex]);

					location.X += GameButtonSize;
				}

				location.Y += GameButtonSize;
				location.X = StartingFormPosition.X;
			}
		}

		private void NewGame()
		{
			ClearForm();

			_gameSettings = new GameSettings(10, 10, 10);
			_gameButtons = new Button[_gameSettings.GameFieldHeight * _gameSettings.GameFieldWidth];
			_game = new Game(_gameSettings);

			InitializeButtons();
		}

		private void EndGame(string lastWords)
		{
			_game.EndGame();

			foreach (Button button in _gameButtons)
			{
				button.Enabled = false;
				button.BackColor = Color.Silver;
			}

			Invalidate();
			MessageBox.Show(lastWords, "Game over.");
		}

		private void GameButtonMouseClick(object sender, MouseEventArgs e)
        {
			int buttonIndex = _gameButtons.ToList().IndexOf((Button)sender);
			int rowIndex = buttonIndex / _gameSettings.GameFieldWidth;
			int columnIndex = buttonIndex % _gameSettings.GameFieldWidth;

			if (e.Button == MouseButtons.Right)
				RefreshButton(_game.RemarkCell(rowIndex, columnIndex));
			else
				RefreshButtons(_game.OpenCells(rowIndex, columnIndex));

			if (_game.State != GameState.PlayerLose)
			{
				if (_game.PlayerWins())
				{
					EndGame("You are win!");
					return;
				}
			}
			else
			{
				EndGame("You are lose!");
				return;
			}

			Invalidate();
        }

		private void RefreshButtons(List<Cell> buttonsLocationsList)
		{
			foreach (Cell location in buttonsLocationsList)
				RefreshButton(location);
		}

		private void RefreshButton(Cell location)
		{
			int buttonIndex = location.Row * _gameSettings.GameFieldHeight + location.Column;
			if (_game.Field[location.Row, location.Column].IsOpened)
			{
				_gameButtons[buttonIndex].Enabled = false;
				_gameButtons[buttonIndex].BackColor = Color.Silver;
			}
		}

		private void MainMineSweeperForm_Paint(object sender, PaintEventArgs e)
		{
			for (int i = 0; i < _gameSettings.GameFieldHeight; ++i)
				for (int j = 0; j < _gameSettings.GameFieldWidth; ++j)
					if (_game.Field[i, j].IsOpened && _game.Field[i, j].Value > 0)
						_gameButtons[i * _gameSettings.GameFieldWidth + j].Text = _game.Field[i, j].Value.ToString();
					else if (_game.Field[i, j].IsMarked)
						_gameButtons[i * _gameSettings.GameFieldWidth + j].Text = "P";
					else if ((_game.State == GameState.PlayerLose || _game.State == GameState.PlayerWin) &&
						_game.Field[i, j].IsMine && !_game.Field[i, j].IsMarked)
						_gameButtons[i * _gameSettings.GameFieldWidth + j].Text = "X";
					else
						_gameButtons[i * _gameSettings.GameFieldWidth + j].Text = "";
		}

		private void newGameButton_Click(object sender, EventArgs e)
		{
			NewGame();
		}
    }
}
