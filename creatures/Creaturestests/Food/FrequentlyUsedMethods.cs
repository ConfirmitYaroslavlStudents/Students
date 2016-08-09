using CellsAutomate.Food;
using CellsAutomate.Constants;
using System.Drawing;

namespace Creaturestests.Food
{
    public static class FrequentlyUsedMethods
    {
        public static void RaiseFoodLevelToConstant(FoodMatrix eatMatrix, Point point, int constant)
        {
            var counter = 0;

            while (counter < constant)
            {
                eatMatrix.AddFood(point);
                counter += FoodMatrixConstants.AddedFoodLevel;
            }
        }
    }
}
