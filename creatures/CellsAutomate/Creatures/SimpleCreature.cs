using System;
using System.Collections.Generic;
using System.Drawing;
using CellsAutomate.Constants;

namespace CellsAutomate
{
    public class SimpleCreature : BaseCreature
    {
        private readonly Random _random;

        public SimpleCreature(Point position, Random random, int generation)
        {
            Position = position;
            _random = random;
            Generation = generation;
        }

        public override Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, BaseCreature[,] creatures)
        {
            if (FoodSupply < CreatureConstants.MinFoodToSurvive)
                return Tuple.Create(ActionEnum.Die, DirectionEnum.Stay);

            FoodSupply -= CreatureConstants.MinFoodToSurvive;

            return Tuple.Create(GetAction(eatMatrix), GetDirection(eatMatrix, creatures));
        }

        private DirectionEnum GetDirection(FoodMatrix eatMatrix, BaseCreature[,] creatures)
        {
            var points = DirectionEx.GetPoints(Position.X, Position.Y);
            var directions = new List<DirectionEnum>();
            foreach (var item in points)
            {
                if (DirectionEx.IsValid(item, eatMatrix.Length, eatMatrix.Height) && eatMatrix.HasFood(item) &&
                    DirectionEx.IsFree(item, creatures))
                {
                    directions.Add(DirectionEx.DirectionByPoints(Position, item));
                }
            }

            if (directions.Count != 0)
            {
                var result = _random.Next(directions.Count) + 1;
                return DirectionEx.DirectionByNumber(result);
            }
            return DirectionEnum.Stay;
        }

        private ActionEnum GetAction(FoodMatrix eatMatrix)
        {
            if (FoodSupply < CreatureConstants.CriticalLevelOfFood && eatMatrix.HasFood(Position))
                return ActionEnum.Eat;
            var result = _random.Next(3) + 1;
            switch (result)
            {
                case 1: return ActionEnum.MakeChild;
                case 2: return ActionEnum.Go;
                case 3: return ActionEnum.Eat;
                default: throw new Exception();
            }
        }

        public override BaseCreature MakeChild(Point position)
        {
            FoodSupply -= CreatureConstants.ChildPrice;
            return new SimpleCreature(position, new Random(), Generation + 1);
        }
    }
}
