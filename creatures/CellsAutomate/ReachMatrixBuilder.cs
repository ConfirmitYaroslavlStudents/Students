using System.Collections.Generic;
using System.Drawing;

namespace CellsAutomate
{
    public class ReachMatrixBuilder
    {
        public int[,] Build(
            bool[,] placeHoldersMatrix,
            int pointX,
            int pointY)
        {
            int n = placeHoldersMatrix.GetLength(0);
            int m = placeHoldersMatrix.GetLength(1);

            return Build(placeHoldersMatrix, new int[n, m], pointX, pointY);
        }

        public int[,] Build(
            bool[,] placeHoldersMatrix,
            int[,] eatMatrix,
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
                    && eatMatrix[current.X, current.Y] == 0)
                {
                    eatMatrix[current.X, current.Y] += Creature.N;

                    foreach (var point in ActionEx.GetPoints(current.X, current.Y))
                    {
                        stack.Push(point);
                    }
                }
            }

            return eatMatrix;
        }
    }
}
