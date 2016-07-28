using System;
using CellsAutomate.Mutator.Mutations.InternalClasses;
using CellsAutomate.Mutator.Mutations.Logging;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class ReplaceCommandMutation : IMutation
    {
        private Random _rnd;

        public ReplaceCommandMutation(Random rnd)
        {
            _rnd = rnd;
        }

        public ICommand[] Transform(ICommand[] commands)
        {
            return new Replacer(commands, _rnd).Replace(_rnd.Next(commands.Length));
        }

        public ICommand[] Transform(ICommand[] commands, ILogger logger)
        {
            return new Replacer(commands, _rnd, logger).Replace(_rnd.Next(commands.Length));
        }
    }
}