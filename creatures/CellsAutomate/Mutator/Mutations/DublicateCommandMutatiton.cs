using System;
using CellsAutomate.Mutator.Mutations.InternalClasses;
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
    }
}