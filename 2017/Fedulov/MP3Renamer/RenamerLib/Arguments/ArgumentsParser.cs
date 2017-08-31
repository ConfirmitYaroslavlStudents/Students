using System;
using System.Collections.Generic;

namespace RenamerLib.Arguments
{
    public class ArgumentsParser
    {
        private static readonly Dictionary<string, AllowedActions> PossibleActions =
            new Dictionary<string, AllowedActions>
            {
                {"-toFileName", AllowedActions.ToFileName},
                {"-toTag", AllowedActions.ToTag}
            };

        public RenamerArguments ParseArguments(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
                throw new ArgumentException("Wrong number of parameters provided!");

            RenamerArguments renamerArguments = new RenamerArguments
            {
                Mask = args[0],
                IsRecursive = false
            };

            for (int i = 1; i < args.Length; ++i)
            {
                if (Equals(args[i], "-recursive"))
                    renamerArguments.IsRecursive = true;
                else if (PossibleActions.ContainsKey(args[i]))
                    renamerArguments.Action = PossibleActions[args[i]];
                else
                    throw new ArgumentException("Unknown argument!");
            }

            return renamerArguments;
        }
    }
}