namespace CommandCreation
{
    public abstract class Command
    {
        public abstract string Execute();

        public abstract void Complete();
    }
}
