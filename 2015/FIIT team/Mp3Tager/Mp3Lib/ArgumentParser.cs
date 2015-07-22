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

        public static void ParseArguments(string[] args, out string commandName, out string commandForHelp, out string path, out string pattern, out string tag, out string newTagValue)
        {
            if (!CheckArgs(args))
            {
                throw new InvalidOperationException("Invalid operation: there is no such command!");
            }
            commandName = args[0];
            switch (commandName)
            {
                case "help":
                    if (args.Length == 1)
                    {
                        commandForHelp = path = pattern = tag = newTagValue = null;
                    }
                    else
                    {
                        commandForHelp = args[1];
                        path = pattern = tag = newTagValue = null;
                    }
                    break;

                case "rename":
                    path = args[1];
                    pattern = args[2];
                    commandForHelp = tag = newTagValue = null;
                    break;

                case "changeTag":
                    path = args[1];
                    tag = args[2];
                    newTagValue = args[3];
                    commandForHelp = pattern = null;
                    break;
                default:
                    commandForHelp = commandName = path = pattern = tag = newTagValue = null;
                    break;
            }
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
