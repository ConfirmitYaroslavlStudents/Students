using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace CellsAutomate
{
    public class FoodMatrix
    {
        private readonly int[,] _matrix;

        public int Length => _matrix.GetLength(0);
        public int Height => _matrix.GetLength(1);

        public FoodMatrix(int length, int height)
        {
            _matrix = new int[length, height];
        }

        public bool HasFood(Point currentPoint)
        {
            if (_matrix[currentPoint.X, currentPoint.Y] != 0)
                return true;
            return false;
        }

        private void AddFood(Point currentPoint, int embeddedFood)
        {
            _matrix[currentPoint.X, currentPoint.Y] += embeddedFood;
        }

        public bool TakeFood(Point currentPoint, int takingFood)
        {
            if (takingFood > _matrix[currentPoint.X, currentPoint.Y])
                return false;
            _matrix[currentPoint.X, currentPoint.Y] -= takingFood;
            return true;
        }

        public void Build(
            bool[,] creaturesMatrix,
            int pointX,
            int pointY)
        {
            int length = creaturesMatrix.GetLength(0);
            int width = creaturesMatrix.GetLength(1);

            var stack = new Stack<Point>();
            stack.Push(new Point(pointX, pointY));

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (
                    current.X >= 0
                    && current.X < width
                    && current.Y >= 0
                    && current.Y < length
                    && !creaturesMatrix[current.X, current.Y]
                    && _matrix[current.X, current.Y] <= Constants.MaxFoodLevel)
                {
                    AddFood(current, Constants.FoodLevel);

                    foreach (var point in DirectionEx.GetPoints(current.X, current.Y))
                    {
                        stack.Push(point);
                    }
                }
            }
        }
    }
}
