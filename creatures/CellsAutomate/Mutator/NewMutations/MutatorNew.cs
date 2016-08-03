using System;
using System.Collections.Generic;

namespace CellsAutomate.Mutator.NewMutations
{
    public class MutatorNew
    {
        private Random _random;
        private ICommandsList _commands;
        private const int NumOfAttempts = 5;

        private readonly List<IMutationNew> _mutations;

        public MutatorNew(Random random, ICommandsList commands)
        {
            _random = random;
            _commands = commands;
            _mutations = new List<IMutationNew>
            {
                 new AddCommandMutationNew(_random,_commands),
                 new DeleteCommandMutationNew(_random, _commands),
                 new DublicateCommandMutationNew(_random,_commands),
                 new ReplaceCommandMutationNew(_random,_commands),
                 new SwapCommandMutationNew(_random,_commands)
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

        private IMutationNew GetRandomMutation()
        {
            return ChooseRandom(_mutations);
        }

        private T ChooseRandom<T>(IReadOnlyList<T> array)
        {
            return array[_random.Next(array.Count)];
        }
    }
}