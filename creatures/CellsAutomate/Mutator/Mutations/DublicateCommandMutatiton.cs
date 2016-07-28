using System;
using CellsAutomate.Mutator.Mutations.InternalClasses;
using CellsAutomate.Mutator.Mutations.Logging;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class DublicateCommandMutatiton : IMutation
    {
        private Random _rnd;

        public DublicateCommandMutatiton(Random rnd)
        {
            _rnd = rnd;
        }

        public ICommand[] Transform(ICommand[] commands)
        {
            return new Dublicator(commands, _rnd).Dublicate(_rnd.Next(commands.Length));
        }

        public ICommand[] Transform(ICommand[] commands, ILogger logger)
        {
            return new Dublicator(commands, _rnd,logger).Dublicate(_rnd.Next(commands.Length));
        }
    }
}