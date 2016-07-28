using System;
using System.Drawing;
using System.Linq;
using CellsAutomate.Constants;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate
{
    public class Creature : BaseCreature
    {
        private readonly Random _random;
        private readonly Executor _executor;
        private readonly ICommand[] _commands;

        public Creature(Point position, Executor executor, ICommand[] commands, Random random, int generation)
        {
            Position = position;
            _executor = executor;
            _commands = commands;
            _random = random;
            Generation = generation;
        }

        public override Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, BaseCreature[,] creatures)
        {
            if (FoodSupply < CreatureConstants.MinFoodToSurvive)
                return Tuple.Create(ActionEnum.Die, DirectionEnum.Stay);

            FoodSupply -= CreatureConstants.MinFoodToSurvive;

            return Tuple.Create(GetAction(eatMatrix), DirectionEx.DirectionByNumber(GetDirection(eatMatrix, creatures)));
        }

        public override BaseCreature MakeChild(Point position)
        {
            FoodSupply -= CreatureConstants.ChildPrice;
            return new SimpleCreature(position, new Random(), Generation + 1);
        }

        private int GetDirection(FoodMatrix eatMatrix, BaseCreature[,] creatures)
        {
            var state =
                    DirectionEx
                        .GetPoints(Position.X, Position.Y)
                        .ToDictionary(x => DirectionEx.DirectionByPointsWithNumber(Position, x),
                        x => (DirectionEx.IsValid(x, eatMatrix.Length, eatMatrix.Height)
                        && eatMatrix.HasFood(x) && DirectionEx.IsFree(Position, creatures)) ? 4 : 0);

            var result = _executor.Execute(_commands, new MyExecutorToolset(_random, state));
            return int.Parse(result);
        }

        private ActionEnum GetAction(FoodMatrix eatMatrix)
        {
            if (FoodSupply < CreatureConstants.CriticalLevelOfFood && eatMatrix.HasFood(Position))
                return ActionEnum.Eat;
            var result = _random.Next(3) + 1;
            switch (result)
            {
                case 1: return ActionEnum.MakeChild;
                case 2: return ActionEnum.Go;
                case 3: return ActionEnum.Eat;
                default: throw new Exception();
            }
        }
    }
}
