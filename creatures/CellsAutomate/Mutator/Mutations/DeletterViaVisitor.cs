using System.Collections.Generic;
using System.Linq;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    internal class DeletterViaVisitor : ICommandVisitor
    {
        private ICommand[] _commands;
        private bool[] _marks;
        private int _index;

        public DeletterViaVisitor(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToArray();
            _marks = new bool[_commands.Length];
        }

        public ICommand[] DeleteCommand(int index)
        {
            _index = index;
            Execute(_commands[_index]);

            var newCommands = new List<ICommand>();
            for (int i = 0; i < _commands.Length; i++)
            {
                if (_marks[i]) continue;

                newCommands.Add(_commands[i]);
            }
            return newCommands.ToArray();
        }

        private void Execute(ICommand command)
        {
            if (_marks[_index]) return;
            _marks[_index] = true;
            command.AcceptVisitor(this);
        }

        public void Accept(NewInt command)
        {
            DeleteAllUnderWhereUsed(command.Name, _index);
        }
        private void DeleteAllUnderWhereUsed(string variable, int index)
        {
            var indexOfthiscommand = _index;
            for (int i = index + 1; i < _commands.Length; i++)
            {
                if (!(_commands[i] is ICommandWithArgument)) continue;
                var commandToDelete = _commands[i] as ICommandWithArgument;

                if (!commandToDelete.ContainsAsArgument(variable))
                    if ((commandToDelete as ICommandSetter)?.TargetName != variable)
                        continue;

                _index = i;
                Execute(commandToDelete);
                _index = indexOfthiscommand;
            }
        }
        public void Accept(SetValue command)
        {
            if (IsInitializeSetter(command, _index))
                DeleteAllUnderWhereUsed(command.TargetName, _index);
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
        public void Accept(Plus command)
        {
            if (IsInitializeSetter(command, _index))
                DeleteAllUnderWhereUsed(command.TargetName, _index);
        }

        public void Accept(Print command)
        {
            return;
        }

        public void Accept(Minus command)
        {
            if (IsInitializeSetter(command, _index))
                DeleteAllUnderWhereUsed(command.TargetName, _index);
        }

        public void Accept(CloneValue command)
        {
            if (IsInitializeSetter(command, _index))
                DeleteAllUnderWhereUsed(command.TargetName, _index);
        }

        public void Accept(Condition command)
        {
            var countOfAnotherConditions = 0;
            var indexOfthiscommand = _index;
            for (int i = indexOfthiscommand + 1; i < _commands.Length; i++)
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
                    _index = i;
                    Execute(_commands[_index]);
                    _index = indexOfthiscommand;
                    break;
                }
            }
        }

        public void Accept(Stop command)
        {
            return;
        }

        public void Accept(CloseCondition command)
        {
            var countOfAnotherCloseConditions = 0;
            var indexOfthiscommand = _index;
            for (int i = indexOfthiscommand - 1; i >= 0; i--)
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
                    _index = i;
                    Execute(_commands[_index]);
                    _index = indexOfthiscommand;
                    break;
                }
            }
        }

        public void Accept(GetState command)
        {
            if (IsInitializeSetter(command, _index))
                DeleteAllUnderWhereUsed(command.TargetName, _index);
        }

        public void Accept(GetRandom command)
        {
            if (IsInitializeSetter(command, _index))
                DeleteAllUnderWhereUsed(command.TargetName, _index);
        }
    }
}