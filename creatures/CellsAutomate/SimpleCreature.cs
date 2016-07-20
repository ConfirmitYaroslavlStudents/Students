using System.Drawing;

namespace CellsAutomate
{
    public class SimpleCreature
    {
        public Point Position { get; private set; }

        public static int FoodLevel = 16;
        private int _minToSurvive = 3;
        private int _childPrice = 6;
        private int _oneBite = 8;
        private int _maxTurns = 20;

        private int _turns;
        private int _foodSupply = 7; //Choose
        public bool HadMoved { get; set; }

        public int Generation { get; }

        public SimpleCreature(Point position, int generation)
        {
            Position = position;
            Generation = generation;
            HadMoved = false;
        }

        public ActionEnum MyTurn()
        {
            HadMoved = true;
            if (_foodSupply < _minToSurvive || _turns >= _maxTurns)
                return ActionEnum.Die;

            _foodSupply -= _minToSurvive;
            _turns++;

            if (_foodSupply >= _childPrice + _minToSurvive)
            {
                return ActionEnum.MakeChild;
            }

            return ActionEnum.Go;
        }

        public void EatFood(FoodMatrix eatMatrix)
        {
            if (eatMatrix.TakeFood(Position, _oneBite))
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
            Position = newPosition;
        }
    }
}
