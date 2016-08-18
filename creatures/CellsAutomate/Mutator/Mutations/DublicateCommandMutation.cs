using System;
using System.Collections.Generic;
using CellsAutomate.Mutator.CommandsList;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class DublicateCommandMutation : IMutation
    {
        private ICommandsList _commands;
        private Random _random;

        private DublicateCommand _dublicated;

        public DublicateCommandMutation(Random random, ICommandsList commands)
        {
            _commands = commands;
            _random = random;
        }

        public void Transform()
        {
            InitNextTransform();
            var dublicateFromIndex = _random.Next(_commands.Count);
            var dublicateToIndex = _random.Next(dublicateFromIndex, _commands.Count + 1);
            var dublicateCommand = new DublicateCommand(_commands, dublicateFromIndex, dublicateToIndex);
            dublicateCommand.Execute();
            _dublicated = dublicateCommand;
        }

        public void Undo()
        {
            _dublicated?.Undo();
            _dublicated = null;
        }

        private void InitNextTransform()
        {
            _dublicated = null;
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

            public void Execute()
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