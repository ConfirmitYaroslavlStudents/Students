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


        private void CheckForProperNumberOfArguments(int[] numberOfArguments)
        {
            CheckForEmptiness();
            if (!numberOfArguments.Contains(_args.Length))
            {
                string errorMessage = CreateErrorMessage(numberOfArguments);
                throw new ArgumentException(errorMessage);
            }
        }

        private string CreateErrorMessage(int[] numberOfArguments)
        {
            int length = numberOfArguments.Length;
            string num = numberOfArguments[0].ToString();
            if (numberOfArguments.Length != 1)
            {
                for (int i = 1; i < length; i++)
                {
                    num += " or " + numberOfArguments[i];
                }
            }
            string message = String.Format("Wrong number of arguments passed. {0} passed. {1} expected", _args.Length, num);
            return message;
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
            CheckForProperNumberOfArguments(numOfCommandArguments);
        }

    }
}
