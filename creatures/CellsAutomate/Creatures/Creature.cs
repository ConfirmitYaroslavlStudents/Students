using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CellsAutomate.Food;
using CellsAutomate.Mutator.CommandsList;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;
using CellsAutomate.Tools;
using CellsAutomate.Constants;

namespace CellsAutomate.Creatures
{
    public class Creature : BaseCreature
    {
        private readonly Executor _executor;
        public ICommand[] CommandsForGetDirection { get; }
        public ICommand[] CommandsForGetAction { get; }

        public Creature(Executor executor, ICommand[] commandsForGetDirection, 
            ICommand[] commandsForGetAction)
        {
            _executor = executor;
            CommandsForGetDirection = commandsForGetDirection;
            CommandsForGetAction = commandsForGetAction;
        }

        protected override DirectionEnum GetDirection(FoodMatrix eatMatrix, 
            Membrane[,] creatures, Point position, Random random)
        {
            var points = CommonMethods.GetPoints(position);
            var state = new Dictionary<int, int>();

            foreach (var point in points)
            {
                var direction = DirectionEx.DirectionByPointsWithNumber(position, point);

                if (CommonMethods.IsValidAndFree(point, creatures))
                    state.Add(direction, eatMatrix.HasOneBite(point) ? 4 : 3);

                if (!CommonMethods.IsValid(point, eatMatrix.Length, eatMatrix.Height))
                    state.Add(direction, 1);
                else
                    if (!CommonMethods.IsFree(point, creatures))
                        state.Add(direction, 2);
            }

            var result = _executor.Execute(CommandsForGetDirection, new MyExecutorToolset(random, state));

            return DirectionEx.DirectionByNumber(int.Parse(result));
        }

        protected override ActionEnum GetAction(Random random, bool hasOneBite, int energyPoints)
        {
            var state = new Dictionary<int, int>
            {
                {0, energyPoints < CreatureConstants.CriticalLevelOfFood ? 0 : -1},
                {1, hasOneBite ? 0 : -1},
                {2, energyPoints >= CreatureConstants.ChildPrice ? 0 : -1}
            };

            var result = _executor.Execute(CommandsForGetAction, new MyExecutorToolset(random, state));
            
            return ActionEx.ActionByNumber(int.Parse(result));
        }
    }


}
