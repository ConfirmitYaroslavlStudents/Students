using System.Collections.Generic;
using Creatures.Language.Commands.Interfaces;

namespace CellsAutomate.Mutator.CommandsList
{
    public interface ICommandsList : IEnumerable<ICommand>
    {
        int Count { get; }
        void Insert(int index, ICommand item);
        void RemoveAt(int index);
        ICommand this[int index] { get; set; }
    }
}