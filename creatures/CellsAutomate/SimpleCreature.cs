using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate
{
    public static class Stats
    {
        public static int Up;
        public static int Right;
        public static int Down;
        public static int Left;
    }

    //public enum Direction
    //{
    //    Up,
    //    Right,
    //    Down,
    //    Left
    //}

    public class MyExecutorToolset : IExecutorToolset
    {
        private readonly Random _random;
        private readonly IDictionary<int, int> _state;

        public MyExecutorToolset(Random random, IDictionary<int, int> state)
        {
            _random = random;
            _state = state;
        }

        public int GetState(int direction)
        {
            return _state[direction];
        }

        public int GetRandom(int maxValue)
        {
            var result = _random.Next(maxValue);
            return result + 1;
        }
    }

    //public enum CellState
    //{
    //    Nothing,
    //    Empty,
    //    Cell,
    //    Food
    //}

  

    //public interface ICellTool
    //{
    //    CellState GetState(Direction cellNumber);
    //}

    public class Creature
    {
        private readonly Random _random;
        private Point _position;
        private readonly Executor _executor;
        private readonly ICommand[] _commands;

        public static int N = 1;  // Food level
        private int K = 3;  // Minimum to survive
        private int C = 6;  // Child price
        private int M = 3; // Food for 1 time
        private int T = 20; //Maximum turns

        private int _turns;
        private int _store = 1;

        public int Generation { get; private set; }

        public Creature(Point position, Executor executor, ICommand[] commands, Random random, int generation)
        {
            _position = position;
            _executor = executor;
            _commands = commands;
            _random = random;
            Generation = generation;
        }

        public Tuple<bool, ActionEnum> MyTurn(int[,] eatMatrix, Creature[,] cellsMatrix)
        {
            if (_store < K || _turns >= T)
                return Tuple.Create(false, ActionEnum.Die);

            _store -= K;
            _turns++;

            if (_store >= C)
            {
                var directions = new List<ActionEnum>();
                var points = ActionEx.GetPoints(_position.X, _position.Y);
                foreach (var item in points)
                {
                    if (isValid(item, eatMatrix) && cellsMatrix[item.X, item.Y] == null)
                    {
                        directions.Add(ActionEx.ActionByPoint(_position, item));
                    }
                }

                if (directions.Count != 0)
                {
                    _store -= C;
                    return Tuple.Create(true, directions.ElementAt(0));
                }
            }

            var state =
                ActionEx
                    .GetPoints(_position.X, _position.Y)
                    .ToDictionary(x => ActionEx.DirectionByPoint(_position, x), x => (isValid(x, eatMatrix) && eatMatrix[x.X, x.Y] != 0) ? 4 : 0);

            var result = _executor.Execute(_commands, new MyExecutorToolset(_random, state));
            var parsedResult = int.Parse(result);


            ActionEnum action;
            switch (parsedResult)
            {
                case 0:
                    action = ActionEnum.Stay;
                    if (eatMatrix[_position.X, _position.Y] >= M)
                    {
                        _store += M;
                        eatMatrix[_position.X, _position.Y] -= M;
                    }
                    break;
                case 1: action = ActionEnum.Up; Stats.Up++; break;
                case 2: action = ActionEnum.Right; Stats.Right++; break;
                case 3: action = ActionEnum.Down; Stats.Down++; break;
                case 4: action = ActionEnum.Left; Stats.Left++; break;
                default: throw new Exception();
            }

            return Tuple.Create(false, action);
        }

        private bool isValid(Point x, int[,] eatMatrix)
        {
            if (x.X < 0) return false;
            if (x.Y < 0) return false;
            if (x.X >= eatMatrix.GetLength(0)) return false;
            if (x.Y >= eatMatrix.GetLength(1)) return false;

            return true;
        }

        public Creature MakeChild(Point position, int generetion)
        {
            return new Creature(position, _executor, _commands, _random, generetion + 1);
        }

        internal void SetPosition(Point newPosition)
        {
            _position = newPosition;
        }
    }




    public class SimpleCreature
    {
        private int _i;
        private int _j;
        private readonly Random _random;
        private int _store = 1;
        private int _turnsOnPlace;
        public int Generation { get; private set; }

        public Random RandomGenerator { get { return _random; } }

        public SimpleCreature(int i, int j, int generation, Random random)
        {
            _i = i;
            _j = j;
            _random = random;
            Generation = generation;
        }

        public Tuple<bool, ActionEnum> MyTurn(bool[,] eatMatrix)
        {
            if (_store == 0)
                return Tuple.Create(false, ActionEnum.Die);

            _store--;

            if ((_turnsOnPlace < 4 && _store < 1) || (_turnsOnPlace < 4 && _random.Next(3) != 1))
            {
                _store += 3;
                _turnsOnPlace++;
                return Tuple.Create(false, ActionEnum.Stay);
            }
            else
            {
                var directions2 = ActionEx
                        .GetPoints(_i, _j)
                        .Where(x => IsValid(eatMatrix, x))
                        .Where(x => IsFree(eatMatrix, x))
                        .ToArray();

                if (_store > 3 && _random.Next(2) == 1 && directions2.Length != 0)
                {
                    _store -= 3;
                    _turnsOnPlace++;

                    return Tuple.Create(true, ActionEx.ActionByPoint(new Point(_i, _j), directions2[_random.Next(directions2.Length)]));
                }
                else
                {
                    _turnsOnPlace = 0;
                }
            }


            var directions = ActionEx.GetPoints(_i, _j).Where(x => IsValid(eatMatrix, x)).Where(x => IsFree(eatMatrix, x)).ToArray();

            if (!directions.Any()) return Tuple.Create(false, ActionEnum.Die);

            return Tuple.Create(false, ActionEx.ActionByPoint(new Point(_i, _j), directions[_random.Next(directions.Length)]));
        }

        private bool IsFree(bool[,] eatMatrix, Point point)
        {
            return eatMatrix[point.X, point.Y];
        }

        private bool IsValid(bool[,] matrix, Point point)
        {
            if (point.X < 0 || matrix.GetLength(1) <= point.X) return false;
            if (point.Y < 0 || matrix.GetLength(0) <= point.Y) return false;

            return true;
        }

        public void SetPosition(Point pointByAction)
        {
            _i = pointByAction.X;
            _j = pointByAction.Y;
        }
    }
}
