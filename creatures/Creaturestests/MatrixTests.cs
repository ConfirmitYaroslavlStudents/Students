using System;
using System.Drawing;
using CellsAutomate;
using CellsAutomate.Food;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void AliveCountIsNotEmptyTest()
        {
            var random = new Random();
            var creator = new CreatorOfCreature();
            var matrix = new Matrix(2, 2, creator, new FillingFromCornersByWavesStrategy())
            {
                Creatures = {
                    [0, 0] = new Membrane(creator.CreateAbstractCreature(), random, new Point(0, 0), 1, creator),
                    [1, 0] = new Membrane(creator.CreateAbstractCreature(), random, new Point(1, 0), 1, creator)}
            };
            Assert.AreEqual(2, matrix.AliveCount);
        }

        [TestMethod()]
        public void AliveCountIsEmptyTest()
        {
            var matrix = new Matrix(2, 2, new CreatorOfCreature(), new FillingFromCornersByWavesStrategy());
            Assert.AreEqual(0, matrix.AliveCount);
        }
    }
}