namespace CommandCreation // todo : fix namespaces
{
    // todo : commands have to be isolated from each other
    // todo : sync command - 1. filename -> tags 2. tags -> filename or vice versa 
    // todo : return log messages (?)
    // todo : possibility to skip command on execution step

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