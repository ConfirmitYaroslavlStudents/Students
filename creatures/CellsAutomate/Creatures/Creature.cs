using System;
using System.Drawing;
using System.Linq;
using CellsAutomate.Constants;
using CellsAutomate.Food;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate.Creatures
{
    public class Creature : BaseCreature
    {
        private readonly Executor _executor;
        private readonly ICommand[] _commands;

        public Creature(Executor executor, ICommand[] commands)
        {
            _executor = executor;
            _commands = commands;
        }

        protected override DirectionEnum GetDirection(FoodMatrix eatMatrix, Membrane[,] creatures, Point position, Random random)
        {
            var state =
                    DirectionEx
                        .GetPoints(position.X, position.Y)
                        .ToDictionary(x => DirectionEx.DirectionByPointsWithNumber(position, x),
                        x => (DirectionEx.IsValidAndFree(x, creatures)
                        && eatMatrix.GetLevelOfFood(x) >= CreatureConstants.OneBite) ? 4 : 0);

            var result = _executor.Execute(_commands, new MyExecutorToolset(random, state));
            return DirectionEx.DirectionByNumber(int.Parse(result));
        }

        protected override ActionEnum GetAction(FoodMatrix eatMatrix, int energyPoints, Point position, Random random, bool canMakeChild)
        {
            if (energyPoints < CreatureConstants.CriticalLevelOfFood)
                return eatMatrix.GetLevelOfFood(position) >= CreatureConstants.OneBite ? ActionEnum.Eat : ActionEnum.Go;

            if (canMakeChild)
                return ActionEnum.MakeChild;

            return random.Next(100) % 2 == 1 ? ActionEnum.Eat : ActionEnum.Go;
        }
    }
}
