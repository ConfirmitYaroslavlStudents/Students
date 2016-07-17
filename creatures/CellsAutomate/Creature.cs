using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate
{
    public class Creature
    {
        private readonly Random _random;
        private Point _position;
        private readonly Executor _executor;
        private readonly ICommand[] _commands;

        public static int FoodLevel = 10;  
        private int _minToSurvive = 3;  
        private int _childPrice = 6;  
        private int _oneBite = 3; 
        private int _maxTurns = 20;

        private int _turns;
        private int _foodSupply = 5; //Choose

        public int Generation { get; }

        public Creature(Point position, Executor executor, ICommand[] commands, Random random, int generation)
        {
            _position = position;
            _executor = executor;
            _commands = commands;
            _random = random;
            Generation = generation;
        }

        public ActionEnum MyTurn(FoodMatrix eatMatrix, Creature[,] cellsMatrix)
        {
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

        public Creature MakeChild(Point position)
        {
            _foodSupply -= _childPrice;
            return new Creature(position, _executor, _commands, _random, Generation + 1);
        }

        internal void SetPosition(Point newPosition)
        {
            _position = newPosition;
        }

        public int GetDirectionForLanguage(Point currentPoint, FoodMatrix eatMatrix)
        {
            var state =
                    DirectionEx
                        .GetPoints(currentPoint.X, currentPoint.Y)
                        .ToDictionary(x => DirectionEx.DirectionByPointForLanguage(currentPoint, x), x => (DirectionEx.IsValid(x, eatMatrix.Length, eatMatrix.Width) && eatMatrix.HasFood(x)) ? 4 : 0);

            var result = _executor.Execute(_commands, new MyExecutorToolset(_random, state));
            return int.Parse(result);
        }
    }
}
