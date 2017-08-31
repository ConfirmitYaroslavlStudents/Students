namespace AutomatizationSystemLib
{
    public class ConsoleCommandStep : IStep
    {
        private ConsoleCommandOptions _options;

        public ConsoleCommandStep(ConsoleCommandOptions options)
        {
            _options = options;
        }

        public void Execute()
        {
            System.Diagnostics.Process.Start("cmd.exe", "/C " + _options.CommandName);
        }
    }
}
