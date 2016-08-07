using System;
using System.Drawing;
using CellsAutomate.Food;

namespace CellsAutomate.Creatures
{
    public abstract class BaseCreature
    {
        protected abstract DirectionEnum GetDirection(FoodMatrix eatMatrix, Membrane[,] creatures, Point position, Random random);

        protected abstract ActionEnum GetAction(Random random, bool canMakeChild, bool hasCriticalLevelOfFood,
            bool hasOneBite);

        public Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, Membrane[,] creatures, Point position, 
            Random random, bool canMakeChild, bool hasToEat, bool hasOneBite)
        {
            var action = GetAction(random, canMakeChild, hasToEat, hasOneBite);
            var direction = (action == ActionEnum.Eat || action == ActionEnum.MakeChild) 
                ? DirectionEnum.Stay : GetDirection(eatMatrix, creatures, position, random);
            return Tuple.Create(action, direction);
        }
    }
}
