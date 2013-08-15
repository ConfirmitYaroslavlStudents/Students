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
        private Point _pacManCoordinate;
        private Point _eyeCoordinate;
        private Point _mouthCoordinate;
        private bool _openOrClosedMouth = true;
        private static System.Threading.Timer _pacManMouthTimer;

        public readonly Point eyeCoordinateLeft = new Point(15, 5);
        public readonly Point eyeCoordinateRight = new Point(25, 5);
        public readonly Point eyeCoordinateTop = new Point(35, 15);
        public readonly Point eyeCoordinateDown = new Point(35, 25);
        public readonly Point mouthCoordinateLeft = new Point(140, 80);
        public readonly Point mouthCoordinateRight = new Point(-40, 80);
        public readonly Point mouthCoordinateTop = new Point(-130, 80);
        public readonly Point mouthCoordinateDown = new Point(50, 80);

        public Point PacManCoordinate
        {
            get { return _pacManCoordinate;}
        }
        public Point MouthCoordinate
        {
            get { return _mouthCoordinate; }
            set { _mouthCoordinate = value; }
        }
        public Point EyeCoordinate
        {
            get { return _eyeCoordinate; }
            set { _eyeCoordinate = value; }
        }
        public int PacManStepInPixels
        {
            get { return GameSettings.CellSize; }
        }

        public PacMan(Point pacManCoordinate)
        {
            this._pacManCoordinate = pacManCoordinate;
            this._eyeCoordinate = new Point(_pacManCoordinate.X + eyeCoordinateTop.X, _pacManCoordinate.Y + eyeCoordinateTop.Y);
            this._mouthCoordinate = mouthCoordinateTop;
            _pacManMouthTimer = new System.Threading.Timer(MouthState, null, 0, GameSettings.PacManMouthSpeed);
        }

        public void DoStep(Point newLocation)
        {
            _pacManCoordinate = newLocation;
        }
        public void EatFood(Food levelFood)
        {
            foreach (Point foodCoordinate in levelFood.FoodCoordinates)
            {
                if (foodCoordinate == _pacManCoordinate)
                {
                    levelFood.FoodCoordinates.Remove(foodCoordinate);
                    break;
                }
            }
        }
        public bool IsOpenMouth()
        {
            return _openOrClosedMouth;
        }
        public void MouthState(object sender)
        {
            _openOrClosedMouth = !_openOrClosedMouth;
        }
    }
}
