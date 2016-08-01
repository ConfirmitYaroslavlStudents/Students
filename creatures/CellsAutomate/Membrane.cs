using System;
using CellsAutomate.Creatures;
using CellsAutomate.Food;

namespace CellsAutomate
{
    public class Membrane
    {
        private readonly BaseCreature _creature;

        public Membrane(BaseCreature creature)
        {
            _creature = creature;
        }

        public Tuple<ActionEnum, DirectionEnum> Turn(FoodMatrix eatMatrix, 
            BaseCreature[,] creatures)
        {
            var currentCreature = creatures[_creature.GetPosition().X, _creature.GetPosition().Y];
            return HasToDie() ? Tuple.Create(ActionEnum.Die, DirectionEnum.Stay) : currentCreature.MyTurn(eatMatrix, creatures);
        }

        private bool HasToDie()
        {
            return !_creature.HasMinToSurvive;
        }

        public void Eat(FoodMatrix eatMatrix)
        {
            if (eatMatrix.TakeFood(_creature.GetPosition()))
            {
                _creature.EatFood();
            }
        }
    }
}
