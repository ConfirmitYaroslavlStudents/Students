using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    public class Food
    {
        private List<Point> _foodCoordinates;

        public List<Point> FoodCoordinates
        {
            get { return _foodCoordinates; }
            set { _foodCoordinates = value; }
        }

        public Food()
        {
            _foodCoordinates = new List<Point>();
        }

        public void Add(Point newFood)
        {
            _foodCoordinates.Add(newFood);
        }
    }
}
