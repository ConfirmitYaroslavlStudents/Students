using System;
using System.Linq;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Parsers
{
    public class Constructor
    {
        public ICommand NewInt(string command)
        {
            return new NewInt(CheckTypeNamePair("int", command));
        }

        public ICommand NewArray(string command)
        {
            var exception = new ArgumentException("Should be 'array <name> : v1, v2, v3...', but it is: " + command);

            var parts = command.Split(':').ToList();
            
            if (parts.Count != 2)
                throw exception;

            var name = CheckTypeNamePair("array", parts[0]);

            return new NewArray(name, parts[1].Split(',').Select(item => int.Parse(item.Trim())));
        }

        public ICommand CloneValue(string command)
        {
            var exception = new ArgumentException("Should be '<name_target> = <name_soure>', but it is: " + command);
            var parts = command.Split('=').Select(item=>item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            if (IsIdentifier(parts[0]) && IsIdentifier(parts[1]))
                return new CloneValue(parts[0], parts[1]);

            throw exception;
        }

        public ICommand SetValue(string command)
        {
            var exception = new ArgumentException("Should be '<name_target> = <value>', but it is: " + command);
            var parts = command.Split('=').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            var value = int.Parse(parts[1]);
            if (IsIdentifier(parts[0]))
                return new SetValue(parts[0], value);

            throw exception;
        }

        public ICommand Plus(string command)
        {
            var exception = new ArgumentException("Should be '<name_target> = <name_source1> + <name_source_2>', but it is: " + command);
            var parts = command.Split('=').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            var partsLeft = parts[1].Split('+').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            if (IsIdentifier(parts[0]) && IsIdentifier(partsLeft[0]) && IsIdentifier(partsLeft[1]))
                return new Plus(parts[0], partsLeft[0], partsLeft[1]);

            throw exception;
        }

        public ICommand Minus(string command)
        {
            var exception = new ArgumentException("Should be '<name_target> = <name_source1> - <name_source_2>', but it is: " + command);
            var parts = command.Split('=').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            var partsLeft = parts[1].Split('-').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            if (IsIdentifier(parts[0]) && IsIdentifier(partsLeft[0]) && IsIdentifier(partsLeft[1]))
                return new Minus(parts[0], partsLeft[0], partsLeft[1]);

            throw exception;
        }

        public ICommand Print(string command)
        {
            var exception = new ArgumentException("Should be 'print <name>', but it is: " + command);

            var parts = command.Split(' ').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            if (parts[0] != "print")
                throw exception;

            if (IsIdentifier(parts[1]))
                return new Print(parts[1]);

            throw exception;
        }

        public ICommand Condition(string command)
        {
            var exception = new ArgumentException("Should be 'if <name> then', but it is: " + command);
            var parts = command.Split(' ').Select(item => item.Trim()).ToList();
            if (parts[0] != "if")
                throw exception;
            if (!IsIdentifier(parts[1]))
                throw new ArgumentException("Identifier expected, but have : " + parts[1]);
            if (parts[2] != "then")
                throw exception;

            return new Condition(parts[1]);
        }

        public ICommand CloseCondition(string command)
        {
            var exception = new ArgumentException("Should be 'endif', but it is: " + command);
            if (command.Trim() != "endif")
                throw exception;

            return new CloseCondition();
        }

        public ICommand GetState(string command)
        {
            var exception = new ArgumentException("Should be '<name_target> = getState <0..3>', but it is: " + command);
            var parts = command.Split('=').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            var partsLeft = parts[1].Split(' ').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            if (IsIdentifier(parts[0]) && partsLeft[0] == "getState")
                return new GetState(parts[0], int.Parse(partsLeft[1]));

            throw exception;
        }

        public ICommand GetRandom(string command)
        {
            var exception = new ArgumentException("Should be '<name_target> = random <name_source>', but it is: " + command);
            var parts = command.Split('=').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            var partsLeft = parts[1].Split(' ').Select(item => item.Trim()).ToList();
            if (parts.Count != 2)
                throw exception;

            if (IsIdentifier(parts[0]) && partsLeft[0] == "random" && IsIdentifier(partsLeft[1]))
                return new GetRandom(parts[0], partsLeft[1]);

            throw exception;
        }

        public ICommand Stop(string command)
        {
            var exception = new ArgumentException("Should be 'stop', but it is: " + command);

            if (command.Trim() == "stop")
                return new Stop();

            throw exception;
        }

        private string CheckTypeNamePair(string type, string value)
        {
            var exception = new ArgumentException(string.Format("Should be '{0} <name>', but it is: ", type) + value);

            var parts = value.Split(' ').Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            if (parts.Count != 2)
                throw exception;
            if (parts[0] != type)
                throw exception;
            if (IsIdentifier(parts[1]))
                throw new ArgumentException("Identifier expected, but have : " + parts[1]);

            return parts[1];
        }

        private bool IsIdentifier(string value)
        {
            if (value.Length == 0)
                return false;

            return value.All(x =>  char.IsLetter(x) || x == '_');
        }
    }
}
