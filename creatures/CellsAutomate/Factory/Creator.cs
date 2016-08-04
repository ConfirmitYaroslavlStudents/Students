using System;
using System.Drawing;
using CellsAutomate.Creatures;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate.Factory
{
    public abstract class Creator
    {
        public abstract BaseCreature CreateAbstractCreature();
    }

    public class CreatorOfCreature : Creator
    {
        private ICommand[] _commands;
        public CreatorOfCreature()
        {
            _commands = new SeedGenerator().StartAlgorithm;
        }

        public override BaseCreature CreateAbstractCreature()
        {
            var executor = new Executor();
            return new Creature(executor, _commands);
        }
    }

    public class CreatorOfSimpleCreature : Creator
    {
        public override BaseCreature CreateAbstractCreature()
        {
            return new SimpleCreature();
        }
    }
}
