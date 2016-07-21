using System;
using CellsAutomate.Mutator.Mutations.InternalClasses;
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
	}
}