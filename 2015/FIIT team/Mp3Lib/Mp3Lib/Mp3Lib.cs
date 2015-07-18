using System;
using System.Collections.Generic;
using System.Linq;

namespace Mp3Lib
{
    public class Mp3Lib
    {
        private Dictionary<string, List<int>> _commandList = new Dictionary<string, List<int>>()
        {
            {"help", new List<int>(){1, 2}},
            {"raname", new List<int>(){3}},
        };

        private string[] _args;


        public Mp3Lib(string[] args)
        {
            _args = args;
        }

        private bool CheckArgs(string commandName)
        {
            if (_commandList.Keys.Contains(commandName))
                if (_commandList[commandName].Contains(_args.Length))
                    return true;
            return false;
        }

        public void ShowHelp()
        {
            var helper = new Helper();
            helper.ShowInstructions();
        }

        public void ExecuteCommand()
        {
            string command = _args[0];
            IEnumerable<string> otherArgs = new ArraySegment<string>(_args, 1, _args.Length-1);

            switch (command)
            {
                case "help" : ShowHelp(); break;

                case "rename" :
                    break;
            }

        }
    }
}
