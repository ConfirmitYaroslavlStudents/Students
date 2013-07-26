using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Game : Form
    {
        private PacMan _pacMan;
        public Enemy _enemy;
        private Wall _levelWall;

        public Game()
        {
            InitializeComponent();
            FirstLevel();
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Rectangle r in _levelWall.WallCordinate)
            {
                g.FillRectangle(Brushes.Aquamarine, r);
            }
            foreach (Point p in _pacMan.LevelFood.FoodCoordinates)
            {
                g.FillEllipse(Brushes.White, p.X + 20, p.Y + 20, 10, 10);
            }
            g.FillEllipse(Brushes.Indigo, _enemy.EnemyCoordinate.X, _enemy.EnemyCoordinate.Y, 50, 50);
            g.FillEllipse(Brushes.Yellow, _pacMan.PacManCoordinate.X, _pacMan.PacManCoordinate.Y, 50, 50);
            g.FillEllipse(Brushes.Black, _pacMan.EyeCoordinate.X, _pacMan.EyeCoordinate.Y, 10, 10);
            if(_pacMan.isOpenMouth())
            g.FillPie(Brushes.Black, _pacMan.PacManCoordinate.X, _pacMan.PacManCoordinate.Y, 50, 50, _pacMan.MouthCoordinate.X, _pacMan.MouthCoordinate.Y);
            if (_pacMan.isLoose())
            {
                MessageBox.Show("Потрачено");
                this.Close();
            }
            if (_pacMan.isWin())
            {
                MessageBox.Show("Победа");
                this.Close();
            }
            Invalidate();
        }
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            _pacMan.makeMove(e);
        }
        private void FirstLevel()
        {
            _enemy = new Enemy(new Point(200, 150));

            _enemy.AddPointToRoute(new Point(200,100));
            _enemy.AddPointToRoute(new Point(150, 100));
            _enemy.AddPointToRoute(new Point(100, 100));
            _enemy.AddPointToRoute(new Point(100, 150));
            _enemy.AddPointToRoute(new Point(100, 200));
            _enemy.AddPointToRoute(new Point(100, 250));
            _enemy.AddPointToRoute(new Point(150, 250));
            _enemy.AddPointToRoute(new Point(200, 250));
            _enemy.AddPointToRoute(new Point(250, 250));
            _enemy.AddPointToRoute(new Point(300, 250));
            _enemy.AddPointToRoute(new Point(300, 200));
            _enemy.AddPointToRoute(new Point(300, 150));
            _enemy.AddPointToRoute(new Point(300, 100));
            _enemy.AddPointToRoute(new Point(250, 100));
            _enemy.AddPointToRoute(new Point(200, 100));
            _enemy.AddPointToRoute(new Point(200, 150));

            _levelWall = new Wall();
            _levelWall.Add(new Rectangle(50, 50, 50, 100));
            _levelWall.Add(new Rectangle(50, 50, 150, 50));
            _levelWall.Add(new Rectangle(50, 200, 50, 150));
            _levelWall.Add(new Rectangle(50, 300, 150, 50));
            _levelWall.Add(new Rectangle(250, 50, 150, 50));
            _levelWall.Add(new Rectangle(350, 50, 50, 100));
            _levelWall.Add(new Rectangle(350, 200, 50, 100));
            _levelWall.Add(new Rectangle(250, 300, 150, 50));
            _levelWall.Add(new Rectangle(150, 150, 50, 100));
            _levelWall.Add(new Rectangle(150, 200, 150, 50));
            _levelWall.Add(new Rectangle(250, 150, 50, 100));


            _pacMan = new PacMan(new Point(0, 0), _levelWall, _enemy);
        }

    }
}
