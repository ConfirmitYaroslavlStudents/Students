using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    public class Map
    {
        private Wall _levelWall;
        private Food _levelFood;
        private int _numberColumns;
        private int _numberLines;

        public Food LevelFood
        {
            get { return _levelFood; }
        }
        public Wall LevelWall
        {
            get { return _levelWall; }
        }
        public int NumberColimns
        {
            get { return _numberColumns; }
        }
        public int NumberLines
        {
            get { return _numberLines; }
        }

        public Map(Wall levelWall,int numberColumns,int numberLines)
        {
            this._numberColumns = numberColumns;
            this._numberLines = numberLines;
            this._levelWall = levelWall;
            this._levelFood = InitialiazeFood();
        }

        public bool IsOutOfWall(Point _newLocation)
        {
            foreach (Rectangle rect in _levelWall.WallCordinate)
            {
                if (rect.Contains(_newLocation))
                    return false;
            }
            return true;
        }
        private Food InitialiazeFood()
        {
            Point temp = new Point(0, 0);
            Food result = new Food();
            for (int j = 0; j < _numberColumns; j++)
            {
                for (int k = 0; k < _numberLines; k++)
                {
                    if (IsFood(temp))
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
        private bool IsFood(Point point)
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
