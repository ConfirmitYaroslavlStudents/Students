using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandCreation
{
    public class ArgumentParser
    {
        private string[] _args;
        private string _commandName;

        public readonly Dictionary<string, int[]> CommandArguments = new Dictionary<string, int[]>()
        {
            {CommandNames.Help, new[] {1, 2}},
            {CommandNames.Rename, new [] {3,4} },
            {CommandNames.ChangeTags, new [] {3,4}},
            {CommandNames.Analyse, new [] {3}},
            {CommandNames.Sync, new [] {3,4}},
        };

        public ArgumentParser(string[] args)
        {
            _args = args;
            CheckForEmptiness();
            _commandName = _args[0];
        }
        private void CheckForEmptiness()
        {
            if (_args == null)
                throw new ArgumentNullException("args");
            if (_args.Length == 0)
                throw new ArgumentException("You haven't passed any argument!");
        }
                
        public void CheckIfCanBeExecuted()
        {            
            CheckForProperCommandName();
            CheckForProperNumberOfArguments();
        }

        private void CheckForProperCommandName()
        {
            if(!CommandArguments.Keys.Contains(_commandName))
                throw new InvalidOperationException("Invalid operation: there is no such command!");
        }

        private void CheckForProperNumberOfArguments()
        {
            if (!CommandArguments[_commandName].Contains(_args.Length))
            {
                string errorMessage = CreateErrorMessage();
                throw new ArgumentException(errorMessage);
            }
        }

        private string CreateErrorMessage()
        {
            int length = CommandArguments[_commandName].Length;
            string num = CommandArguments[_commandName][0].ToString();
            if (CommandArguments[_commandName].Length != 1)
            {
                for (int i = 1; i < length; i++)
                {
                    num += " or " + CommandArguments[_commandName][i];
                }
            }
            string message = String.Format("Wrong number of arguments passed. {0} passed. {1} expected", _args.Length, num);
            return message;
        }
    }
}
