using System;
using System.Drawing;
using CellsAutomate.Food;

namespace CellsAutomate.Creatures
{
    public abstract class BaseCreature
    {
        protected abstract DirectionEnum GetDirection(FoodMatrix eatMatrix, Membrane[,] creatures, Point position, Random random);

        protected abstract ActionEnum GetAction(Random random, bool hasOneBite, int energyPoints);

        public Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, Membrane[,] creatures, Point position, 
            Random random, bool hasOneBite, int energyPoints)
        {
            var action = GetAction(random, hasOneBite, energyPoints);
            var direction = (action == ActionEnum.Eat || action == ActionEnum.MakeChild) 
                ? DirectionEnum.Stay : GetDirection(eatMatrix, creatures, position, random);
            return Tuple.Create(action, direction);
        }
    }
}
