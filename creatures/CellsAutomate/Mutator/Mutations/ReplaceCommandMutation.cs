using System;
using System.Collections.Generic;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations.InternalClasses;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
    public class ReplaceCommandMutation : IMutation
    {
        private ICommandsList _commands;
        private Random _random;
        private CommandsCreator _creator;

        private readonly List<Func<int, ICommand>> _handlers;
        private ReplaceCommand _replaced;
        public ReplaceCommandMutation(Random random, ICommandsList commands)
        {
            _commands = commands;
            _random = random;
            _creator = new CommandsCreator(random, commands);
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
            _replaced = replaceCommand;
            replaceCommand.Execute();
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
            _replaced?.Undo();
            _replaced = null;
        }

        private void InitNextTransform()
        {
            _replaced = null;
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

            public void Execute()
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