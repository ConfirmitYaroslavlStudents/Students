namespace ToDoLibrary.CommandHandler
{
    public interface IHandlerCommand
    {
        public ResultHandler CommandHandler(string[] partsOfCommands);
    }
}