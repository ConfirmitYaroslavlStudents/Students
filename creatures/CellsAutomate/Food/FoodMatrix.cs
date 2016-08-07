using System.Drawing;
using CellsAutomate.Constants;

namespace CellsAutomate.Food
{
    public class FoodMatrix
    {
        private readonly int[,] _matrix;

        private readonly IFoodDistributionStrategy _strategy;

        public int Length => _matrix.GetLength(0);
        public int Height => _matrix.GetLength(1);

        public FoodMatrix(int length, int height)
        {
            _matrix = new int[length, height];
            _strategy = new FillingFromCornersByWavesStrategy();
        }

        public bool HasOneBite(Point currentPoint)
        {
            return GetLevelOfFood(currentPoint) >= CreatureConstants.OneBite;
        }

        public bool HasMaxFoodLevel(Point currentPoint)
        {
            return GetLevelOfFood(currentPoint) >= FoodMatrixConstants.MaxFoodLevel;
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

        private int GetLevelOfFood(Point currentPoint)
        {
            return _matrix[currentPoint.X, currentPoint.Y];
        }

        public void Build(bool[,] creatures)
        {
            _strategy.Build(creatures, this);
        }
    }
}
