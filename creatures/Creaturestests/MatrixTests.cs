using CellsAutomate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
namespace CellsAutomate.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void AliveCountIsNotEmptyTest()
        {
            var matrix = new Matrix(5, 5);
            //for (int i = 0; i < 5; i++)
                //matrix.Cells[i, i] = new SimpleCreature(new Point(i, i), 1);
            Assert.AreEqual(5, matrix.AliveCount);
        }

        [TestMethod()]
        public void AliveCountIsEmptyTest()
        {
            var matrix = new Matrix(5, 5);
            Assert.AreEqual(0, matrix.AliveCount);
        }

        [TestMethod()]
        public void FillMatrixWithFoodTest()
        {
            var matrix = new Matrix(5, 5);
            //matrix.Cells[0, 0] = new SimpleCreature(new Point(0, 0), 1);
            //matrix.Cells[0, 4] = new SimpleCreature(new Point(0, 4), 1);
            //matrix.Cells[4, 0] = new SimpleCreature(new Point(4, 0), 1);
            matrix.FillMatrixWithFood();

            var expectedFoodMatrix = new FoodMatrix(5, 5);

            //for (int i = 0; i < 5; i++)
                //for (int j = 0; j < 5; j++)
                    //expectedFoodMatrix.AddFood(new Point(i, j), SimpleCreature.FoodLevel);
            //expectedFoodMatrix.TakeFood(new Point(0, 0), SimpleCreature.FoodLevel);
            //expectedFoodMatrix.TakeFood(new Point(0, 4), SimpleCreature.FoodLevel);
            //expectedFoodMatrix.TakeFood(new Point(4, 0), SimpleCreature.FoodLevel);

            //CollectionAssert.AreEqual(expectedFoodMatrix, matrix.EatMatrix);
        }

        [TestMethod()]
        public void MakeTurnGoTest()
        {
            var matrix = new Matrix(2, 2);
            //var actualSimpleCreature = new SimpleCreature(new Point(0, 0), 1);
            //matrix.Cells[0, 0] = actualSimpleCreature;
            //matrix.EatMatrix.AddFood(new Point(0, 1), SimpleCreature.FoodLevel);
            matrix.MakeTurn();
            //Assert.AreEqual(new Point(0, 1), actualSimpleCreature.Position);
        }

        [TestMethod()]
        public void IsFreeIsTrueTest()
        {
            var matrix = new Matrix(2, 2);
            //matrix.Cells[0, 0] = new SimpleCreature(new Point(0, 0), 1);
            //Assert.IsTrue(matrix.IsFree(new Point(1, 1)));
        }

        [TestMethod()]
        public void IsFreeIsFalseTest()
        {
            var matrix = new Matrix(2, 2);
            //matrix.Cells[0, 0] = new SimpleCreature(new Point(0, 0), 1);
            //Assert.IsFalse(matrix.IsFree(new Point(0, 0)));
        }

        [TestMethod()]
        public void MakeTurnMakeChildTest()
        {
            var matrix = new Matrix(2, 2);
            //matrix.Cells[0, 0] = new SimpleCreature(new Point(0, 0), 1);
            //matrix.EatMatrix.AddFood(new Point(0, 0), SimpleCreature.FoodLevel);
            matrix.MakeTurn();
            matrix.MakeTurn();
            //Assert.IsFalse(matrix.IsFree(new Point(1, 0)));
        }

        [TestMethod()]
        public void MakeTurnEatTest()
        {
            var matrix = new Matrix(2, 2);
            //matrix.Cells[0, 0] = new SimpleCreature(new Point(0, 0), 1);
            //matrix.EatMatrix.AddFood(new Point(0, 0), SimpleCreature.FoodLevel);
            matrix.MakeTurn();
            //Assert.IsFalse(matrix.IsFree(new Point(0, 0)));
        }

        [TestMethod()]
        public void MakeTurnDieTest()
        {
            var matrix = new Matrix(1, 1);
            //matrix.Cells[0, 0] = new SimpleCreature(new Point(0, 0), 1);
            matrix.MakeTurn();
            matrix.MakeTurn();
            matrix.MakeTurn();
            //Assert.IsTrue(matrix.IsFree(new Point(0, 0)));
        }
    }
}