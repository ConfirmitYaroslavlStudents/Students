using System;
using System.Collections.Generic;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations;

namespace CellsAutomate.Mutator
{
    public class Mutator
    {
        private Random _random;
        private ICommandsList _commands;
        private const int NumOfAttempts = 5;

        private readonly List<IMutation> _mutations;

        public Mutator(Random random, ICommandsList commands)
        {
            _random = random;
            _commands = commands;
            _mutations = new List<IMutation>
            {
                 new AddCommandMutation(_random,_commands),
                 new DeleteCommandMutation(_random, _commands),
                 new DublicateCommandMutation(_random,_commands),
                 new ReplaceCommandMutation(_random,_commands),
                 new SwapCommandMutation(_random,_commands)
            };
        }

        public void Mutate()
        {
            var mutation = GetRandomMutation();
            for (int i = 0; i < NumOfAttempts; i++)
            {
                mutation.Transform();
                if (_commands.AssertValid()) break;
                mutation.Undo();
            }
        }

        private IMutation GetRandomMutation()
        {
            return ChooseRandom(_mutations);
        }

        private T ChooseRandom<T>(IReadOnlyList<T> array)
        {
            return array[_random.Next(array.Count)];
        }
    }
}