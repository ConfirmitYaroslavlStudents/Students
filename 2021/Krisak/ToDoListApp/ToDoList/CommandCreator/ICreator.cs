using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandCreator

{
    public interface ICreator
    {
        ICommand GetCommand();
        ICreator SetNext(ICreator creator);
    }
}