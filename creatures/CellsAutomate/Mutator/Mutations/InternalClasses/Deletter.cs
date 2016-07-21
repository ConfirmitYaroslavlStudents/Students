using System.Collections.Generic;
using System.Linq;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{
    internal class Deletter
    {
        private ICommand[] _commands;
        private bool[] _marks;
        public Deletter(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToArray();
            _marks = new bool[_commands.Length];
        }

        public ICommand[] DeleteCommand(int index)
        {
            Delete(index);
            var newCommands = new List<ICommand>();
            for (int i = 0; i < _commands.Length; i++)
            {
                if (_marks[i]) continue;

                newCommands.Add(_commands[i]);
            }
            return newCommands.ToArray();
        }

        private void Delete(int index)
        {
            _marks[index] = true;
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
            if (comToDel is ICommandLonely)
            {

            }
            if (comToDel is ICommandWithConstruction)
            {
                AssertCommandWithConstruction(index);
            }
        }

        private void AssertDeclarationCommand(int index)
        {
            var comToDel = _commands[index] as ICommandDeclaration;
            for (int i = index + 1; i < _commands.Length; i++)
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
                for (int i = index + 1; i < _commands.Length; i++)
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
            for (int i = index + 1; i < _commands.Length; i++)
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
    }
}