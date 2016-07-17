using System.Collections.Generic;
using System.Drawing;

namespace CellsAutomate
{
    public class FoodMatrix
    {
        private int[,] _matrix;
        public int Length { get; set; }
        public int Width { get; set; }

        public FoodMatrix(int length, int width)
        {
            _matrix = new int[length,width];
        }

        public bool HasFood(Point currentPoint)
        {
            if (_matrix[currentPoint.X, currentPoint.Y] != 0)
                return true;
            return false;
        }

        private void AddFood(Point currentPoint, int embaddedFood)
        {
            _matrix[currentPoint.X, currentPoint.Y] += embaddedFood;
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
                    && !HasFood(current))
                {
                    AddFood(current, Creature.FoodLevel);

                    foreach (var point in DirectionEx.GetPoints(current.X, current.Y))
                    {
                        stack.Push(point);
                    }
                }
            }
        }
    }
}
