using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public abstract class AbstractHandlerResponsibility: IHandlerResponsibility
    {
        private IHandlerResponsibility _nextHandler;

        public IHandlerResponsibility SetNext(IHandlerResponsibility handlerResponsibility)
        {
            _nextHandler = handlerResponsibility;
            return handlerResponsibility;
        }

        public virtual object HandlerResponsibility(object @object)
        {
            return _nextHandler?.HandlerResponsibility(@object);
        }
    }
}