using System.Drawing;
using CellsAutomate.Constants;
using CellsAutomate.Food;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.Food
{
    [TestClass()]
    public class FillingOfEntireFieldStrategyTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            var frequency = 10;
            var eatMatrix = new FoodMatrix(2, 2, new FillingOfEntireFieldStrategy(frequency));
            var creatures = new bool[2, 2];

            for (int k = 0; k < 10; k++)
            {
                if (k != 9 && FoodMatrixConstants.AddedFoodLevel <= FoodMatrixConstants.MaxFoodLevel)
                {
                    eatMatrix.Build(creatures);
                    for (int i = 0; i < eatMatrix.Length; i++)
                        for (int j = 0; j < eatMatrix.Height; j++)
                            Assert.IsFalse(eatMatrix.HasMaxFoodLevel(new Point(i, j)));
                }
                
                if (k == 9)
                {
                    FrequentlyUsedMethods.RaiseFoodLevelToConstantWithBuild(eatMatrix, creatures, FoodMatrixConstants.MaxFoodLevel, frequency);
                    for (int i = 0; i < eatMatrix.Length; i++)
                        for (int j = 0; j < eatMatrix.Height; j++)
                            Assert.IsTrue(eatMatrix.HasMaxFoodLevel(new Point(i, j)));
                }
            }
        }
    }
}