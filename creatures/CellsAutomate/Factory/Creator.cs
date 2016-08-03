using System;
using System.Drawing;
using CellsAutomate.Creatures;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate.Factory
{
    public abstract class Creator
    {
        public abstract BaseCreature CreateAbstractCreature(Point position, Random random, int generation);
    }

    public class CreatorOfCreature : Creator
    {
        private ICommand[] _commands;
        public CreatorOfCreature()
        {
            _commands = new SeedGenerator().StartAlgorithm;
        }

        public override BaseCreature CreateAbstractCreature(Point position, Random random, int generation)
        {
            var executor = new Executor();
            return new Creature(position, executor, _commands, random, generation);
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
