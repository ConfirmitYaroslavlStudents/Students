using System;
using System.Collections.Generic;

namespace CellsAutomate.Mutator.NewMutations
{
    public class SwapCommandMutationNew : IMutationNew
    {
        private ICommandsList _commands;
        private Random _random;

        private readonly Stack<SwapCommand> _swapped;
        public SwapCommandMutationNew(Random random, ICommandsList commands)
        {
            _commands = commands;
            _random = random;
            _swapped = new Stack<SwapCommand>();
        }

        public void Transform()
        {
            InitNextTransform();
            var firstIndex = _random.Next(_commands.Count);
            var secondIndex = _random.Next(_commands.Count);
            var command = new SwapCommand(_commands, firstIndex, secondIndex);
            command.Release();
            _swapped.Push(command);
        }

        private void InitNextTransform()
        {
            _swapped.Clear();
        }

        public void Undo()
        {
            while (_swapped.Count > 0)
            {
                var swapped = _swapped.Pop();
                swapped.Undo();
            }
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

            public void Release()
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