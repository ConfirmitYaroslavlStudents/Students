using System.Linq;
using System.Text;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;

namespace CellsAutomate.Algorithms
{
    public class SeedGenerator
    {
        public ICommand[] StartAlgorithm => new Parser().ProcessCommands(GetAlgorithm()).ToArray();

        public string GetAlgorithm()
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

                // возьмём состояние вокруг
                .AppendLine("int upState")
                .AppendLine("int rightState")
                .AppendLine("int downState")
                .AppendLine("int leftState")
                .AppendLine("upState    = getState 0")
                .AppendLine("rightState = getState 1")
                .AppendLine("downState  = getState 2")
                .AppendLine("leftState  = getState 3")

                // посчитаем сколько свободных клеток
                .AppendLine("int directionsToGo")
                .AppendLine("directionsToGo = 0")

                .AppendLine("int upState_isEmpty")
                .AppendLine("int rightState_isEmpty")
                .AppendLine("int downState_isEmpty")
                .AppendLine("int leftState_isEmpty")

                .AppendLine("upState_isEmpty    = upState    - four")
                .AppendLine("rightState_isEmpty = rightState - four")
                .AppendLine("downState_isEmpty  = downState  - four")
                .AppendLine("leftState_isEmpty  = leftState  - four")

                .AppendLine("if upState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                .AppendLine("if rightState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                .AppendLine("if downState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                .AppendLine("if leftState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                //Если некуда идти, выведем 0, т.е. "стой на месте"
                .AppendLine("int minus_directionsToGo")
                .AppendLine("minus_directionsToGo = zero - directionsToGo")
                .AppendLine("if directionsToGo then")
                .AppendLine("if minus_directionsToGo then")
                .AppendLine("print zero")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                //Решим куда шагнуть
                .AppendLine("int selectedCell")
                .AppendLine("selectedCell = random directionsToGo")

                //Шагнём
                .AppendLine("int counter")
                .AppendLine("counter = zero")
                .AppendLine("int isThisCell")

                .AppendLine("if upState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print one")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if rightState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print two")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if downState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print three")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if leftState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print four")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif");

            return commands.ToString();
        }
    }
}
