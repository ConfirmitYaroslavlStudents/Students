using System;
using System.Collections.Generic;
using System.Linq;

namespace Mp3Tager
{
    internal class ArgumentParser
    {
        static readonly Dictionary<string, int[]> CommandList = new Dictionary<string, int[]>
        {
            {"help",  new [] {1, 2}},
            {"rename", new [] {3}},
            {"changeTag", new [] {4}}
        };

        public ParsedArgs ParseArguments(string[] args)
        {
            CheckArgs(args);

            var parsedArgs = new ParsedArgs
            {
                CommandName = args[0]
            };

            //[TODO] there should be no more than 1 switch on command
            switch (parsedArgs.CommandName)
            {
                case "help":
                    if (args.Length == 2)
                        parsedArgs.CommandForHelp = args[1];
                    break;

                case "rename":
                    parsedArgs.Path = args[1];
                    parsedArgs.Pattern = args[2];
                    break;

                case "changeTag":
                    parsedArgs.Path = args[1];
                    parsedArgs.Tag = args[2];
                    parsedArgs.NewTagValue = args[3];
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
