using System;
using System.Collections.Generic;
using CellsAutomate.Mutator.CommandsList;

namespace CellsAutomate.Mutator.Mutations
{
    public class SwapCommandMutation : IMutation
    {
        private ICommandsList _commands;
        private Random _random;

        private SwapCommand _swapped;
        public SwapCommandMutation(Random random, ICommandsList commands)
        {
            _commands = commands;
            _random = random;
        }

        public void Transform()
        {
            InitNextTransform();
            var firstIndex = _random.Next(_commands.Count);
            var secondIndex = _random.Next(_commands.Count);
            var command = new SwapCommand(_commands, firstIndex, secondIndex);
            command.Execute();
            _swapped=command;
        }

        private void InitNextTransform()
        {
            _swapped = null;
        }

        public void Undo()
        {
            _swapped?.Undo();
            _swapped = null;
        }

        private class SwapCommand
        {
            public ICommandsList Commands { get; }
            public int First { get; }
            public int Second { get; }

            public SwapCommand(ICommandsList commands, int first, int second)
            {
                Commands = commands;
                First = first;
                Second = second;
            }

            public void Execute()
            {
                Swap(First, Second);
            }

            public void Undo()
            {
                Swap(First, Second);
            }

            private void Swap(int first, int second)
            {
                var temp = Commands[second];
                Commands[second] = Commands[first];
                Commands[first] = temp;
            }
        }
    }
}