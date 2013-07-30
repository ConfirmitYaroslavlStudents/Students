using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            this.Width = _newGame.Map.NumberColimns * 50 + 18;
            this.Height = _newGame.Map.NumberLines * 50 + 40;
        }
        private void Game_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Rectangle r in _newGame.Map.LevelWall.WallCordinate)
            {
                g.FillRectangle(Brushes.Aquamarine, r);
            }
            foreach (Point p in _newGame.Map.LevelFood.FoodCoordinates)
            {
                g.FillEllipse(Brushes.White, p.X + 20, p.Y + 20, 10, 10);
            }
            g.FillEllipse(Brushes.Indigo, _newGame.Enemy.EnemyCoordinate.X, _newGame.Enemy.EnemyCoordinate.Y, 50, 50);
            g.FillEllipse(Brushes.Yellow, _newGame.PacMan.PacManCoordinate.X, _newGame.PacMan.PacManCoordinate.Y, 50, 50);
            g.FillEllipse(Brushes.Black, _newGame.PacMan.EyeCoordinate.X, _newGame.PacMan.EyeCoordinate.Y, 10, 10);
            if (_newGame.PacMan.IsOpenMouth())
                g.FillPie(Brushes.Black, _newGame.PacMan.PacManCoordinate.X, _newGame.PacMan.PacManCoordinate.Y, 50, 50, _newGame.PacMan.MouthCoordinate.X, _newGame.PacMan.MouthCoordinate.Y);
            if (_newGame.IsLoose())
            {
                MessageBox.Show("GameOver");
                this.Close();
            }
            if (_newGame.IsWin())
            {
                _newGame = new Game(++_newGame._currentLevel);
                MessageBox.Show("Level: " + _newGame._currentLevel.ToString());
                AdjustTheWindowSize();
            }
            Invalidate();
        }
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                Point newPacManLocation = new Point(_newGame.PacMan.PacManCoordinate.X - _newGame.PacMan.PacManStepInPixels, _newGame.PacMan.PacManCoordinate.Y);
                _newGame.MakeMovePacMan(newPacManLocation,_newGame.PacMan.eyeCoordinateLeft,_newGame.PacMan.mouthCoordinateLeft);
            }
            if (e.KeyCode == Keys.D)
            {
                Point newPacManLocation = new Point(_newGame.PacMan.PacManCoordinate.X + _newGame.PacMan.PacManStepInPixels, _newGame.PacMan.PacManCoordinate.Y);
                _newGame.MakeMovePacMan(newPacManLocation, _newGame.PacMan.eyeCoordinateRight, _newGame.PacMan.mouthCoordinateRight);
            }
            if (e.KeyCode == Keys.W)
            {
                Point newPacManLocation = new Point(_newGame.PacMan.PacManCoordinate.X, _newGame.PacMan.PacManCoordinate.Y - _newGame.PacMan.PacManStepInPixels);
                _newGame.MakeMovePacMan(newPacManLocation, _newGame.PacMan.eyeCoordinateTop, _newGame.PacMan.mouthCoordinateTop);
            }
            if (e.KeyCode == Keys.S)
            {
                Point newPacManLocation = new Point(_newGame.PacMan.PacManCoordinate.X, _newGame.PacMan.PacManCoordinate.Y + _newGame.PacMan.PacManStepInPixels);
                _newGame.MakeMovePacMan(newPacManLocation, _newGame.PacMan.eyeCoordinateDown, _newGame.PacMan.mouthCoordinateDown);
            }
        }
        
    }
}
