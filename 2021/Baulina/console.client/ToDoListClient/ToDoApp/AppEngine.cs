using System.Threading.Tasks;

namespace ToDoApp
{
    abstract class AppEngine
    {
        public abstract AppController ToDoAppController { get; }
        public abstract Task Run();
    }
}
