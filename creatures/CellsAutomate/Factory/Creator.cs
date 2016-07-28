using System;
using System.Drawing;
using Creatures.Language.Executors;

namespace CellsAutomate.Factory
{
    public abstract class Creator
    {
        public abstract BaseCreature CreateAbstractCreature(Point position, Random random, int generation);
    }

    public class CreatorOfCreature : Creator
    {
        public override BaseCreature CreateAbstractCreature(Point position, Random random, int generation)
        {
            var executor = new Executor();
            var commands = new SeedGenerator().StartAlgorithm;
            return new Creature(position, executor, commands, random, generation);
        }
    }

    public class CreatorOfSimpleCreature : Creator
    {
        public override BaseCreature CreateAbstractCreature(Point position, Random random, int generation)
        {
            return new SimpleCreature(position, random, generation);
        }
    }
}
