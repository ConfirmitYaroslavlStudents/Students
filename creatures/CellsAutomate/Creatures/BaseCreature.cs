using System;
using System.Drawing;
using CellsAutomate.Constants;
using CellsAutomate.Food;

namespace CellsAutomate.Creatures
{
    public abstract class BaseCreature
    {
        protected static Random Random;
        protected Point Position;
        protected int Generation;
        protected int EnergyPoints = CreatureConstants.StartEnergyPoints;

        public bool CanMakeChild => EnergyPoints >= CreatureConstants.ChildPrice + CreatureConstants.MinFoodToSurvive;
        public bool HasMinToSurvive => EnergyPoints >= CreatureConstants.MinFoodToSurvive;

        public int GetGeneration => Generation;
        public Point GetPosition => Position;

        public abstract BaseCreature MakeChild(Point position);
        protected abstract DirectionEnum GetDirection(FoodMatrix eatMatrix, BaseCreature[,] creatures);

        public void SetPosition(Point newPosition)
        {
            Position = newPosition;
        }

        public void EatFood()
        {
            EnergyPoints += CreatureConstants.OneBite;
        }

        public Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, BaseCreature[,] creatures)
        {
            EnergyPoints -= CreatureConstants.MinFoodToSurvive;

            return Tuple.Create(GetAction(eatMatrix), GetDirection(eatMatrix, creatures));
        }

        private ActionEnum GetAction(FoodMatrix eatMatrix)
        {
            if (EnergyPoints < CreatureConstants.CriticalLevelOfFood)
                return eatMatrix.GetLevelOfFood(Position) >= CreatureConstants.OneBite ? ActionEnum.Eat : ActionEnum.Go;
            
            if (CanMakeChild)
                return ActionEnum.MakeChild;

            return Random.Next(100) % 2 == 1 ? ActionEnum.Eat : ActionEnum.Go;
        }
    }
}
