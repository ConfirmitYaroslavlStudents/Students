using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace CellsAutomate.Tests
{
    [TestClass()]
    public class MembraneTests
    {

        [TestMethod()]
        public void MakeChildTest()
        {
            var random = new Random();
            var creator = new CreatorOfCreature();
            var pointOfBirth = new Point(1, 0);
            var parentMembrane = new Membrane(creator.CreateAbstractCreature(), random, new Point(0, 0), 1, creator);
            var childMembrane = parentMembrane.MakeChild(pointOfBirth);

            Assert.AreEqual(parentMembrane.GetType(), childMembrane.GetType());
            Assert.AreEqual(pointOfBirth, childMembrane.Position);
            Assert.AreEqual(2, childMembrane.Generation);
        }

        [TestMethod()]
        public void MoveTest()
        {
            var random = new Random();
            var creator = new CreatorOfCreature();
            var creatures = new Membrane[2, 2];
            var creature = new Membrane(creator.CreateAbstractCreature(), random, new Point(0, 0), 1, creator);
            var destinationPoint = new Point(1, 0);
            creatures[0, 0] = creature;
            creature.Move(creatures, destinationPoint);

            Assert.AreEqual(destinationPoint, creature.Position);
            Assert.AreEqual(null, creatures[0, 0]);
            Assert.AreEqual(creature, creatures[destinationPoint.X, destinationPoint.Y]);
        }
    }
}