using System;
using System.Drawing;
using CellsAutomate.Constants;
using CellsAutomate.Food;

namespace CellsAutomate.Creatures
{
    public abstract class BaseCreature
    {
        protected Point Position;

        protected int Generation;

        public int FoodSupply = CreatureConstants.EnergyPoints;

        public bool CanMakeChild => FoodSupply >= CreatureConstants.ChildPrice + CreatureConstants.MinFoodToSurvive;

        public abstract Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, BaseCreature[,] creatures);

        public abstract BaseCreature MakeChild(Point position);

        public void SetPosition(Point newPosition)
        {
            Position = newPosition;
        }

        public Point GetPosition()
        {
            return Position;
        }

        public int GetGeneration()
        {
            return Generation;
        }

        public void EatFood()
        {
            FoodSupply += CreatureConstants.OneBite;
        }
    }
}
