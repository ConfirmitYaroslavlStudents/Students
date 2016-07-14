using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations
{
	public interface IMutation
	{
		ICommand[] Transform(ICommand[] commands);
	}
}