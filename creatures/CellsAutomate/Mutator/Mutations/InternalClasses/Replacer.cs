using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.Mutations.Logging;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{
    internal class Replacer : ICommandVisitor
    {
        private const int MaxLengthOfName = 6;

        private ICommand[] _commands;
        private Random _rnd;
        private int _index;

        private readonly bool _logging = false;
        public ILogger Logger { get; }

        public Replacer(IEnumerable<ICommand> commands, Random random)
        {
            _commands = commands.ToArray();
            _rnd = random;
        }

        public Replacer(IEnumerable<ICommand> commands, Random random, ILogger logger) : this(commands, random)
        {
            _logging = true;
            Logger = logger;
        }

        public ICommand[] Replace(int index)
        {
            _index = index;
            var prev = _commands[index];
            Execute(_commands[index]);
            if (_logging) Logger.Write(LogHelper.CreateReplaceMutationLog(prev, _commands[index], index));
            return _commands;
        }

        private void Execute(ICommand command)
        {
            command.AcceptVisitor(this);
        }

        public void Accept(NewInt command)
        {
            var newCommand = new NewInt(GetRandomName());
            _commands[_index] = newCommand;
        }

        public void Accept(SetValue command)
        {
            int newValue;
            do
            {
                newValue = GetRandomIntValue();
            } while (command.Value == newValue);
            var newCommand = new SetValue(command.TargetName, newValue);
            _commands[_index] = newCommand;
        }

        public void Accept(Plus command)
        {
            var declarations = FindDeclarationsBefore(_index);

            var targetName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.TargetName;
            var firstSourceName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.FirstSource;
            var secondSourceName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.SecondSource;

            var newCommand = new Plus(targetName, firstSourceName, secondSourceName);
            _commands[_index] = newCommand;
        }

        public void Accept(Print command)
        {
            var declarations = FindDeclarationsBefore(_index);
            var newCommand = new Print(ChooseRandom(declarations).Name);
            _commands[_index] = newCommand;
        }

        public void Accept(Minus command)
        {
            var declarations = FindDeclarationsBefore(_index);
            var targetName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.TargetName;
            var firstSourceName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.FirstSource;
            var secondSourceName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.SecondSource;

            var newCommand = new Minus(targetName, firstSourceName, secondSourceName);
            _commands[_index] = newCommand;
        }

        public void Accept(CloneValue command)
        {
            var declarations = FindDeclarationsBefore(_index);
            var targetName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.TargetName;
            var sourceName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.SourceName;

            var newCommand = new CloneValue(targetName, sourceName);
            _commands[_index] = newCommand;
        }

        public void Accept(Condition command)
        {
            var declarations = FindDeclarationsBefore(_index);
            var conditionName = ChooseRandom(declarations).Name;

            var newCommand = new Condition(conditionName);
            _commands[_index] = newCommand;
        }

        public void Accept(Stop command)
        {
            return;
        }

        public void Accept(CloseCondition command)
        {
            return;
        }

        public void Accept(GetState command)
        {
            var declarations = FindDeclarationsBefore(_index);

            var direction = (GetRandomAccess()) ? GetRandomIntValue() : command.Direction;
            var targetName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.TargetName;

            var newCommand = new GetState(targetName, direction);
            _commands[_index] = newCommand;
        }

        public void Accept(GetRandom command)
        {
            var declarations = FindDeclarationsBefore(_index);

            var targetName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.TargetName;
            var sourceName = (GetRandomAccess()) ? ChooseRandom(declarations).Name : command.MaxValueName;

            var newCommand = new GetRandom(targetName, sourceName);
            _commands[_index] = newCommand;
        }

        private string GetRandomName()
        {
            var length = _rnd.Next(MaxLengthOfName);
            return GetRandomName(length);
        }

        /// <summary>
        /// ASCII Name
        /// </summary>
        private string GetRandomName(int length)
        {
            var alphabet = GetASCIILetters();
            string result = string.Empty;
            for (int i = 0; i < length; i++)
                result += ChooseRandom(alphabet);

            return result;
        }

        private char[] GetASCIILetters()
        {
            const int lowLimit = 0x61;
            const int highLimit = 0x7A;
            var alphabet = new List<char>(highLimit - lowLimit);

            for (char ch = (char)lowLimit; ch <= highLimit; ch++)
                alphabet.Add(ch);

            return alphabet.ToArray();
        }

        private int GetRandomIntValue()
        {
            return _rnd.Next(Int32.MinValue, Int32.MaxValue);
        }

        private NewInt[] FindDeclarationsBefore(int index)
        {
            var declarations = new List<NewInt>();
            for (int i = index - 1; i >= 0; i--)
                if (_commands[i] is NewInt) declarations.Add(_commands[i] as NewInt);

            return declarations.ToArray();
        }

        private bool GetRandomAccess(double probability = 0.5)
        {
            return _rnd.NextDouble() <= probability;
        }

        private T ChooseRandom<T>(T[] array)
        {
            return array[_rnd.Next(array.Length)];
        }
    }
}