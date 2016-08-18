using System.Collections;
using System.Collections.Generic;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;

namespace CellsAutomate.Mutator.CommandsList
{
    public class CommandsListWithLogger : ICommandsList
    {
        private ICommandsList _commands;
        private ILogger _logger;
        private CommandToStringParser _toStringParser = new CommandToStringParser();
        public int Count => _commands.Count;

        public CommandsListWithLogger(ICommandsList commands, ILogger logger)
        {
            _commands = commands;
            _logger = logger;
        }

        public void Insert(int index, ICommand item)
        {
            var parsedCommand = _toStringParser.ParseCommand(item);
            _commands.Insert(index, item);
            _logger.Write($"Command \"{parsedCommand}\" inserted at \"{index}\"\n");
        }

        public void RemoveAt(int index)
        {
            var parsedCommand = _toStringParser.ParseCommand(_commands[index]);
            _commands.RemoveAt(index);
            _logger.Write($"Command \"{parsedCommand}\" deleted from \"{index}\"\n");
        }

        public ICommand this[int index]
        {
            get { return _commands[index]; }
            set
            {
                var parsedCommand = _toStringParser.ParseCommand(_commands[index]);
                _commands[index] = value;
                var parsedInput = _toStringParser.ParseCommand(_commands[index]);
                _logger.Write($"Command \"{parsedCommand}\" replaced \"{parsedInput}\" at {index}\n");
            }
        }

        public bool AssertValid()
        {
            return _commands.AssertValid();
        }

        public IEnumerator<ICommand> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}