using System.Drawing;

namespace CellsAutomate.Tools
{
    class CommonMethods
    {
        public static bool IsFree(Point currentPoint, Membrane[,] creatures)
        {
            return creatures[currentPoint.X, currentPoint.Y] == null;
        }

        public static bool IsValid(Point x, int length, int height)
        {
            if (x.X < 0) return false;
            if (x.Y < 0) return false;
            if (x.X >= length) return false;
            if (x.Y >= height) return false;

            return true;
        }

        public static bool IsValidAndFree(Point position, Membrane[,] creatures)
        {
            return IsValid(position, creatures.GetLength(0), creatures.GetLength(1))
                   && IsFree(position, creatures);
        }

        public static Point[] GetPoints(Point position)
        {
            int i = position.X;
            int j = position.Y;
            return new[] { new Point(i + 1, j), new Point(i, j + 1), new Point(i - 1, j), new Point(i, j - 1) };
        }
    }
}
