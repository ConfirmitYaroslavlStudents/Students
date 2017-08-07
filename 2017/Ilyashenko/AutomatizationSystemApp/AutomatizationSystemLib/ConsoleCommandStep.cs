namespace AutomatizationSystemLib
{
    public class ConsoleCommandStep : IStep
    {
        private string _command;
        private bool _isIndependent;

        public ConsoleCommandStep(string command, bool isIndependent = false)
        {
            _isIndependent = isIndependent;
        }

        public void Execute(bool previousStepsExecutedCorrectly)
        {
            if (_isIndependent || (!_isIndependent && previousStepsExecutedCorrectly))
                System.Diagnostics.Process.Start("cmd.exe", "/C " + _command);
        }
    }
}
