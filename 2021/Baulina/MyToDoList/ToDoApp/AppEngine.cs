namespace ToDoApp
{
    abstract class AppEngine
    {
        public abstract AppController ToDoAppController { get; }

        public abstract void Run();
    }
}
