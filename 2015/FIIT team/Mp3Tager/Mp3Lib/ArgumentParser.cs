using System;
using System.Collections.Generic;
using System.Linq;

namespace Mp3Lib
{
    public static class ArgumentParser
    {
        static readonly Dictionary<string, int[]> CommandList = new Dictionary<string, int[]>
        {
            {"help",  new [] {1, 2}},
            {"rename", new [] {3}},
            {"changeTag", new [] {4}}
        };

        public static Dictionary<string, string> ParseArguments(string[] args)
        {
            if (!CheckArgs(args))
                throw new InvalidOperationException("Invalid operation: there is no such command!");

            var parsedArgs = new Dictionary<string, string>
            {
                {"commandName", args[0]}
            };

            switch (parsedArgs["commandName"])
            {
                case "help":
                    if (args.Length == 2)
                        parsedArgs.Add("commandForHelp", args[1]);
                    break;

                case "rename":
                    parsedArgs.Add("path", args[1]);
                    parsedArgs.Add("pattern", args[2]);
                    break;

                case "changeTag":
                    parsedArgs.Add("path", args[1]);
                    parsedArgs.Add("tag", args[2]);
                    parsedArgs.Add("newTagValue", args[3]);
                    break;
            }

            return parsedArgs;
        }

        private static bool CheckArgs(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Not enough arguments!");
            if (CommandList.Keys.Contains(args[0]))
                if (CommandList[args[0]].Contains(args.Length))
                    return true;
                else
                {       
                    throw new ArgumentException(CreateErrorMessage(args));
                }                       
            return false;
        }

        private static string CreateErrorMessage(string[] args)
        {
            string num = CommandList[args[0]][0].ToString();
            if (CommandList[args[0]].Length != 1)
            {
                for (int i = 1; i < CommandList[args[0]].Length; i++)
                {
                    num += " or " + CommandList[args[0]][i];
                }
            }        
            string message = String.Format("Wrong number of arguments passed. {0} passed. {1} expected", args.Length, num);
            return message;
        }
    }
}
