using System;
using System.Drawing;

namespace CellsAutomate.Tools
{
    static class DirectionEx
    {
        public static Point PointByDirection(DirectionEnum dirEnum, Point start)
        {
            switch (dirEnum)
            {
                case DirectionEnum.Left:
                    return new Point(start.X - 1, start.Y);
                case DirectionEnum.Right:
                    return new Point(start.X + 1, start.Y);
                case DirectionEnum.Up:
                    return new Point(start.X, start.Y - 1);
                case DirectionEnum.Down:
                    return new Point(start.X, start.Y + 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dirEnum), dirEnum, null);
            }
        }

        public static DirectionEnum DirectionByPoints(Point start, Point finish)
        {
            var xOffset = finish.X - start.X;
            var yOffset = finish.Y - start.Y;

            if (xOffset == -1 && yOffset == 0) return DirectionEnum.Left;
            if (xOffset == 1 && yOffset == 0) return DirectionEnum.Right;
            if (xOffset == 0 && yOffset == 1) return DirectionEnum.Down;
            if (xOffset == 0 && yOffset == -1) return DirectionEnum.Up;

            throw new ArgumentException();
        }

        public static int DirectionByPointsWithNumber(Point start, Point finish)
        {
            var xOffset = finish.X - start.X;
            var yOffset = finish.Y - start.Y;

            if (xOffset == 0 && yOffset == -1) return 0;
            if (xOffset == 1 && yOffset == 0) return 1;
            if (xOffset == 0 && yOffset == 1) return 2;
            if (xOffset == -1 && yOffset == 0) return 3;

            throw new ArgumentException();
        }

        public static DirectionEnum DirectionByNumber(int number)
        {
            switch (number)
            {
                case 0: return DirectionEnum.Stay;
                case 1: return DirectionEnum.Up;
                case 2: return DirectionEnum.Right;
                case 3: return DirectionEnum.Down;
                case 4: return DirectionEnum.Left;
                default: throw new Exception();
            }
        }
    }
}
