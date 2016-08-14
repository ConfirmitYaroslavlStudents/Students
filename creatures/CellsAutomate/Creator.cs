using CellsAutomate.Algorithms;
using CellsAutomate.Creatures;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate
{
    public abstract class Creator
    {
        public abstract BaseCreature CreateAbstractCreature();
    }

    public class CreatorOfCreature : Creator
    {
        private readonly ICommand[] _commandsForGetDirection;
        private readonly ICommand[] _commandsForGetAction;

        public CreatorOfCreature(ICommand[] commandsForGetAction, ICommand[] commandsForGetDirection)
        {
            _commandsForGetAction = commandsForGetAction;
            _commandsForGetDirection = commandsForGetDirection;
        }

        public override BaseCreature CreateAbstractCreature()
        {
            var executor = new Executor();
            return new Creature(executor, _commandsForGetDirection, _commandsForGetAction);
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
