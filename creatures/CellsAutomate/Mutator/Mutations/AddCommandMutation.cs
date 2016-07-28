using System;
using CellsAutomate.Mutator.Mutations.InternalClasses;
using CellsAutomate.Mutator.Mutations.Logging;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
	public class AddCommandMutation : IMutation
	{
	    private Random _rnd;

	    public AddCommandMutation(Random rnd)
	    {
	        _rnd = rnd;
	    }

        public ICommand[] Transform(ICommand[] commands)
		{
			return new Adder(commands,_rnd).AddCommand();
		}

	    public ICommand[] Transform(ICommand[] commands, ILogger logger)
	    {
	        return new Adder(commands, _rnd, logger).AddCommand();
	    }
	}
}