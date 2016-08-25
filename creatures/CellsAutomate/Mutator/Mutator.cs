using System;
using System.Collections.Generic;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Executors;

namespace CellsAutomate.Mutator
{
    public class Mutator
    {
        private Random _random;
        private ICommandsList _commands;
        private const int NumOfAttempts = 5;
        private const double MutateProbability = 0.01;

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
            var mutationCount = GetNumberOfMutations();
            for (int i = 0; i < mutationCount; i++)
            {
                MutateSingle();
            }
        }

        public static int MUTATIONS = 0;
        public static int MUTATIONSREAL = 0;
        private void MutateSingle()
        {
            MUTATIONS++;
            var mutation = GetRandomMutation();
            for (int i = 0; i < NumOfAttempts; i++)
            {
                MUTATIONSREAL++;
                mutation.Transform();
                if (AssertValid()) break;
                mutation.Undo();
                MUTATIONSREAL--;
            }
        }

        private bool AssertValid()
        {
            var executor = new ValidationExecutor();
            return executor.Execute(_commands, new ExecutorToolset(new Random()));
        }

        private int GetNumberOfMutations()
        {
            var mutationCount = 0;
            for (int i = 0; i < _commands.Count; i++)
            {
                if (ShouldMutate()) mutationCount++;
            }
            return mutationCount;
        }

        private bool ShouldMutate()
        {
            return _random.NextDouble() < MutateProbability;
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