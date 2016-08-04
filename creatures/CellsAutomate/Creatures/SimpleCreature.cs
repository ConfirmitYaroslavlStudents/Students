using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CellsAutomate.Constants;
using CellsAutomate.Food;

namespace CellsAutomate.Creatures
{
    public class SimpleCreature : BaseCreature
    {
        protected override DirectionEnum GetDirection(FoodMatrix eatMatrix, Membrane[,] creatures, Point position, Random random)
        {
            var points = DirectionEx.GetPoints(position.X, position.Y);
            var directions = new List<DirectionEnum>();
            var directionsWithFood = new List<DirectionEnum>();
            foreach (var item in points)
            {
                if (!DirectionEx.IsValidAndFree(item, creatures)) continue;
                directions.Add(DirectionEx.DirectionByPoints(position, item));
                if(eatMatrix.GetLevelOfFood(item) >= CreatureConstants.OneBite)
                    directionsWithFood.Add(DirectionEx.DirectionByPoints(position, item));
            }
            if (directions.Count == 0) return DirectionEnum.Stay;
            return directionsWithFood.Count == 0 ? directions.ElementAt(random.Next(directions.Count)) : directionsWithFood.ElementAt(random.Next(directionsWithFood.Count));
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
