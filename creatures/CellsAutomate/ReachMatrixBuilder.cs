using System.Collections.Generic;
using System.Drawing;

namespace CellsAutomate
{
    public class ReachMatrixBuilder
    {
        public bool[,] Build(
            bool[,] placeHoldersMatrix,
            int pointX,
            int pointY)
        {
            int n = placeHoldersMatrix.GetLength(0);
            int m = placeHoldersMatrix.GetLength(1);

            return Build(placeHoldersMatrix, new bool[n, m], pointX, pointY);
        }

        public bool[,] Build(
            bool[,] placeHoldersMatrix,
            bool[,] reachingMatrix,
            int pointX,
            int pointY)
        {
            int n = placeHoldersMatrix.GetLength(0);
            int m = placeHoldersMatrix.GetLength(1);

            var stack = new Stack<Point>();
            stack.Push(new Point(pointX, pointY));

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (
                    current.X >= 0
                    && current.X < m
                    && current.Y >= 0
                    && current.Y < n
                    && !placeHoldersMatrix[current.X, current.Y]
                    && !reachingMatrix[current.X, current.Y])
                {
                    reachingMatrix[current.X, current.Y] = true;

                    foreach (var point in ActionEx.GetPoints(current.X, current.Y))
                    {
                        stack.Push(point);
                    }
                }
            }

            return reachingMatrix;
        }
    }
}
