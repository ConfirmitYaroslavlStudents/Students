using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public interface IHandlerResponsibility
    {
        public IHandlerResponsibility SetNext(IHandlerResponsibility handlerResponsibility);
        public object HandlerResponsibility(object @object);
    }
}