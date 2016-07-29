using System;
using System.Drawing;
using CellsAutomate.Creatures;

namespace CellsAutomate
{
    public enum ActionEnum
    {
        Die,
        MakeChild,
        Go,
        Eat
    }

    public enum DirectionEnum
    {
        Stay,
        Up,
        Right,
        Down,
        Left
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

        public static Point[] GetPoints(int i, int j)
        {
            return new[] { new Point(i + 1, j), new Point(i, j + 1), new Point(i - 1, j), new Point(i, j - 1) };
        }

        public static bool IsValid(Point x, int length, int height)
        {
            if (x.X < 0) return false;
            if (x.Y < 0) return false;
            if (x.X >= length) return false;
            if (x.Y >= height) return false;

            return true;
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

        public static bool IsFree(Point currentPoint, BaseCreature[,] creatures)
        {
            if (creatures[currentPoint.X, currentPoint.Y] == null)
                return true;
            return false;
        }
    }
}