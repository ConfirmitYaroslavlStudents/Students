using System;
using System.Collections.Generic;
using System.Linq;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{
    internal class Swapper
    {
        private ICommand[] _commands;
        public Swapper(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToArray();
        }

        public ICommand[] SwapCommand(int firstSwapIndex, int secondSwapIndex)
        {
            Swap(firstSwapIndex, secondSwapIndex);
            return _commands;
        }

        private void Swap(int firstSwapIndex, int secondSwapIndex)
        {
            var newCommands = new ICommand[_commands.Length];
            Array.Copy(_commands, 0, newCommands, 0, firstSwapIndex);
            newCommands[firstSwapIndex] = _commands[secondSwapIndex];
            Array.Copy(_commands, firstSwapIndex + 1, newCommands, firstSwapIndex + 1, secondSwapIndex - firstSwapIndex - 1);
            newCommands[secondSwapIndex] = _commands[firstSwapIndex];
            Array.Copy(_commands, secondSwapIndex + 1, newCommands, secondSwapIndex + 1, _commands.Length - secondSwapIndex - 1);
            _commands = newCommands;
        }
    }
}
