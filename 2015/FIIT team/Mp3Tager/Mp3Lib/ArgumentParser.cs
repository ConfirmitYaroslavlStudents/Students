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
                throw new ArgumentException("Not enough argumens!");
            if (CommandList.Keys.Contains(args[0]))
                if (CommandList[args[0]].Contains(args.Length))
                    return true;
            return false;
        }
    }
}
