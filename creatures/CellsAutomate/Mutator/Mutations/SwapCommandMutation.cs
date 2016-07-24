using System;
using CellsAutomate.Mutator.Mutations.InternalClasses;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class SwapCommandMutation : IMutation
    {
        private Random _rnd;

        public SwapCommandMutation(Random random)
        {
            _rnd = random;
        }
        public ICommand[] Transform(ICommand[] commands)
        {
            var firstSwapIndex = _rnd.Next(commands.Length);
            var secondSwapIndex = _rnd.Next(commands.Length - 1);
            if (firstSwapIndex > secondSwapIndex)
            {
                var tempSwapIndex = firstSwapIndex;
                firstSwapIndex = secondSwapIndex;
                secondSwapIndex = tempSwapIndex;
            }
            return new Swapper(commands).SwapCommand(firstSwapIndex, secondSwapIndex);
        }
    }
}