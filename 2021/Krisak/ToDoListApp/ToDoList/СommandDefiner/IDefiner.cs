using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public interface IDefiner
    {
        ICommand TryGetCommand(string[] partsOfCommand);
    }
}