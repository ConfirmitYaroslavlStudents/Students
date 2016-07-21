using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace CellsAutomate
{
    public class CreatureIsDeadException : ApplicationException
    {
        public Point Coordinates { get; private set; }

        public CreatureIsDeadException(Point deadPoint) : base("Point is dead!")
        {
            Coordinates = deadPoint;
        }
    }
}
