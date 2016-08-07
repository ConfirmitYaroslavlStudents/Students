using System.Linq;
using System.Text;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;

namespace CellsAutomate.Algorithms
{
    class GetActionAlgorithm
    {
        public ICommand[] Algorithm => new Parser().ProcessCommands(GetAlgorithm()).ToArray();

        private string GetAlgorithm()
        {
            var commands =
                new StringBuilder()
                    // зададим константы
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("int two")
                    .AppendLine("int three")
                    .AppendLine("int four")

                    .AppendLine("zero  = 0")
                    .AppendLine("one   = 1")
                    .AppendLine("two   = 2")
                    .AppendLine("three = 3")
                    .AppendLine("four  = 4")

                    .AppendLine("int hasToEat")
                    .AppendLine("int hasOneBite")
                    .AppendLine("int canMakeChild")

                    .AppendLine("hasToEat = getState 0")
                    .AppendLine("hasOneBite = getState 1")
                    .AppendLine("canMakeChild = getState 2")

                    .AppendLine("if hasToEat then")
                    .AppendLine("if hasOneBite then")
                    .AppendLine("print three")
                    .AppendLine("stop")
                    .AppendLine("endif")
                    .AppendLine("print two")
                    .AppendLine("stop")
                    .AppendLine("endif")

                    .AppendLine("if canMakeChild then")
                    .AppendLine("print one")
                    .AppendLine("stop")
                    .AppendLine("endif")

                    .AppendLine("int returnAction")
                    .AppendLine("returnAction = random two")
                    .AppendLine("returnAction = returnAction + one")
                    .AppendLine("print returnAction")
                    .AppendLine("stop");

            return commands.ToString();
        }
    }
}
