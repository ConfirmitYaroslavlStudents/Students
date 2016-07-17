using System;
using System.Drawing;

namespace CellsAutomate
{
    public class SimpleCreature
    {
        private Point _position;
        private Random _random = new Random();

        public static int FoodLevel = 10;
        private int _minToSurvive = 3;
        private int _childPrice = 6;
        private int _oneBite = 3;
        private int _maxTurns = 20;

        private int _turns;
        private int _foodSupply = 7; //Choose
        public bool HadMoved { get; set; }

        public int Generation { get; }

        public SimpleCreature(Point position, int generation)
        {
            _position = position;
            Generation = generation;
            HadMoved = false;
        }

        public ActionEnum MyTurn(FoodMatrix eatMatrix, SimpleCreature[,] cellsMatrix)
        {
            HadMoved = true;
            if (_foodSupply < _minToSurvive || _turns >= _maxTurns)
                return ActionEnum.Die;

            _foodSupply -= _minToSurvive;
            _turns++;

            if (_foodSupply >= _childPrice)
            {
                return ActionEnum.MakeChild;
            }

            return ActionEnum.Go;
        }

        public void EatFood(FoodMatrix eatMatrix)
        {
            if (eatMatrix.TakeFood(_position, _oneBite))
            {
                _foodSupply += _oneBite;
            }
        }

        public SimpleCreature MakeChild(Point position)
        {
            _foodSupply -= _childPrice;
            return new SimpleCreature(position, Generation + 1);
        }

        internal void SetPosition(Point newPosition)
        {
            _position = newPosition;
        }

        public DirectionEnum GetDirectionOrEat(FoodMatrix eatMatrix)
        {
            var r = _random.Next(3) + 1;

            if (eatMatrix.HasFood(_position))
            {
                EatFood(eatMatrix);
                return DirectionEnum.Stay;
            }

            switch (r)
            {
                case 1:
                    Stats.Up++;
                    return DirectionEnum.Up;
                case 2:
                    Stats.Right++;
                    return DirectionEnum.Right;
                case 3:
                    Stats.Down++;
                    return DirectionEnum.Down;
                case 4:
                    Stats.Left++;
                    return DirectionEnum.Left;
                default:
                    throw new Exception();
            }
        }
    }
}
