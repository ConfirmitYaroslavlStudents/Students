using System;
using System.Collections.Generic;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.NewMutations
{
    public class ReplaceCommandMutationNew : IMutationNew
    {
        private ICommandsList _commands;
        private Random _random;
        private CommandsCreator _creator;

        private readonly List<Func<int, ICommand>> _handlers;
        private readonly Stack<ReplaceCommand> _replaced;
        public ReplaceCommandMutationNew(Random random, ICommandsList commands)
        {
            _commands = commands;
            _random = random;
            _creator = new CommandsCreator(random, commands);
            _replaced = new Stack<ReplaceCommand>();
            _handlers = new List<Func<int, ICommand>>
            {
                _creator.CreateCloseCondition,
                _creator.CreateCloneValue,
                _creator.CreateCondition,
                _creator.CreateGetRandom,
                _creator.CreateGetState,
                _creator.CreateMinus,
                _creator.CreateNewInt,
                _creator.CreatePlus,
                _creator.CreatePrint,
                _creator.CreateSetValue,
                _creator.CreateStop
            };
        }

        public void Transform()
        {
            InitNextTransform();
            var replaceIndex = _random.Next(_commands.Count);
            var replacedICommand = GetReplacedVersionOf(replaceIndex);
            var replaceCommand = new ReplaceCommand(_commands, replacedICommand, replaceIndex);
            _replaced.Push(replaceCommand);
            replaceCommand.Release();
        }

        private ICommand GetReplacedVersionOf(int index)
        {
            var command = _commands[index];
            var handler = GetHandlerOfType(command.GetType());
            return handler(index);
        }

        private Func<int, ICommand> GetHandlerOfType(Type type)
        {
            return _handlers.Find(handler => handler.Method.ReturnType == type);
        }

        public void Undo()
        {
            while (_replaced.Count > 0)
            {
                var replacedCommand = _replaced.Pop();
                replacedCommand.Undo();
            }
        }

        private void InitNextTransform()
        {
            _replaced.Clear();
        }

        private class ReplaceCommand
        {
            public ICommandsList Commands { get; }
            public ICommand Previous { get; }
            public ICommand Now { get; }
            public int Index { get; }

            public ReplaceCommand(ICommandsList commands, ICommand now, int index)
            {
                Commands = commands;
                Previous = commands[index];
                Now = now;
                Index = index;
            }

            public void Release()
            {
                Commands[Index] = Now;
            }

            public void Undo()
            {
                Commands[Index] = Previous;
            }
        }
    }
}