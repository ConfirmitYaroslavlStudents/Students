using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.CommandsList;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class DeleteCommandMutation : IMutation
    {
        private Random _random;
        private ICommandsList _commands;
        private bool[] _marks;

        private readonly Stack<RemoveCommand> _removed;
        private readonly List<RemoveCommand> _commandsToRemove;

        public DeleteCommandMutation(Random random, ICommandsList commands)
        {
            _random = random;
            _commands = commands;
            _marks = new bool[commands.Count];
            _removed = new Stack<RemoveCommand>();
            _commandsToRemove = new List<RemoveCommand>();
        }

        public void Transform()
        {
            InitNewTransform();
            var deleteIndex = _random.Next(_commands.Count);
            Delete(deleteIndex);
            ReleaseRemoveCommands();
        }

        private void InitNewTransform()
        {
            _marks = new bool[_commands.Count];
            _removed.Clear();
        }

        private void ReleaseRemoveCommands()
        {
            if (_commandsToRemove.Count == 0) return;
            foreach (var item in _commandsToRemove.OrderByDescending(n => n.Index))
            {
                item.Execute();
                _removed.Push(item);
            }
            _commandsToRemove.Clear();
        }

        public void Undo()
        {
            while (_removed.Count > 0)
            {
                _removed.Pop().Undo();
            }
        }

        private void Delete(int index)
        {
            if (_marks[index]) return;
            _marks[index] = true;
            _commandsToRemove.Add(new RemoveCommand(_commands, index));
            AssertValid(index);
        }

        private void AssertValid(int index)
        {
            var comToDel = _commands[index];
            if (comToDel is ICommandDeclaration)
            {
                AssertDeclarationCommand(index);
            }
            if (comToDel is ICommandWithArgument)
            {
                AssertCommandWithArgument(index);
            }
            if (comToDel is ICommandSetter)
            {
                AssertCommandSetter(index);
            }
            if (comToDel is ICommandWithConstruction)
            {
                AssertCommandWithConstruction(index);
            }
        }

        private void AssertDeclarationCommand(int index)
        {
            var comToDel = _commands[index] as ICommandDeclaration;
            for (int i = index + 1; i < _commands.Count; i++)
            {
                if (_marks[i]) continue;

                if (_commands[i] is ICommandWithArgument)
                {
                    if ((_commands[i] as ICommandWithArgument).ContainsAsArgument(comToDel.Name))
                        Delete(i);

                    if (_commands[i] is ICommandSetter)
                    {
                        if ((_commands[i] as ICommandSetter).TargetName == comToDel.Name)
                        {
                            Delete(i);
                        }
                    }
                }

            }
        }

        private void AssertCommandSetter(int index)
        {
            var comToDel = _commands[index] as ICommandSetter;

            if (IsInitializeSetter(comToDel, index))
            {
                DeleteAllUnderWhereUsed(comToDel.TargetName, index);
            }
        }

        private void AssertCommandWithArgument(int index)
        {
            var comToDel = _commands[index] as ICommandWithArgument;

        }

        private void AssertCommandWithConstruction(int index)
        {
            if (_commands[index] is CloseCondition)
            {
                var countOfAnotherCloseConditions = 0;
                for (int i = index - 1; i >= 0; i--)
                {
                    if (_commands[i] is CloseCondition)
                    {
                        countOfAnotherCloseConditions++;
                        continue;
                    }
                    if (_commands[i] is Condition)
                    {
                        if (countOfAnotherCloseConditions > 0)
                        {
                            countOfAnotherCloseConditions--;
                            continue;
                        }
                        if (_marks[i]) return;
                        Delete(i);
                        break;
                    }
                }
            }
            if (_commands[index] is Condition)
            {
                var countOfAnotherConditions = 0;
                for (int i = index + 1; i < _commands.Count; i++)
                {
                    if (_commands[i] is Condition)
                    {
                        countOfAnotherConditions++;
                        continue;
                    }
                    if (_commands[i] is CloseCondition)
                    {
                        if (countOfAnotherConditions > 0)
                        {
                            countOfAnotherConditions--;
                            continue;
                        }
                        if (_marks[i]) return;
                        Delete(i);
                        break;
                    }
                }
            }
        }

        private bool IsInitializeSetter(ICommandSetter comToDel, int index)
        {
            for (int i = index - 1; i >= 0; i--)
            {
                if (_marks[i]) continue;

                if (_commands[i] is ICommandSetter)
                {
                    if ((_commands[i] as ICommandSetter).TargetName == comToDel.TargetName)
                        return false;
                }
                if (_commands[i] is ICommandDeclaration)
                {
                    if ((_commands[i] as ICommandDeclaration).Name == comToDel.TargetName)
                        return true;
                }
            }
            return true;
        }

        private void DeleteAllUnderWhereUsed(string variable, int index)
        {
            for (int i = index + 1; i < _commands.Count; i++)
            {
                if (_marks[i]) continue;

                if (_commands[i] is ICommandWithArgument)
                {
                    if ((_commands[i] as ICommandWithArgument).ContainsAsArgument(variable))
                    {
                        Delete(i);
                    }
                }
            }
        }

        private class RemoveCommand
        {
            public ICommandsList Commands { get; }
            public ICommand Command { get; }
            public int Index { get; }

            public RemoveCommand(ICommandsList commands, int index)
            {
                Commands = commands;
                Command = commands[index];
                Index = index;
            }

            public void Execute()
            {
                Commands.RemoveAt(Index);
            }

            public void Undo()
            {
                Commands.Insert(Index, Command);
            }
        }

    }
}