using System.Collections.Generic;
using System.Linq;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.Mutations.InternalClasses
{
    internal class Swapper
    {
        private ICommand[] _commands;
        private bool _firstSwapMark;
        private bool _secondSwapMark;
        public Swapper(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToArray();
        }

        public ICommand[] SwapCommand(int firstSwapIndex, int secondSwapIndex)
        {
            Swap(firstSwapIndex, secondSwapIndex);
            var newCommands = new List<ICommand>();
            for (int i = 0; i < _commands.Length; i++)
            {
               if (_firstSwapMark == true && _secondSwapMark == true)
               { 
                   if (i == firstSwapIndex)
                   {
                       newCommands.Add(_commands[secondSwapIndex]);
                   }
                   else
                       if (i == secondSwapIndex)
                       {
                           newCommands.Add(_commands[firstSwapIndex]);
                       }
                       else
                           newCommands.Add(_commands[i]);
               }
               else
                   newCommands.Add(_commands[i]);
            }
            return newCommands.ToArray();
        }

        private void Swap(int firstSwapIndex, int secondSwapIndex)
        {
            _firstSwapMark = true;
            _secondSwapMark = true;
            AssertValid(firstSwapIndex, secondSwapIndex);
        }

        private void AssertValid(int firstSwapIndex, int secondSwapIndex)
        {
            var firstComToSwap = _commands[firstSwapIndex];
            var secondComToSwap = _commands[secondSwapIndex];
            //TODO:
        }

        private void AssertDeclarationCommand(int firstSwapIndex, int secondSwapIndex)
        {
            //TODO:
        }

        private void AssertCommandSetter(int firstSwapIndex, int secondSwapIndex)
        {
            //TODO:
        }

        private void AssertCommandWithArgument(int firstSwapIndex, int secondSwapIndex)
        {
            //TODO:
        }

        private void AssertCommandWithConstruction(int firstSwapIndex, int secondSwapIndex)
        {
            //TODO:
        }
    }
}
