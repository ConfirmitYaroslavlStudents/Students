using System.Drawing;
using CellsAutomate.Food;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.Food
{
    [TestClass()]
    public class FillingFromCornersByWavesStrategyTests
    {
        //[TestMethod()]
        //public void BuildIsFullTest()
        //{
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    var creatures = new bool[2, 2];
        //    eatMatrix.Build(creatures);
        //    for(int i = 0; i < eatMatrix.Length; i++)
        //        for(int j = 0; j < eatMatrix.Height; j++)
        //            Assert.IsTrue(eatMatrix.HasFood(new Point(i, j)));
        //}

        //[TestMethod()]
        //public void BuildIsEmptyTest()
        //{
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    var creatures = new bool[2, 2];

        //    for (int i = 0; i < eatMatrix.Length; i++)
        //        for (int j = 0; j < eatMatrix.Height; j++)
        //            creatures[i, j] = true;

        //    eatMatrix.Build(creatures);
        //    for (int i = 0; i < eatMatrix.Length; i++)
        //        for (int j = 0; j < eatMatrix.Height; j++)
        //            Assert.IsFalse(eatMatrix.HasFood(new Point(i, j)));
        //}
    }
}