using System.Collections.Generic;
using System.Drawing;
using CellsAutomate.Constants;

namespace CellsAutomate.Food
{
    public class FillingFromCornersByWavesStrategy : IFoodDistributionStrategy
    {
        public void Build(bool[,] creatures, FoodMatrix eatMatrix)
        {
            Build(creatures, 0, 0, eatMatrix);
            Build(creatures, 0, creatures.GetLength(0) - 1, eatMatrix);
            Build(creatures, creatures.GetLength(1) - 1, 0, eatMatrix);
            Build(creatures, creatures.GetLength(1) - 1, creatures.GetLength(0) - 1, eatMatrix);
        }

        private void Build(
            bool[,] creaturesMatrix,
            int pointX,
            int pointY,
            FoodMatrix eatMatrix)
        {
            int length = eatMatrix.Length;
            int height = eatMatrix.Height;

            var stack = new Stack<Point>();
            var visitedCells = new bool[length, height];
            
            stack.Push(new Point(pointX, pointY));

            while (stack.Count != 0)
            {
                var current = stack.Pop();
                visitedCells[current.X, current.Y] = true;

                if (DirectionEx.IsValid(current, length, height)
                    && !creaturesMatrix[current.X, current.Y])
                {
                    if (!eatMatrix.HasMaxFoodLevel(current))
                        eatMatrix.AddFood(current);

                    foreach (var point in DirectionEx.GetPoints(current.X, current.Y))
                    {
                        if(DirectionEx.IsValid(point, length, height) && !visitedCells[point.X, point.Y])
                            stack.Push(point);
                    }
                }
            }
        }
    }
}
