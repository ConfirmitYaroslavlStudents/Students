using System.Drawing;
using CellsAutomate.Constants;
using CellsAutomate.Food;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests
{
    [TestClass()]
    public class FoodMatrixTests
    {
        //[TestMethod()]
        //public void HasFoodIsTrueTest()
        //{
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    eatMatrix.AddFood(new Point(0, 0));
        //    Assert.IsTrue(eatMatrix.HasFood(new Point(0, 0)));
        //}

        //[TestMethod()]
        //public void HasFoodIsFalseTest()
        //{
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    Assert.IsFalse(eatMatrix.HasFood(new Point(0, 0)));
        //}

        //[TestMethod()]
        //public void AddFoodTest()
        //{
        //    var point = new Point(0, 0);
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    Assert.IsFalse(eatMatrix.HasFood(point));
        //    eatMatrix.AddFood(point);
        //    Assert.IsTrue(eatMatrix.HasFood(point));
        //    Assert.AreEqual(FoodMatrixConstants.FoodLevel, eatMatrix.GetLevelOfFood(point));
        //}

        //[TestMethod()]
        //public void TakeFoodIsTrueTest()
        //{
        //    var point = new Point(0, 0);
        //    var levelOfFood = 0;
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    while (eatMatrix.GetLevelOfFood(point) <= CreatureConstants.OneBite)
        //    {
        //        eatMatrix.AddFood(point);
        //        levelOfFood += FoodMatrixConstants.FoodLevel;
        //    }
        //    Assert.IsTrue(eatMatrix.TakeFood(point));
        //    levelOfFood -= CreatureConstants.OneBite;
        //    Assert.AreEqual(levelOfFood, eatMatrix.GetLevelOfFood(point));
        //}

        //[TestMethod()]
        //public void TakeFoodIsFalseTest()
        //{
        //    var point = new Point(0, 0);
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    Assert.IsFalse(eatMatrix.TakeFood(point));
        //}

        //[TestMethod()]
        //public void GetLevelOfFoodTest()
        //{
        //    var point = new Point(0, 0);
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    eatMatrix.AddFood(point);
        //    Assert.AreEqual(FoodMatrixConstants.FoodLevel, eatMatrix.GetLevelOfFood(point));
        //}
    }
}