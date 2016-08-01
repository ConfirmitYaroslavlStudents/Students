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

        public Creature(Point position, Executor executor, ICommand[] commands, Random random, int generation)
        {
            Position = position;
            _executor = executor;
            _commands = commands;
            Random = random;
            Generation = generation;
        }

        protected override DirectionEnum GetDirection(FoodMatrix eatMatrix, BaseCreature[,] creatures)
        {
            var state =
                    DirectionEx
                        .GetPoints(Position.X, Position.Y)
                        .ToDictionary(x => DirectionEx.DirectionByPointsWithNumber(Position, x),
                        x => (DirectionEx.IsValidAndFree(x, creatures)
                        && eatMatrix.HasFood(x)) ? 4 : 0);

            var result = _executor.Execute(_commands, new MyExecutorToolset(Random, state));
            return DirectionEx.DirectionByNumber(int.Parse(result));
        }

        public override BaseCreature MakeChild(Point position)
        {
            EnergyPoints -= CreatureConstants.ChildPrice;
            return new SimpleCreature(position, Random, Generation + 1);
        }
    }
}
