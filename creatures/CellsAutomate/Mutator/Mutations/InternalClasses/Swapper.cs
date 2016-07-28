using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.Mutations.Logging;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{
    internal class Swapper
    {
        private ICommand[] _commands;
        private readonly bool _logging = false;
        public ILogger Logger { get; }

        public Swapper(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToArray();
        }

        public Swapper(IEnumerable<ICommand> commands, ILogger logger) : this(commands)
        {
            _logging = true;
            Logger = logger;
        }

        public ICommand[] SwapCommand(int firstSwapIndex, int secondSwapIndex)
        {
            if (_logging)
                Logger.Write(LogHelper.CreateSwapMutationLog(_commands[firstSwapIndex], firstSwapIndex,
                    _commands[secondSwapIndex], secondSwapIndex));

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