using System;
using System.Drawing;
using CellsAutomate.Creatures;
using CellsAutomate.Food;

namespace CellsAutomate
{
    public static class Membrane
    {
        public static Tuple<ActionEnum, DirectionEnum> Turn(FoodMatrix eatMatrix, 
            BaseCreature[,] creatures, Point position)
        {
            var currentCreature = creatures[position.X, position.Y];
            if (HasToDie(currentCreature))
                return Tuple.Create(ActionEnum.Die, DirectionEnum.Stay);
            return currentCreature.MyTurn(eatMatrix, creatures);
        }

        private static bool HasToDie(BaseCreature creature)
        {
            if (creature.HasMinToSurvive)
                return false;
            return true;
        }

        public static void Eat(FoodMatrix eatMatrix, BaseCreature creature)
        {
            if (eatMatrix.TakeFood(creature.GetPosition()))
            {
                creature.EatFood();
            }
        }
    }
}
