using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PacMan
{
    public class PacMan
    {
        private Wall _levelWall;
        private Food _levelFood;
        private Enemy _enemyAtLevel;

        private Point _pacManCoordinate;
        private Point _newLocation;
        private Point _eyeCoordinate;
        private Point _mouthCoordinate;
        private bool _openOrClosedMouth=true;
        private static System.Threading.Timer _pacManMouthTimer;

        public Point PacManCoordinate
        {
            get { return _pacManCoordinate;}
        }
        public Point MouthCoordinate
        {
            get { return _mouthCoordinate; }
        }
        public Point EyeCoordinate
        {
            get { return _eyeCoordinate; }
        }
        public Food LevelFood
        {
            get { return _levelFood; }
        }

        public PacMan(Point pacManCoordinate,Wall wall, Enemy enemy)
        {
            this._pacManCoordinate = pacManCoordinate;
            this._levelWall = wall;
            this._levelFood = InitialiazeFood();
            this._enemyAtLevel = enemy;
            this._eyeCoordinate = new Point(_pacManCoordinate.X + 35, _pacManCoordinate.Y + 25);
            this._mouthCoordinate = new Point(50, 80);
            _pacManMouthTimer = new System.Threading.Timer(mouthState, null, 300, 300);
        }

        public void makeMove(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                doStep(new Point(_pacManCoordinate.X - 50, _pacManCoordinate.Y));
                _eyeCoordinate = new Point(_pacManCoordinate.X+15,_pacManCoordinate.Y+5);
                _mouthCoordinate = new Point(140,80);
            }
            if (e.KeyCode == Keys.D)
            {
                doStep(new Point(_pacManCoordinate.X + 50, _pacManCoordinate.Y));
                _eyeCoordinate = new Point(_pacManCoordinate.X + 25, _pacManCoordinate.Y + 5);
                _mouthCoordinate = new Point(-40, 80);
            }
            if (e.KeyCode == Keys.W)
            {
                doStep(new Point(_pacManCoordinate.X, _pacManCoordinate.Y - 50));
                _eyeCoordinate = new Point(_pacManCoordinate.X + 35, _pacManCoordinate.Y + 15);
                _mouthCoordinate = new Point(-130, 80);
            }
            if (e.KeyCode == Keys.S)
            {
                doStep(new Point(_pacManCoordinate.X, _pacManCoordinate.Y + 50));
                _eyeCoordinate = new Point(_pacManCoordinate.X + 35, _pacManCoordinate.Y + 25);
                _mouthCoordinate = new Point(50, 80);
            }
        }
        private void doStep(Point newLocation)
        {
            _newLocation = newLocation;
            if (newLocation.X >= 0 && newLocation.X <= 400 && newLocation.Y >= 0 && newLocation.Y <= 350)
            {
                if (isOutOfWall())
                {
                    _pacManCoordinate = newLocation;
                    eatFood();
                }
            }
        }
        private bool isOutOfWall()
        {
            foreach (Rectangle rect in _levelWall.WallCordinate)
            {
                if (rect.Contains(_newLocation))
                    return false;
            }
            return true;
        }
        private void eatFood()
        {
            foreach (Point foodCoordinate in _levelFood.FoodCoordinates)
            {
                if (foodCoordinate == _newLocation)
                {
                    _levelFood.FoodCoordinates.Remove(foodCoordinate);
                    break;
                }
            }
        }
        public bool isWin()
        {
            if (_levelFood.FoodCoordinates.Count==0)
                return true;
            return false;
        }
        public bool isLoose()
        {
            if (_pacManCoordinate == _enemyAtLevel.EnemyCoordinate)
                return true;
            return false;
        }
        public bool isOpenMouth()
        {
            return _openOrClosedMouth;
        }
        public void mouthState(object sender)
        {
            _openOrClosedMouth = !_openOrClosedMouth;
        }
        private Food InitialiazeFood()
        {
            Point temp = new Point(0, 0);
            Food result = new Food();
            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (isFood(temp))
                    {
                        result.Add(temp);
                    }
                    temp.Y = temp.Y + 50;
                }
                temp.Y = 0;
                temp.X = temp.X + 50;
            }
            return result;
        }
        private bool isFood(Point point)
        {
            foreach (Rectangle rect in _levelWall.WallCordinate)
            {
                if (rect.Contains(point))
                    return false;
            }
            return true;
        }
        
    }
}
