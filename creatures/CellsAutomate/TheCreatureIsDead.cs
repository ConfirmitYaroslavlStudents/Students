using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace CellsAutomate
{
    [Serializable]
    public class TheCreatureIsDeadException : ApplicationException
    {
        public Point _coordinates;

        public TheCreatureIsDeadException() { }

        public TheCreatureIsDeadException(string message) : base(message) { }

        public TheCreatureIsDeadException(string message, Exception inner) : base(message, inner){}

        protected TheCreatureIsDeadException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public TheCreatureIsDeadException(Point deadPoint) : this("Point is dead!")
        {
            _coordinates = deadPoint;
        }
    }
}
