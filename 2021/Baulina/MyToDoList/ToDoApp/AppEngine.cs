namespace ToDoApp
{
    abstract class AppEngine
    {
        public abstract ToDoAppMenu ToDoAppMenu { get; }

        public abstract void Run();
    }
}
