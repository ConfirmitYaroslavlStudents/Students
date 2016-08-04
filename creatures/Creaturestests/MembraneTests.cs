using System;
using System.Drawing;
using CellsAutomate;
using CellsAutomate.Creatures;
using CellsAutomate.Food;
using Creatures.Language.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests
{
    [TestClass()]
    public class MembraneTests
    {
        //[TestMethod()]
        //public void TurnDieTest()
        //{
        //    var point = new Point(0, 0);
        //    var creature = new Creature(point, new Executor(), new SeedGenerator().StartAlgorithm, new Random(), 1);
        //    var membrane = new Membrane(creature);
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    var creatures = new Creature[2, 2];
        //    creatures[point.X, point.Y] = creature;
        //    while (creature.HasMinToSurvive)
        //        membrane.Turn(eatMatrix, creatures);
        //    Assert.AreEqual(Tuple.Create(ActionEnum.Die, DirectionEnum.Stay), membrane.Turn(eatMatrix, creatures));
        //}

        //[TestMethod()]
        //public void TurnNotDieTest()
        //{
        //    var point = new Point(0, 0);
        //    var creature = new Creature(point, new Executor(), new SeedGenerator().StartAlgorithm, new Random(), 1);
        //    var membrane = new Membrane(creature);
        //    var eatMatrix = new FoodMatrix(2, 2, new FillingFromCornersByWavesStrategy());
        //    var creatures = new Creature[2, 2];
        //    creatures[point.X, point.Y] = creature;
        //    Assert.AreNotEqual(Tuple.Create(ActionEnum.Die, DirectionEnum.Stay), membrane.Turn(eatMatrix, creatures));
        //}
    }
}