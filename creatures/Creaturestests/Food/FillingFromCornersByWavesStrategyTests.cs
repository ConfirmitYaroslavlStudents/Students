using System.Drawing;
using CellsAutomate.Food;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CellsAutomate.Constants;

namespace Creaturestests.Food
{
    [TestClass()]
    public class FillingFromCornersByWavesStrategyTests
    {
        [TestMethod()]
        public void BuildIsFullTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var creatures = new bool[2, 2];

            eatMatrix.Build(creatures);
            for (int i = 0; i < eatMatrix.Length; i++)
                for (int j = 0; j < eatMatrix.Height; j++)
                    Assert.IsTrue(eatMatrix.HasMaxFoodLevel(new Point(i, j)), "Your MaxFoodLevel constant exceeds AddedFoodLevel multiplied by 4");
        }

        [TestMethod()]
        public void BuildIsEmptyTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var creatures = new bool[2, 2];

            for (int i = 0; i < eatMatrix.Length; i++)
                for (int j = 0; j < eatMatrix.Height; j++)
                    creatures[i, j] = true;

            eatMatrix.Build(creatures);
            for (int i = 0; i < eatMatrix.Length; i++)
                for (int j = 0; j < eatMatrix.Height; j++)
                    Assert.IsFalse(eatMatrix.HasOneBite(new Point(i, j)));
        }

        [TestMethod()]
        public void BuildWithOneFreeCellTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var creatures = new bool[2, 2];
            creatures[0, 0] = true;
            creatures[0, 1] = true;
            creatures[1, 0] = true;

            FrequentlyUsedMethods.RaiseFoodLevelToConstantWithBuild(eatMatrix, creatures, FoodMatrixConstants.MaxFoodLevel, 1);
            Assert.IsTrue(eatMatrix.HasMaxFoodLevel(new Point(1, 1)));
        }
    }
}