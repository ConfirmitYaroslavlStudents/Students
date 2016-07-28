using System;
using System.Drawing;

namespace CellsAutomate
{
    public class CreatureDeadException : ApplicationException
    {
        public Point Coordinates { get; private set; }

        public CreatureDeadException(Point deadPoint) : base("Point is dead!")
        {
            Coordinates = deadPoint;
        }
    }
}
