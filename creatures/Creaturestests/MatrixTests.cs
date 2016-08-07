using System;
using System.Drawing;
using CellsAutomate;
using CellsAutomate.Creatures;
using CellsAutomate.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests
{
    [TestClass()]
    public class MatrixTests
    {
        //[TestMethod()]
        //public void AliveCountIsNotEmptyTest()
        //{
        //    var random = new Random();
        //    var matrix = new Matrix(2, 2, new CreatorOfSimpleCreature()) {Creatures = {
        //            [0, 0] = new SimpleCreature(new Point(0, 0), random, 1),
        //            [1, 0] = new SimpleCreature(new Point(1, 0), random, 1)}};
        //    Assert.AreEqual(2, matrix.AliveCount);
        //}

        [TestMethod()]
        public void AliveCountIsEmptyTest()
        {
            var matrix = new Matrix(2, 2, new CreatorOfSimpleCreature());
            Assert.AreEqual(0, matrix.AliveCount);
        }
    }
}