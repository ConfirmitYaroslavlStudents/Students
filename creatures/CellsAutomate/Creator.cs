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

        public CreatorOfCreature()
        {
            _commandsForGetDirection = new GetDirectionAlgorithm().Algorithm;
            _commandsForGetAction = new GetActionAlgorithm().Algorithm;

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
