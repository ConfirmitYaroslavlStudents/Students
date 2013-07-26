using System;
using System.Drawing;
using System.Collections.Generic;

namespace PacMan
{
    public class Wall
    {
        private List<Rectangle> _wallCordinates;

        public List<Rectangle> WallCordinate
        {
            get { return _wallCordinates; }
            set { _wallCordinates = value; }
        }

        public Wall()
        {
            _wallCordinates = new List<Rectangle>();
        }

        public void Add(Rectangle newWall)
        {
            _wallCordinates.Add(newWall);
        }
    }
}
