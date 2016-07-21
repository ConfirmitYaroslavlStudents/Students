using System;
using System.Collections.Generic;
using System.Linq;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{

    internal class Dublicator
    {
        private ICommand[] _commands;
        private Random _rnd;

        public Dublicator(IEnumerable<ICommand> commands, Random random)
        {
            _commands = commands.ToArray();
            _rnd = random;
        }

        public ICommand[] Dublicate(int index)
        {
            Insert(_commands[index], _rnd.Next(_commands.Length+1));
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
