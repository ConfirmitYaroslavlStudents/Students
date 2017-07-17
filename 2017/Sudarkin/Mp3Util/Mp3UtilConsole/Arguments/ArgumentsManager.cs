using System;
using System.Collections.Generic;

namespace Mp3UtilConsole.Arguments
{
    public class ArgumentsManager
    {
        private static readonly Dictionary<string, ProgramAction> AllowedActions = new Dictionary<string, ProgramAction>
        {
            { "-toFileName",    ProgramAction.ToFileName },
            { "-toTag",         ProgramAction.ToTag }
        };

        public static Args Parse(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Not specified search pattern!");
            }

            //Default params
            string mask = args[0];
            bool recursive = false;
            ProgramAction action = ProgramAction.ToFileName;

            for(int i = 1; i < args.Length; i++)
            {
                if (args[i] == "-recursive")
                {
                    recursive = true;
                }
                else if (AllowedActions.ContainsKey(args[i]))
                {
                    action = AllowedActions[args[i]];
                }
                else
                {
                    throw new ArgumentException("Unknown argument!");
                }
            }

            return new Args(mask, recursive, action);
        }
    }
}