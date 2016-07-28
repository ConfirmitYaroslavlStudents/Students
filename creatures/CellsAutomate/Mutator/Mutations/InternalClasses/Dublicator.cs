using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.Mutations.Logging;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{

    internal class Dublicator
    {
        private ICommand[] _commands;
        private Random _rnd;

        private readonly bool _logging = false;
        public ILogger Logger { get; }

        public Dublicator(IEnumerable<ICommand> commands, Random random)
        {
            _commands = commands.ToArray();
            _rnd = random;
        }

        public Dublicator(IEnumerable<ICommand> commands, Random random, ILogger logger) : this(commands, random)
        {
            Logger = logger;
            _logging = true;
        }

        public ICommand[] Dublicate(int index)
        {
            var indexToInsert = _rnd.Next(_commands.Length + 1);
            Insert(_commands[index], indexToInsert);
            if (_logging) Logger.Write(LogHelper.CreateDublicateMutationLog(_commands[index], index, indexToInsert));
            return _commands;
        }

        private void Insert(ICommand commandToInsert, int index)
        {          
            var newCommands = new ICommand[_commands.Length + 1];
            Array.Copy(_commands, 0, newCommands, 0, index);
            newCommands[index] = commandToInsert;

            if(index!=_commands.Length+1)
            Array.Copy(_commands, index, newCommands, index + 1, _commands.Length - index);

            _commands = newCommands;
        }
    }
}
