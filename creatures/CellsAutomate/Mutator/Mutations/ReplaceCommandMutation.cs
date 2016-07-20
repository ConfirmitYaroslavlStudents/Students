using System;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class ReplaceCommandMutation:IMutation
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
    }
}