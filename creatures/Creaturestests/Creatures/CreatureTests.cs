using System;
using System.Drawing;
using CellsAutomate.Creatures;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.Creatures
{
    [TestClass()]
    public class CreatureTests
    {
        [TestMethod()]
        public void MakeChildTest()
        {
            var parent = new Creature(new Point(0, 0), new Executor(), new ICommand[0],  new Random(), 1);
            var child = parent.MakeChild(new Point(0, 1));
            Assert.AreEqual(2, child.GetGeneration);
        }
    }
}