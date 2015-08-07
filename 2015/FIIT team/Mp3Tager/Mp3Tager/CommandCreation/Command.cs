namespace CommandCreation
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract int[] GetNumberOfArguments();
        public abstract string GetCommandName();

        protected void CheckIfCanBeExecuted(string[] args)
        {
            var parser = new ArgumentParser(args);
            parser.CheckForProperNumberOfArguments(GetNumberOfArguments());
            parser.CheckForTheRightCommandName(GetCommandName());            
        }
    }
}
