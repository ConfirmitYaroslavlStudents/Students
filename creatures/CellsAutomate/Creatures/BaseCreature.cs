using System;
using System.Drawing;
using CellsAutomate.Food;

namespace CellsAutomate.Creatures
{
    public abstract class BaseCreature
    {
        protected abstract DirectionEnum GetDirection(FoodMatrix eatMatrix, Membrane[,] creatures, Point position,
            Random random);

        protected abstract ActionEnum GetAction(FoodMatrix eatMatrix, int energyPoints, Point position, Random random,
            bool canMakeChild);

        public Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, Membrane[,] creatures, int energyPoints, 
            Point position, Random random, bool canMakeChild)
        {
            var action = GetAction(eatMatrix, energyPoints, position, random, canMakeChild);
            var direction = (action == ActionEnum.Eat || action == ActionEnum.MakeChild) 
                ? DirectionEnum.Stay : GetDirection(eatMatrix, creatures, position, random);
            return Tuple.Create(action, direction);
        }
    }
}
