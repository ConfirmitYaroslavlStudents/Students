using System;
using System.Collections.Generic;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.NewMutations
{
    public class DublicateCommandMutationNew : IMutationNew
    {
        private ICommandsList _commands;
        private Random _random;

        private readonly Stack<DublicateCommand> _dublicated;

        public DublicateCommandMutationNew(Random random, ICommandsList commands)
        {
            _commands = commands;
            _random = random;
            _dublicated = new Stack<DublicateCommand>();
        }

        public void Transform()
        {
            InitNextTransform();
            var dublicateFromIndex = _random.Next(_commands.Count);
            var dublicateToIndex = _random.Next(dublicateFromIndex, _commands.Count + 2);
            var dublicateCommand = new DublicateCommand(_commands, dublicateFromIndex, dublicateToIndex);
            dublicateCommand.Release();
            _dublicated.Push(dublicateCommand);
        }

        public void Undo()
        {
            while (_dublicated.Count > 0)
            {
                var dublicateCommand = _dublicated.Pop();
                dublicateCommand.Undo();
            }
        }

        private void InitNextTransform()
        {
            _dublicated.Clear();
        }

        private class DublicateCommand
        {
            public ICommandsList Commands { get; }
            public ICommand Command { get; }
            public int From { get; }
            public int To { get; }

            public DublicateCommand(ICommandsList commands, int from, int to)
            {
                Commands = commands;
                Command = commands[from];
                From = from;
                To = to;
            }

            public void Release()
            {
                Commands.Insert(To, Command);
            }

            public void Undo()
            {
                Commands.RemoveAt(To);
            }
        }
    }
}