using System;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands.Interfaces;

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
            return mutation.Transform(commands);
		}

		private IMutation GetRandomMutation()
		{
			int randomNumber = _random.Next(3);

			if (randomNumber == 0)
				return new AddCommandMutation();
			else if (randomNumber == 1)
				return new DeleteCommandMutation(_random);
			else
				return new SwapCommandMutation();
		}
	}
}
