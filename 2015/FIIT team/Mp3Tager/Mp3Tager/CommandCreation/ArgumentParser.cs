using System;
using System.Linq;

namespace CommandCreation
{
    internal class ArgumentParser
    {
        private string[] _args;
        public ArgumentParser(string[] args)
        {
            _args = args;
        }
        
        public void CheckForTheRightCommandName(string commandName)
        {
            CheckForEmptiness();
            if (commandName != _args[0])
                throw new InvalidOperationException("Invalid operation: there is no such command!");
        }

        public void CheckForProperNumberOfArguments(int[] numberOfArguments)
        {
            CheckForEmptiness();
            if (!numberOfArguments.Contains(_args.Length))
                throw new ArgumentException("Not enough arguments for this command!");
        }

        public void CheckForEmptiness()
        {
            if (_args == null)
                throw new ArgumentNullException("args");
            if (_args.Length == 0)
                throw new ArgumentException("You haven't passed any argument!");
        }

    }
}
