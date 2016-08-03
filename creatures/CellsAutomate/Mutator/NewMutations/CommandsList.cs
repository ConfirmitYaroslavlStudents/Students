using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate.Mutator.NewMutations
{
    public class CommandsList : ICommandsList
    {
        private List<ICommand> _commands;
        public int Count => _commands.Count;

        public CommandsList(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToList();
        }

        public bool AssertValid()
        {
            var executor = new ValidationExecutor();
            return executor.Execute(_commands, new ExecutorToolset(new Random()));
        }

        public void Insert(int index, ICommand item)
        {
            _commands.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _commands.RemoveAt(index);
        }

        public ICommand this[int index]
        {
            get { return _commands[index]; }
            set { _commands[index] = value; }
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