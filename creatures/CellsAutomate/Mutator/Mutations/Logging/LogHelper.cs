using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;

namespace CellsAutomate.Mutator.Mutations.Logging
{
    public static class LogHelper
    {
        private static CommandToStringParser _toStringParser = new CommandToStringParser();
        private static Parser _toCommandParser = new Parser();

        public static string CreateAddMutationLog(ICommand added, int index)
        {
            var parsedCommand = _toStringParser.ParseCommand(added);
            return $"Command \"{parsedCommand}\" added at \"{index}\"";
        }

        public static string CreateDeleteMutationLog(ICommand deleted, int index)
        {
            var parsedCommand = _toStringParser.ParseCommand(deleted);
            return $"Command \"{parsedCommand}\" deleted from \"{index}\"";
        }

        public static string CreateDublicateMutationLog(ICommand dublicated, int from, int to)
        {
            var parsedCommand = _toStringParser.ParseCommand(dublicated);
            return $"Command \"{parsedCommand}\" dublicated from \"{from}\" to \"{to}\"";
        }

        public static string CreateReplaceMutationLog(ICommand replaced, ICommand result, int index)
        {
            var parsedReplacedCommand = _toStringParser.ParseCommand(replaced);
            var parsedResultCommand = _toStringParser.ParseCommand(result);
            return $"Command \"{parsedReplacedCommand}\" replaced to \"{parsedResultCommand}\" at \"{index}\"";
        }

        public static string CreateSwapMutationLog(ICommand swappedfirst, int firstIndex, 
            ICommand swappedsecond, int secondIndex)
        {
            var parsedfirst = _toStringParser.ParseCommand(swappedfirst);
            var parsedsecond = _toStringParser.ParseCommand(swappedsecond);
            return
                $"Commands \"{parsedfirst}\" at \"{firstIndex}\" and \"{parsedsecond}\" at \"{secondIndex}\" have been swapped";
        }

        public static Tuple<ICommand,int> ParseAddMutationLog(string log)
        {
            const string pattern = "\".*\"";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var collection = regex.Matches(log);
            if (collection.Count != 2) throw new ArgumentException(nameof(log));
            try
            {
                var command = _toCommandParser.ProcessCommand(collection[0].Value.Trim('"'));
                var index = int.Parse(collection[1].Value.Trim('"'));
                return Tuple.Create(command, index);
            }
            catch (Exception)
            {
                throw new ArgumentException(nameof(log));
            }
        }
    }
}
