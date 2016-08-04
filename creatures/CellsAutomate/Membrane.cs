using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CellsAutomate.Constants;
using CellsAutomate.Creatures;
using CellsAutomate.Factory;
using CellsAutomate.Food;

namespace CellsAutomate
{
    public class Membrane
    {
        private readonly Random _random;
        public Point Position { get; set; }
        public int Generation { get; }
        private int _energyPoints = CreatureConstants.StartEnergyPoints;
        private readonly BaseCreature _creature;
        private readonly Creator _creator;

        public Membrane(BaseCreature creature, Random random, Point position, int generation, Creator creator)
        {
            _creature = creature;
            _random = random;
            Position = position;
            Generation = generation;
            _creator = creator;
        }

        public Tuple<ActionEnum, DirectionEnum> Turn(FoodMatrix eatMatrix, 
            Membrane[,] creatures)
        {
            if (HasToDie()) return Tuple.Create(ActionEnum.Die, DirectionEnum.Stay);

            _energyPoints -= CreatureConstants.MinFoodToSurvive;

            var result = _creature.MyTurn(eatMatrix, creatures, _energyPoints, Position,  _random, CanMakeChild());

            return result.Item1 == ActionEnum.MakeChild ? Tuple.Create(ActionEnum.MakeChild, GetDirectionForChild(creatures)) : result;
        }

        private bool CanMakeChild()
        {
            return _energyPoints >= CreatureConstants.ChildPrice + CreatureConstants.MinFoodToSurvive;
        }

        private bool HasToDie()
        {
            return _energyPoints < CreatureConstants.MinFoodToSurvive;
        }

        private DirectionEnum GetDirectionForChild(Membrane[,] creatures)
        {
            var points = DirectionEx.GetPoints(Position.X, Position.Y);
            var directions = (from item in points where DirectionEx.IsValidAndFree(item, creatures)
                              select DirectionEx.DirectionByPoints(Position, item)).ToList();
            return directions.Count == 0 ? DirectionEnum.Stay : directions.ElementAt(_random.Next(directions.Count));
        }

        public void Eat(FoodMatrix eatMatrix)
        {
            if (eatMatrix.TakeFood(Position))
            {
                _energyPoints += CreatureConstants.OneBite;
            }
        }

        public Membrane MakeChild(Point childPosition)
        {
            _energyPoints -= CreatureConstants.ChildPrice;
            return new Membrane(_creator.CreateAbstractCreature(), _random, childPosition, Generation + 1, _creator);
        }
    }
}
