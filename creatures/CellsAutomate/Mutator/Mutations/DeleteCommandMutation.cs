using System;
using CellsAutomate.Mutator.Mutations.InternalClasses;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class DeleteCommandMutation : IMutation
    {
        private Random _rnd;

        public DeleteCommandMutation()
        {
            _rnd = new Random();
        }

        public DeleteCommandMutation(Random random)
        {
            _rnd = random;
        }
        public ICommand[] Transform(ICommand[] commands)
        {
            var delIndex = _rnd.Next(commands.Length);
            return new DeletterViaVisitor(commands).DeleteCommand(delIndex);
        }
    }
}