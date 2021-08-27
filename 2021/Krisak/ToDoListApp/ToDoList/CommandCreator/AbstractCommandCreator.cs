using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandCreator
{
    public abstract class AbstractCommandCreator : ICreator
    {
        private  ICreator _nextCreator;
        public ICreator SetNext(ICreator creator)
        {
            _nextCreator = creator;
            return creator;
        }

        public virtual ICommand GetCommand() 
            => _nextCreator?.GetCommand();
    }
}