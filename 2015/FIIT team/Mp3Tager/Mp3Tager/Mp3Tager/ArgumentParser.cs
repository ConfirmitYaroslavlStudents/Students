using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Mp3LibTests")]
namespace Mp3Tager
{
    internal class ArgumentParser
    {
        readonly Dictionary<string, int[]> CommandList = new Dictionary<string, int[]>
        {
            {"help",  new [] {1, 2}},
            {"rename", new [] {3}},
            {"changeTag", new [] {4}}
        };

        public Dictionary<string, string> ParseArguments(string[] args)
        {
            CheckArgs(args);

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

        private void CheckArgs(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("You haven't passed any argument!");
            if (!CommandList.Keys.Contains(args[0]))
                throw new InvalidOperationException("Invalid operation: there is no such command!");
            if (!CommandList[args[0]].Contains(args.Length))
                throw new ArgumentException("Not enough arguments for this command!");
        }
    }
}
