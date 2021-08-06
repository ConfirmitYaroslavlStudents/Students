namespace ToDoLibrary.CommandHandler
{
    public interface IHandlerCommand
    {
        public void Handling(string[] partsOfCommands);
    }
}