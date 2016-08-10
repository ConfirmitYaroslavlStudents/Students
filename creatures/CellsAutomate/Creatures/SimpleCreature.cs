using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CellsAutomate.Food;
using CellsAutomate.Tools;

namespace CellsAutomate.Creatures
{
    public class SimpleCreature : BaseCreature
    {
        protected override DirectionEnum GetDirection(FoodMatrix eatMatrix, Membrane[,] creatures, Point position, Random random)
        {
            var points = CommonMethods.GetPoints(position);
            var directions = new List<DirectionEnum>();
            var directionsWithFood = new List<DirectionEnum>();
            foreach (var item in points)
            {
                if (!CommonMethods.IsValidAndFree(item, creatures)) continue;
                directions.Add(DirectionEx.DirectionByPoints(position, item));
                if(eatMatrix.HasOneBite(item))
                    directionsWithFood.Add(DirectionEx.DirectionByPoints(position, item));
            }
            if (directions.Count == 0) return DirectionEnum.Stay;
            return directionsWithFood.Count == 0 ? directions.ElementAt(random.Next(directions.Count)) : directionsWithFood.ElementAt(random.Next(directionsWithFood.Count));
        }

        protected override ActionEnum GetAction(Random random, bool canMakeChild, bool hasToEat, bool hasOneBite)
        {
            if (hasToEat)
                return hasOneBite ? ActionEnum.Eat : ActionEnum.Go;

            if (canMakeChild)
                return ActionEnum.MakeChild;

            return random.Next(2) == 1 ? ActionEnum.Eat : ActionEnum.Go;
        }
    }
}
