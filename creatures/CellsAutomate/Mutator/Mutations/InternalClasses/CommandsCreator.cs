using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.CommandsList;
using Creatures.Language.Commands;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{
    internal class CommandsCreator
    {
        public const int MaxLengthOfName = 6;
        public const int MinimalDirection = 1;
        public const int MaximalDirection = 4;
        public const int MinimalIntValue = Int32.MinValue;
        public const int MaximalIntValue = Int32.MaxValue;

        private Random _random;
        private ICommandsList _commands;

        public CommandsCreator(Random random, ICommandsList commands)
        {
            _random = random;
            _commands = commands;
        }

        public NewInt CreateNewInt(int placeIndex)
        {
            var name = GetRandomName();
            var command = new NewInt(name);
            return command;
        }

        public SetValue CreateSetValue(int placeIndex)
        {
            var declaration = GetRandomDeclarationBefore(placeIndex);
            var name = declaration.Name;
            var value = GetRandomIntValue();
            var command = new SetValue(name, value);
            return command;
        }

        public CloneValue CreateCloneValue(int placeIndex)
        {
            var declarations = GetRandomDeclarationsBefore(placeIndex, 2);
            var targetName = declarations[0].Name;
            var sourceName = declarations[1].Name;
            var command = new CloneValue(targetName, sourceName);
            return command;
        }

        public CloseCondition CreateCloseCondition(int placeIndex)
        {
            return new CloseCondition();
        }

        public Condition CreateCondition(int placeIndex)
        {
            var declaration = GetRandomDeclarationBefore(placeIndex);
            var conditionName = declaration.Name;
            var command = new Condition(conditionName);
            return command;
        }

        public GetRandom CreateGetRandom(int placeIndex)
        {
            var declarations = GetRandomDeclarationsBefore(placeIndex, 2);
            var targetName = declarations[0].Name;
            var maxValueName = declarations[1].Name;
            var command = new GetRandom(targetName, maxValueName);
            return command;
        }

        public GetState CreateGetState(int placeIndex)
        {
            var declaration = GetRandomDeclarationBefore(placeIndex);
            var direction = GetRandomDirection();
            var command = new GetState(declaration.Name, direction);
            return command;
        }

        public Minus CreateMinus(int placeIndex)
        {
            var declarations = GetRandomDeclarationsBefore(placeIndex, 3);
            var targetName = declarations[0].Name;
            var firstSourceName = declarations[1].Name;
            var secondSourceName = declarations[2].Name;
            var command = new Minus(targetName, firstSourceName, secondSourceName);
            return command;
        }

        public Plus CreatePlus(int placeIndex)
        {
            var declarations = GetRandomDeclarationsBefore(placeIndex, 3);
            var targetName = declarations[0].Name;
            var firstSourceName = declarations[1].Name;
            var secondSourceName = declarations[2].Name;
            var command = new Plus(targetName, firstSourceName, secondSourceName);
            return command;
        }

        public Print CreatePrint(int placeIndex)
        {
            var declaration = GetRandomDeclarationBefore(placeIndex);
            return new Print(declaration.Name);
        }

        public Stop CreateStop(int placeIndex)
        {
            return new Stop();
        }

        private NewInt[] FindAllDeclarationsBefore(int index)
        {
            return _commands.Take(index).OfType<NewInt>().ToArray();
        }

        private NewInt GetRandomDeclarationBefore(int index)
        {
            return GetRandomDeclarationsBefore(index, 1)[0];
        }

        private NewInt[] GetRandomDeclarationsBefore(int index, int numbers)
        {
            var result = new NewInt[numbers];
            var declarations = FindAllDeclarationsBefore(index);
            if (declarations.Length == 0) declarations = new[] { new NewInt(GetRandomName()) };
            for (int i = 0; i < numbers; i++)
            {
                result[i] = ChooseRandom(declarations);
            }
            return result;
        }

        private T ChooseRandom<T>(IReadOnlyList<T> array)
        {
            return array[_random.Next(array.Count)];
        }

        private string GetRandomName()
        {
            var length = _random.Next(1, MaxLengthOfName + 1);
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
            return _random.Next(MinimalIntValue, MaximalIntValue);
        }

        private int GetRandomDirection()
        {
            return _random.Next(MinimalDirection, MaximalDirection + 1);
        }
    }
}