using System;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Executors;

namespace CellsAutomate.Mutator
{
	public class Mutator
	{      
        private Random _random;

        public Mutator(Random random)
		{
			_random = random;
		}

		public ICommand[] Mutate(ICommand[] commands,out bool isAlive)
		{
			IMutation mutation = GetRandomMutation();
		    var newCommands = mutation.Transform(commands);
		    isAlive = AssertValid(newCommands);
		    return newCommands;
		}

	    private bool AssertValid(ICommand[] commads)
	    {
	        return new ValidationExecutor().Execute(commads, new ExecutorToolset(_random));
	    }

		private IMutation GetRandomMutation()
		{
		    var mutations = new IMutation[]
		    {
		        new AddCommandMutation(_random), new DeleteCommandMutation(_random),
		        new ReplaceCommandMutation(_random), new DublicateCommandMutatiton(_random)
		    };

		    return mutations[_random.Next(mutations.Length)];
		}
	}
}
