using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public abstract class BaseCommandDefinerDecorator : IDefiner
    {
        protected IDefiner Definer;

        public BaseCommandDefinerDecorator(IDefiner definer)
        {
            Definer = definer;
        }

        public abstract ICommand TryGetCommand(string[] partsOfCommand);
    }
}