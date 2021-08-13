using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public abstract class AbstractCommandCreatorDecorator : ICreator
    {
        protected ICreator Creator;
        protected AbstractCommandCreatorDecorator(ICreator creator) => Creator = creator;
        
        public virtual ICommand TryGetCommand(string[] partsOfCommand)
        {
            return Creator?.TryGetCommand(partsOfCommand);
        }
    }
}