using System.Collections.Generic;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.NewMutations
{
    public interface ICommandsList : IEnumerable<ICommand>
    {
        int Count { get; }
        void Insert(int index, ICommand item);
        void RemoveAt(int index);
        bool AssertValid();
        ICommand this[int index] { get; set; }
    }
}