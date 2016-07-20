using System;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
	public class DeleteCommandMutation : IMutation
	{
	    private Random _rnd;

	    public DeleteCommandMutation()
	    {
	        _rnd=new Random();
	    }

	    public DeleteCommandMutation(Random rnd)
	    {
	        _rnd = rnd;
	    }
		public ICommand[] Transform(ICommand[] commands)
		{
		    var delIndex = _rnd.Next(commands.Length);
		    return new DeletterViaVisitor(commands).DeleteCommand(delIndex);
		}
	}
}