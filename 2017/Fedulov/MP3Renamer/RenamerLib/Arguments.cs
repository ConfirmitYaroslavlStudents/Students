using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenamerLib
{
    public class Arguments
    {
        public string Mask { set; get; }
        public bool IsRecursive { set; get; }
        public AllowedActions Action { set; get; }
    }

    public class ArgumentsParser
    {
        private static readonly Dictionary<string, AllowedActions> PossibleActions = new Dictionary<string, AllowedActions>
        {
            {"-toFileName", AllowedActions.toFileName },
            {"-toTag", AllowedActions.toTag }
        };

        public Arguments ParseArguments(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
                throw new ArgumentException("Wrong number of parameters provided!");

            Arguments arguments = new Arguments();

            arguments.Mask = args[0];
            arguments.IsRecursive = false;
            for (int i = 1; i < args.Length; ++i)
            {
                if (Equals(args[i], "-recursive"))
                    arguments.IsRecursive = true;
                else if (PossibleActions.ContainsKey(args[i]))
                    arguments.Action = PossibleActions[args[i]];
                else
                    throw new ArgumentException("Unknown argument!");
            }

            return arguments;
        }
    }
}
