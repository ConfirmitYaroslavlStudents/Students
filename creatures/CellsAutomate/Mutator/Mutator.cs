using System;
using CellsAutomate.Mutator.Mutations;
using CellsAutomate.Mutator.Mutations.Logging;
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

		public ICommand[] Mutate(ICommand[] commands)
		{
			IMutation mutation = GetRandomMutation();
		    var newCommands = mutation.Transform(commands);
		    while (!AssertValid(newCommands))
		    {
                newCommands = mutation.Transform(commands);
            }
		    return newCommands;
		}

	    public ICommand[] Mutate(ICommand[] commands, ILogger logger)
	    {
            IMutation mutation = GetRandomMutation();
            var newCommands = mutation.Transform(commands,logger);
            while (!AssertValid(newCommands))
            {
                logger.Write("Mutation failed\n");
                _random.Next();
                newCommands = mutation.Transform(commands,logger);
            }
            logger.Write("Mutated\n");
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
		        new ReplaceCommandMutation(_random), new DublicateCommandMutatiton(_random),
                new SwapCommandMutation(_random) 
		    };

		    return mutations[_random.Next(mutations.Length)];
		}
	}
}
