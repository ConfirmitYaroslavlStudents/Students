using System;
using System.Collections.Generic;
using System.Linq;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    internal class Replacer : ICommandVisitor
    {
        private ICommand[] _commands;
        private Random _rnd;
        private int _index;

        public Replacer(IEnumerable<ICommand> commands,Random random)
        {
            _commands = commands.ToArray();
            _rnd = new Random();
        }

        public ICommand[] Replace(int index)
        {
            Execute(_commands[index]);
            _index = index;
            return _commands;
        }

        private void Execute(ICommand command)
        {
            command.AcceptVisitor(this);
        }

        public void Accept(NewInt command)
        {
            return;//TODO
        }

        public void Accept(SetValue command)
        {
            int newValue;
            do
            {
                newValue = _rnd.Next(Int32.MinValue, Int32.MaxValue);
            } while (command.Value==newValue);
            var newCommand = new SetValue(command.TargetName, newValue);
            _commands[_index] = newCommand;
        }

        public void Accept(Plus command)
        {
            return;//TODO
        }

        public void Accept(Print command)
        {
            return;//TODO
        }

        public void Accept(Minus command)
        {
            return;//TODO
        }

        public void Accept(CloneValue command)
        {
            return;//TODO
        }

        public void Accept(Condition command)
        {
            return;//TODO
        }

        public void Accept(Stop command)
        {
            return;
        }

        public void Accept(CloseCondition command)
        {
            return;
        }

        public void Accept(GetState command)
        {
            return;//TODO
        }

        public void Accept(GetRandom command)
        {
            return;//TODO
        }
    }
}