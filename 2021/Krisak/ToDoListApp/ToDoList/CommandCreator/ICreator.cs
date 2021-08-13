using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public interface ICreator
    {
        ICommand TryGetCommand(string[] partsOfCommand);
    }
}