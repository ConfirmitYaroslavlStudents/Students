namespace CommandCreation
{
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void Undo();

        public abstract T Accept<T>(ICommandVisitor<T> visitor);

        public abstract bool IsPlanningCommand();
    }
}
