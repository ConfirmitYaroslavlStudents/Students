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
		private const int GameButtonSize = 25;

		private GameField _gameField;
		private readonly Point StartingFormPosition = new Point(20, 50);

		public Button[] GameButtons { get; set; }
		public int VerticalGameButtonsNumber { get; private set; }
		public int HorizontalGameButtonsNumber { get; private set; }
		public int MinesNumber { get; private set; }

        public MainMineSweeperForm()
        {
            InitializeComponent();

			VerticalGameButtonsNumber = 10;
			HorizontalGameButtonsNumber = 10;
			MinesNumber = 10;
			GameButtons = new Button[VerticalGameButtonsNumber * HorizontalGameButtonsNumber];

			NewGame();
        }

		private void ClearForm()
		{
			if (GameButtons != null)
				foreach (Button button in GameButtons)
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
			for (int i = 0; i < VerticalGameButtonsNumber; ++i)
			{
				for (int j = 0; j < HorizontalGameButtonsNumber; ++j)
				{
					int buttonIndex = i * HorizontalGameButtonsNumber + j;
					GameButtons[buttonIndex] = MakeNewGameButton(location);
					this.Controls.Add(GameButtons[buttonIndex]);

					location.X += GameButtonSize;
				}

				location.Y += GameButtonSize;
				location.X = StartingFormPosition.X;
			}
		}

		private void NewGame()
		{
			ClearForm();

			InitializeButtons();

			_gameField = new GameField(this);
		}

		private void EndGame(string lastWords)
		{
			_gameField.EndGame();
			Invalidate();
			MessageBox.Show(lastWords, "Game over.");
		}

		private void GameButtonMouseClick(object sender, MouseEventArgs e)
        {
			int rowIndex = GameButtons.ToList().IndexOf((Button)sender) / HorizontalGameButtonsNumber;
			int columnIndex = GameButtons.ToList().IndexOf((Button)sender) % HorizontalGameButtonsNumber;

			if (e.Button == MouseButtons.Right)
				_gameField.RemarkCell(rowIndex, columnIndex);
			else
				_gameField.OpenCell(rowIndex, columnIndex);

			if (_gameField.State != GameState.PlayerLose)
				_gameField.PlayerWinCheck();
			else
			{
				EndGame("You are lose!");
				return;
			}

			if (_gameField.State == GameState.PlayerWin)
			{
				EndGame("You are win!");
				return;
			}

			Invalidate();
        }
		
		private void MainMineSweeperForm_Paint(object sender, PaintEventArgs e)
		{
			for (int i = 0; i < VerticalGameButtonsNumber; ++i)
				for (int j = 0; j < HorizontalGameButtonsNumber; ++j)
				{
					if (_gameField[i, j].IsOpened && _gameField[i, j].Value > 0)
						GameButtons[i * HorizontalGameButtonsNumber + j].Text = _gameField[i, j].Value.ToString();
					else if (_gameField[i, j].IsMarked)
						GameButtons[i * HorizontalGameButtonsNumber + j].Text = "P";
					else if ((_gameField.State == GameState.PlayerLose || _gameField.State == GameState.PlayerWin) &&
						_gameField[i, j].IsMine && !_gameField[i, j].IsMarked)
						GameButtons[i * HorizontalGameButtonsNumber + j].Text = "X";
					else
						GameButtons[i * HorizontalGameButtonsNumber + j].Text = "";
				}
		}

		private void newGameButton_Click(object sender, EventArgs e)
		{
			NewGame();
		}

    }
}
