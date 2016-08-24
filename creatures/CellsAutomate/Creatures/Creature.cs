﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private readonly ICommand[] _commandsForGetDirection;
        private readonly ICommand[] _commandsForGetAction;

        public Creature(Executor executor, ICommand[] commandsForGetDirection, 
            ICommand[] commandsForGetAction)
        {
            _executor = executor;
            _commandsForGetDirection = commandsForGetDirection;
            _commandsForGetAction = commandsForGetAction;
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

            var result = _executor.Execute(_commandsForGetDirection, new MyExecutorToolset(random, state));

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

            var result = _executor.Execute(_commandsForGetAction, new MyExecutorToolset(random, state));
            
            return ActionEx.ActionByNumber(int.Parse(result));
        }

        public Creature MakeChild()
        {
            var childsDirections = Mutate(_commandsForGetDirection);
            var childsActions = Mutate(_commandsForGetAction);

            var child = new Creature(_executor, childsDirections, childsActions);
            return child;
        }

        private ICommand[] Mutate(ICommand[] commands)
        {
            var commandsList = new CommandsList(commands);
            var mutator = new Mutator.Mutator(new Random(), commandsList);
            mutator.Mutate();
            return commandsList.ToArray();
        }
    }
}
