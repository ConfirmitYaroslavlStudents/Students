using System;
using System.Collections.Generic;
using Creatures.Language.Executors;

namespace CellsAutomate
{
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
}
