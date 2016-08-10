using CellsAutomate.Food;
using CellsAutomate.Constants;
using System.Drawing;

namespace Creaturestests.Food
{
    public static class FrequentlyUsedMethods
    {
        public static void RaiseFoodLevelToConstantWithAddFood(FoodMatrix eatMatrix, Point point, int constant)
        {
            var counter = 0;

            while (counter < constant)
            {
                eatMatrix.AddFood(point);
                counter += FoodMatrixConstants.AddedFoodLevel;
            }
        }

        public static void RaiseFoodLevelToConstantWithBuild(FoodMatrix eatMatrix, bool[,] creatures, int constant, int frequency)
        {
            var counter = 0;
            var i = 0;

            while (counter < constant)
            {
                eatMatrix.Build(creatures);
                if (i % frequency == 0)
                    counter += FoodMatrixConstants.AddedFoodLevel;
                i++;
            }
        }
    }
}
