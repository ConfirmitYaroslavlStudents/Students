using System;
using System.Collections.Generic;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations.InternalClasses;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class AddCommandMutation : IMutation
    {
        private readonly List<Action<int>> _handlers;

        private Random _random;
        private ICommandsList _commands;
        private CommandsCreator _creator;

        private readonly Stack<AddCommand> _added;
        public AddCommandMutation(Random random, ICommandsList commands)
        {
            _random = random;
            _commands = commands;
            _creator = new CommandsCreator(random, commands);
            _added = new Stack<AddCommand>();
            _handlers = new List<Action<int>>
            {
                AddNewInt,
                AddCloneValue,
                AddConditionBlock,
                AddGetRandom,
                AddGetState,
                AddMinus,
                AddPlus,
                AddPrint,
                AddSetValue,
                AddStop
            };
        }

        public void Transform()
        {
            InitNextTransform();
            var handler = ChooseRandom(_handlers);
            var insertIndex = _random.Next(_commands.Count);
            handler(insertIndex);
        }

        private void InitNextTransform()
        {
            _added.Clear();
        }

        public void Undo()
        {
            while (_added.Count > 0)
            {
                var addCommand = _added.Pop();
                addCommand.Undo();
            }
        }

        private void Insert(ICommand commandToInsert, int index)
        {
            var addCommand = new AddCommand(_commands, commandToInsert, index);
            addCommand.Execute();
            _added.Push(addCommand);
        }

        private void AddNewInt(int insertIndex)
        {
            var command = _creator.CreateNewInt(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddSetValue(int insertIndex)
        {
            var command = _creator.CreateSetValue(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddCloneValue(int insertIndex)
        {
            var command = _creator.CreateCloneValue(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddConditionBlock(int insertIndex)
        {
            AddConditionBlock(insertIndex, _random.Next(insertIndex + 1, _commands.Count + 1));
        }

        private void AddConditionBlock(int ifIndex, int endifIndex)
        {
            AddCondition(ifIndex);
            AddCloseCondition(endifIndex);
        }

        private void AddCondition(int insertIndex)
        {
            var command = _creator.CreateCondition(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddCloseCondition(int insertIndex)
        {
            var command = _creator.CreateCloseCondition(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddGetRandom(int insertIndex)
        {
            var command = _creator.CreateGetRandom(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddGetState(int insertIndex)
        {
            var command = _creator.CreateGetState(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddMinus(int insertIndex)
        {
            var command = _creator.CreateMinus(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddPlus(int insertIndex)
        {
            var command = _creator.CreatePlus(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddPrint(int insertIndex)
        {
            var command = _creator.CreatePrint(insertIndex);
            Insert(command, insertIndex);
        }

        private void AddStop(int insertIndex)
        {
            var command = _creator.CreateStop(insertIndex);
            Insert(command, insertIndex);
        }

        private T ChooseRandom<T>(IReadOnlyList<T> array)
        {
            return array[_random.Next(array.Count)];
        }

        private class AddCommand
        {
            public ICommandsList Commands { get; }
            public ICommand Command { get; }
            public int Index { get; }

            public AddCommand(ICommandsList commands, ICommand command, int index)
            {
                Commands = commands;
                Command = command;
                Index = index;
            }

            public void Execute()
            {
                Commands.Insert(Index, Command);
            }

            public void Undo()
            {
                Commands.RemoveAt(Index);
            }

        }

    }
}
