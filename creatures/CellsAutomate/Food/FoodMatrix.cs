using System.Drawing;
using CellsAutomate.Constants;

namespace CellsAutomate.Food
{
    public class FoodMatrix
    {
        private readonly int[,] _matrix;

        private readonly IStrategyForBuild _strategy;

        public int Length => _matrix.GetLength(0);
        public int Height => _matrix.GetLength(1);

        public FoodMatrix(int length, int height, IStrategyForBuild strategy)
        {
            _matrix = new int[length, height];
            _strategy = strategy;
        }

        public bool HasFood(Point currentPoint)
        {
            if (_matrix[currentPoint.X, currentPoint.Y] != 0)
                return true;
            return false;
        }

        public void AddFood(Point currentPoint)
        {
            _matrix[currentPoint.X, currentPoint.Y] += FoodMatrixConstants.FoodLevel;
        }

        public bool TakeFood(Point currentPoint)
        {
            if (CreatureConstants.OneBite > _matrix[currentPoint.X, currentPoint.Y])
                return false;
            _matrix[currentPoint.X, currentPoint.Y] -= CreatureConstants.OneBite;
            return true;
        }

        public int GetLevelOfFood(Point currentPoint)
        {
            return _matrix[currentPoint.X, currentPoint.Y];
        }

        public void Build(bool[,] creatures)
        {
            _strategy.Build(creatures, this);
        }
    }
}
