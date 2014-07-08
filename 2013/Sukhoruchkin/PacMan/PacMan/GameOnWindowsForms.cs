using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class GameOnWindowsForms : Form
    {
        private Game _newGame;
        public GameOnWindowsForms()
        {
            InitializeComponent();
            _newGame = new Game(1);
            AdjustTheWindowSize();
        }
        private void AdjustTheWindowSize()
        {
            this.Width = _newGame.Map.NumberColimns * GameSettings.WidthCell + 18;
            this.Height = _newGame.Map.NumberLines * GameSettings.HeightCell + 40;
        }
        private void Game_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Rectangle wallCoordinate in _newGame.Map.LevelWall.WallCordinate)
            {
                g.FillRectangle(Brushes.Aquamarine, wallCoordinate);
            }
            foreach (Point foodcoordinate in _newGame.Map.LevelFood.FoodCoordinates)
            {
                g.FillEllipse(Brushes.White, foodcoordinate.X + 20, foodcoordinate.Y + 20, 10, 10);
            }
            foreach (Point enemyCoordinate in _newGame.Enemys.EnemyCoordinates)
            {
                g.FillEllipse(Brushes.Indigo, enemyCoordinate.X, enemyCoordinate.Y, GameSettings.EnemyHeight, GameSettings.EnemyWidth);
            }
           
            g.FillEllipse(Brushes.Yellow, _newGame.PacMan.PacManCoordinate.X, _newGame.PacMan.PacManCoordinate.Y, GameSettings.PacManHeight, GameSettings.PacManWidth);
            g.FillEllipse(Brushes.Black, _newGame.PacMan.EyeCoordinate.X, _newGame.PacMan.EyeCoordinate.Y, 10, 10);
            if (_newGame.PacMan.IsOpenMouth())
                g.FillPie(Brushes.Black, _newGame.PacMan.PacManCoordinate.X, _newGame.PacMan.PacManCoordinate.Y, GameSettings.PacManHeight, GameSettings.PacManWidth, _newGame.PacMan.MouthCoordinate.X, _newGame.PacMan.MouthCoordinate.Y);
            if (_newGame.IsLoose())
            {
                MessageBox.Show("GameOver");
                this.Close();
            }
            if (_newGame.IsLevelComplete())
            {
                _newGame.CurrentLevel++;
                if (_newGame.IsWin())
                {
                    MessageBox.Show("You WIN!");
                    this.Close();
                }
                _newGame = new Game(_newGame.CurrentLevel);
                MessageBox.Show("Level: " + _newGame.CurrentLevel.ToString());
                AdjustTheWindowSize();
            }
            Invalidate();
        }
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "A")
            {
                _newGame.KeyDown(3);
            }
            if (e.KeyCode.ToString() == "D")
            {
                _newGame.KeyDown(2);
            }
            if (e.KeyCode.ToString() == "W")
            {
                _newGame.KeyDown(1);
            }
            if (e.KeyCode.ToString() == "S")
            {
                _newGame.KeyDown(0);
            }
        }
    }
}
