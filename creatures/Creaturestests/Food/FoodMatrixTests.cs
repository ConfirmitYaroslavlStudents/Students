using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using CellsAutomate.Constants;
using Creaturestests.Food;

namespace CellsAutomate.Food.Tests
{
    [TestClass()]
    public class FoodMatrixTests
    {

        [TestMethod()]
        public void HasOneBiteIsTrueTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var point = new Point(0, 0);

            FrequentlyUsedMethods.RaiseFoodLevelToConstant(eatMatrix, point, CreatureConstants.OneBite);

            Assert.IsTrue(eatMatrix.HasOneBite(point));
        }

        [TestMethod()]
        public void HasOneBiteIsFalseTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());

            for (int i = 0; i < eatMatrix.Length; i++)
            {
                for (int j = 0; j < eatMatrix.Height; j++)
                {
                    Assert.IsFalse(eatMatrix.HasOneBite(new Point(i, j)));
                }
            }
        }

        [TestMethod()]
        public void HasMaxFoodLevelIsTrueTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var point = new Point(0, 0);

            FrequentlyUsedMethods.RaiseFoodLevelToConstant(eatMatrix, point, FoodMatrixConstants.MaxFoodLevel);

            Assert.IsTrue(eatMatrix.HasMaxFoodLevel(point));
        }

        [TestMethod()]
        public void HasMaxFoodLevelIsFalseTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());

            for (int i = 0; i < eatMatrix.Length; i++)
            {
                for (int j = 0; j < eatMatrix.Height; j++)
                {
                    if (FoodMatrixConstants.AddedFoodLevel < FoodMatrixConstants.MaxFoodLevel)
                        eatMatrix.AddFood(new Point(i, j));
                    Assert.IsFalse(eatMatrix.HasMaxFoodLevel(new Point(i, j)));
                }
            }
        }

        [TestMethod()]
        public void AddFoodTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var point = new Point(0, 0);

            FrequentlyUsedMethods.RaiseFoodLevelToConstant(eatMatrix, point, CreatureConstants.OneBite);

            Assert.IsTrue(eatMatrix.HasOneBite(point));
        }

        [TestMethod()]
        public void TakeFoodIsTrueTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var point = new Point(0, 0);

            FrequentlyUsedMethods.RaiseFoodLevelToConstant(eatMatrix, point, CreatureConstants.OneBite);

            Assert.IsTrue(eatMatrix.HasOneBite(point));

            var counter = FoodMatrixConstants.AddedFoodLevel / CreatureConstants.OneBite;
            for (int i = 0; i < counter; i++)
            {
                Assert.IsTrue(eatMatrix.TakeFood(point));
            }

            Assert.IsFalse(eatMatrix.HasOneBite(point));
        }

        [TestMethod()]
        public void TakeFoodIsFalseTest()
        {
            var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
            var point = new Point(0, 0);
            Assert.IsFalse(eatMatrix.TakeFood(point));
        }
    }
}