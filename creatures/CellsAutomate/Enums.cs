using System;
using System.Drawing;

namespace CellsAutomate
{
    public enum ActionEnum
    {
        Die,
        MakeChild,
        Go
    }

    public enum DirectionEnum
    {
        Up,
        Right,
        Down,
        Left,
        Stay
    }

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

        public static DirectionEnum DirectionByPoint(Point start, Point finish)
        {
            var xOffset = finish.X - start.X;
            var yOffset = finish.Y - start.Y;

            if (xOffset == -1 && yOffset == 0) return DirectionEnum.Left;
            if (xOffset == 1 && yOffset == 0) return DirectionEnum.Right;
            if (xOffset == 0 && yOffset == 1) return DirectionEnum.Down;
            if (xOffset == 0 && yOffset == -1) return DirectionEnum.Up;

            throw new ArgumentException();
        }

        public static Point[] GetPoints(int i, int j)
        {
            return new[] { new Point(i + 1, j), new Point(i, j + 1), new Point(i - 1, j), new Point(i, j - 1) };
        }

        public static bool IsValid(Point x, int length, int width)
        {
            if (x.X < 0) return false;
            if (x.Y < 0) return false;
            if (x.X >= length) return false;
            if (x.Y >= width) return false;

            return true;
        }

        public static int DirectionByPointForLanguage(Point start, Point finish)
        {
            var xOffset = finish.X - start.X;
            var yOffset = finish.Y - start.Y;

            if (xOffset == 0 && yOffset == -1) return 0;
            if (xOffset == 1 && yOffset == 0) return 1;
            if (xOffset == 0 && yOffset == 1) return 2;
            if (xOffset == -1 && yOffset == 0) return 3;

            throw new ArgumentException();
        }
    }
}