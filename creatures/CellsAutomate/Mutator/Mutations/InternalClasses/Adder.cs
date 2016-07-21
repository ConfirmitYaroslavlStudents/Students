using System;
using System.Collections.Generic;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{
    internal class Adder : ICommandVisitor
    {
        private static readonly ICommand[] AllTypes =
        {
            new NewInt(""), new CloseCondition(), new Condition(""), 
            new GetRandom("",""), new GetState("",0), new Minus("","",""), new NewInt(""), 
            new Plus("","",""), new Print(""), new SetValue("",0), new Stop() 
        };

        private const int MaxLengthOfName = 6;

        private ICommand[] _commands;
        private Random _rnd;

        public Adder(ICommand[] commands, Random rnd)
        {
            _commands = commands;
            _rnd = rnd;
        }

        public ICommand[] AddCommand()
        {
            var addedType = AllTypes[_rnd.Next(AllTypes.Length)];
            Execute(addedType);
            return _commands;
        }

        private void Execute(ICommand command)
        {
            command.AcceptVisitor(this);
        }

        private void Insert(ICommand commandToInsert, int index)
        {
            var newCommands = new ICommand[_commands.Length + 1];
            Array.Copy(_commands, 0, newCommands, 0, index);
            newCommands[index] = commandToInsert;

            if (index != _commands.Length + 1)
                Array.Copy(_commands, index, newCommands, index + 1, _commands.Length - index);

            _commands = newCommands;
        }

        public void Accept(NewInt command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var newCommand = new NewInt(GetRandomName());
            Insert(newCommand, insertIndex);
        }

        public void Accept(SetValue command)
        {
            var insertIndex = _rnd.Next(_commands.Length+1);
            var declarations = FindDeclarationsBefore(insertIndex);

            var randomDeclaration = declarations[_rnd.Next(declarations.Length)];
            var newCommand = new SetValue(randomDeclaration.Name, GetRandomIntValue());
            Insert(newCommand, insertIndex);
        }

        public void Accept(Plus command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var declarations = FindDeclarationsBefore(insertIndex);

            var randomTarget = declarations[_rnd.Next(declarations.Length)];
            var randomFirstSource = declarations[_rnd.Next(declarations.Length)];
            var randomSecondSource = declarations[_rnd.Next(declarations.Length)];

            var newCommand = new Plus(randomTarget.Name, randomFirstSource.Name, randomSecondSource.Name);
            Insert(newCommand, insertIndex);
        }

        public void Accept(Print command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var declarations = FindDeclarationsBefore(insertIndex);

            var randomDeclaration = declarations[_rnd.Next(declarations.Length)];
            var newCommand = new Print(randomDeclaration.Name);
            Insert(newCommand, insertIndex);
        }

        public void Accept(Minus command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var declarations = FindDeclarationsBefore(insertIndex);

            var randomTarget = declarations[_rnd.Next(declarations.Length)];
            var randomFirstSource = declarations[_rnd.Next(declarations.Length)];
            var randomSecondSource = declarations[_rnd.Next(declarations.Length)];

            var newCommand = new Minus(randomTarget.Name, randomFirstSource.Name, randomSecondSource.Name);
            Insert(newCommand, insertIndex);
        }

        public void Accept(CloneValue command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var declarations = FindDeclarationsBefore(insertIndex);

            var randomTarget = declarations[_rnd.Next(declarations.Length)];
            var randomSource = declarations[_rnd.Next(declarations.Length)];

            var newCommand = new CloneValue(randomTarget.Name, randomSource.Name);
            Insert(newCommand, insertIndex);
        }

        public void Accept(Condition command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var declarations = FindDeclarationsBefore(insertIndex);
            var randomTarget = declarations[_rnd.Next(declarations.Length)];

            var newCommand = new Condition(randomTarget.Name);
            Insert(newCommand, insertIndex);
            Accept(new CloseCondition());
        }

        public void Accept(Stop command)
        {
            var newCommand = new Stop();
            var insertIndex = _rnd.Next(_commands.Length + 1);
            Insert(newCommand, insertIndex);
        }

        public void Accept(CloseCondition command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var newCommand=new CloseCondition();
            Insert(newCommand, insertIndex);
        }

        public void Accept(GetState command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var declarations = FindDeclarationsBefore(insertIndex);

            var randomTarget = declarations[_rnd.Next(declarations.Length)];
            var newCommand = new GetState(randomTarget.Name, GetRandomIntValue());
            Insert(newCommand, insertIndex);
        }

        public void Accept(GetRandom command)
        {
            var insertIndex = _rnd.Next(_commands.Length + 1);
            var declarations = FindDeclarationsBefore(insertIndex);

            var randomTarget = declarations[_rnd.Next(declarations.Length)];
            var randomSource = declarations[_rnd.Next(declarations.Length)];
            var newCommand = new GetRandom(randomTarget.Name, randomSource.Name);
            Insert(newCommand, insertIndex);
        }

        private string GetRandomName()
        {
            var length = _rnd.Next(1,MaxLengthOfName+1);
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
                result += alphabet[_rnd.Next(alphabet.Length)];

            return result;
        }

        private char[] GetASCIILetters()
        {
            const int lowLimit = 0x61;
            const int highLimit = 0x7A;
            var alphabet=new List<char>(highLimit-lowLimit);

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
            var declarations=new List<NewInt>();
            for (int i = index - 1; i >= 0; i--)
                if (_commands[i] is NewInt) declarations.Add(_commands[i] as NewInt);

            if (declarations.Count == 0) declarations.Add(new NewInt(GetRandomName()));
            return declarations.ToArray();
        }
    }
}
