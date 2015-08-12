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
        
        private void CheckForTheRightCommandName(string commandName)
        {
            CheckForEmptiness();
            if (commandName != _args[0])
                throw new InvalidOperationException("Invalid operation: there is no such command!");
        }

        private void CheckForProperNumberOfArguments(int[] numberOfArguments)
        {
            CheckForEmptiness();
            // todo: do we need here array?
            if (!numberOfArguments.Contains(_args.Length))
                // todo: detailed info what was wrong
                throw new ArgumentException("Not enough arguments for this command!");
        }

        public void CheckForEmptiness()
        {
            if (_args == null)
                throw new ArgumentNullException("args");
            if (_args.Length == 0)
                throw new ArgumentException("You haven't passed any argument!");
        }

        public void CheckIfCanBeExecuted(string[] args, int[] numOfCommandArguments)
        {
            var parser = new ArgumentParser(args);
            parser.CheckForProperNumberOfArguments(numOfCommandArguments);
            parser.CheckForTheRightCommandName(args[0]);
        }

    }
}
