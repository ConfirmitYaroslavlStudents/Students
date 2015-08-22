namespace CommandCreation
{
    public abstract class Command
    {
        protected Command()
        {
            SetIfShouldBeCompleted();        
        }

        public bool ShouldBeCompleted { get; protected set; }

        public bool EnableBackup { get; set; }

        protected abstract void SetIfShouldBeCompleted();

        public abstract string Execute();

        public abstract void Complete();
    }
}
