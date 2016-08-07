using System.Linq;
using System.Text;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;

namespace CellsAutomate
{
    public class GetDirectionAlgorithm
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
                    .AppendLine("int directions")
                    .AppendLine("directions = 0")
                    .AppendLine("int directionsWithFood")
                    .AppendLine("directionsWithFood = 0")

                    .AppendLine("int upState_isEmpty")
                    .AppendLine("int rightState_isEmpty")
                    .AppendLine("int downState_isEmpty")
                    .AppendLine("int leftState_isEmpty")

                    .AppendLine("int upState_isEmptyAndHasFood")
                    .AppendLine("int rightState_isEmptyAndHasFood")
                    .AppendLine("int downState_isEmptyAndHasFood")
                    .AppendLine("int leftState_isEmptyAndHasFood")

                    .AppendLine("upState_isEmpty    = upState    - three")
                    .AppendLine("rightState_isEmpty = rightState - three")
                    .AppendLine("downState_isEmpty  = downState  - three")
                    .AppendLine("leftState_isEmpty  = leftState  - three")

                    .AppendLine("upState_isEmptyAndHasFood    = upState    - four")
                    .AppendLine("rightState_isEmptyAndHasFood = rightState - four")
                    .AppendLine("downState_isEmptyAndHasFood  = downState  - four")
                    .AppendLine("leftState_isEmptyAndHasFood  = leftState  - four")

                    .AppendLine("if upState_isEmpty then")
                    .AppendLine("directions = directions + one")
                    .AppendLine("if upState_isEmptyAndHasFood then")
                    .AppendLine("directionsWithFood = directionsWithFood + one")
                    .AppendLine("endif")
                    .AppendLine("endif")

                    .AppendLine("if rightState_isEmpty then")
                    .AppendLine("directions = directions + one")
                    .AppendLine("if rightState_isEmptyAndHasFood then")
                    .AppendLine("directionsWithFood = directionsWithFood + one")
                    .AppendLine("endif")
                    .AppendLine("endif")

                    .AppendLine("if downState_isEmpty then")
                    .AppendLine("directions = directions + one")
                    .AppendLine("if downState_isEmptyAndHasFood then")
                    .AppendLine("directionsWithFood = directionsWithFood + one")
                    .AppendLine("endif")
                    .AppendLine("endif")

                    .AppendLine("if leftState_isEmpty then")
                    .AppendLine("directions = directions + one")
                    .AppendLine("if leftState_isEmptyAndHasFood then")
                    .AppendLine("directionsWithFood = directionsWithFood + one")
                    .AppendLine("endif")
                    .AppendLine("endif")

                    //Если некуда идти, выведем 0, т.е. "стой на месте"
                    .AppendLine("int minus_directions")
                    .AppendLine("minus_directions = zero - directions")
                    .AppendLine("int minus_directionsWithFood")
                    .AppendLine("minus_directionsWithFood = zero - directionsWithFood")

                    .AppendLine("if directions then")
                    .AppendLine("if minus_directions then")
                    .AppendLine("print zero")
                    .AppendLine("stop")
                    .AppendLine("endif")
                    .AppendLine("endif")

                    .AppendLine("int selectedCell")
                    .AppendLine("selectedCell = zero - one")

                    //Если есть куда идти, но там нет еды, то выберем рандомное направление из directions
                    .AppendLine("if directions then")
                    .AppendLine("if directionsWithFood then")
                    .AppendLine("if minus_directionsWithFood then")
                    .AppendLine("selectedCell = random directions")
                    .AppendLine("endif")
                    .AppendLine("endif")
                    .AppendLine("endif")

                //Шагнём, если выполнены предыдущие условия
                .AppendLine("if selectedCell then")
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
                .AppendLine("endif")
                .AppendLine("endif")

                    //Если есть куда идти и там есть еда, то выберем рандомное направление из directionsWithFood
                    .AppendLine("if directions then")
                    .AppendLine("if directionsWithFood then")
                    .AppendLine("selectedCell = random directionsWithFood")
                    .AppendLine("endif")
                    .AppendLine("endif")

                //Шагнём
                .AppendLine("int counter")
                .AppendLine("counter = zero")
                .AppendLine("int isThisCell")

                .AppendLine("if upState_isEmptyAndHasFood then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print one")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if rightState_isEmptyAndHasFood then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print two")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if downState_isEmptyAndHasFood then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print three")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if leftState_isEmptyAndHasFood then")
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
