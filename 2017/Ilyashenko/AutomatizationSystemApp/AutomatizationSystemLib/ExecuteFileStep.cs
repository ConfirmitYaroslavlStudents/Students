namespace AutomatizationSystemLib
{
    public class ExecuteFileStep : IStep
    {
        private string _fileName;
        private bool _isIndependent;

        public ExecuteFileStep(string fileName, bool isIndependent)
        {
            _fileName = fileName;
            _isIndependent = isIndependent;
        }

        public void Execute(bool previousStepsExecutedCorrectly)
        {
            if (_isIndependent || (!_isIndependent && previousStepsExecutedCorrectly))
                System.Diagnostics.Process.Start("cmd.exe", "/C " + _fileName);
        }
    }
}
