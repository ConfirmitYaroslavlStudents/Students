using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CellsAutomate.Tests
{
    [TestClass()]
    public class FoodMatrixTests
    {
        [TestMethod()]
        public void HasFoodIsTrueTest()
        {
            var eatMatrix = new FoodMatrix(2, 2);
            //eatMatrix.AddFood(new Point(0, 0), 1);
            Assert.IsTrue(eatMatrix.HasFood(new Point(0, 0)));
        }

        [TestMethod()]
        public void HasFoodIsFalseTest()
        {
            var eatMatrix = new FoodMatrix(2, 2);
            //eatMatrix.AddFood(new Point(0, 0), 1);
            Assert.IsFalse(eatMatrix.HasFood(new Point(1, 1)));
        }

        [TestMethod()]
        public void AddFoodTest()
        {
            var eatMatrix = new FoodMatrix(2, 2);
            //eatMatrix.AddFood(new Point(0, 0), 1);
            Assert.IsTrue(eatMatrix.HasFood(new Point(0, 0)));
        }

        [TestMethod()]
        public void TakeFoodIsTrueTest()
        {
            var eatMatrix = new FoodMatrix(2, 2);
            //eatMatrix.AddFood(new Point(0, 0), 1);
            eatMatrix.TakeFood(new Point(0, 0));
            Assert.IsFalse(eatMatrix.HasFood(new Point(0, 0)));
        }

        [TestMethod()]
        public void TakeFoodIsFalseTest()
        {
            var eatMatrix = new FoodMatrix(2, 2);
            Assert.IsFalse(eatMatrix.TakeFood(new Point(0, 0)));
        }

        [TestMethod()]
        public void BuildTest()
        {
            var actual = new FoodMatrix(4, 4);
            var creatures = new bool[4, 4];
            for (int i = 0; i < 4; i++)
                creatures[i, i] = true;
            actual.Build(creatures, 0, 0);
            actual.Build(creatures, 0, 3);
            actual.Build(creatures, 3, 0);
            actual.Build(creatures, 3, 3);

            var expected = new FoodMatrix(4, 4);
            //for (int i = 0; i < 4; i++)
            //{
                //for (int j = 0; j < 4; j++)
                //{
                    //expected.AddFood(new Point(i, j), SimpleCreature.FoodLevel);
                //}
            //}

            //for (int i = 0; i < 4; i++)
                //expected.TakeFood(new Point(i, i), SimpleCreature.FoodLevel);

            //CollectionAssert.AreEqual(expected, actual);
        }
    }
}