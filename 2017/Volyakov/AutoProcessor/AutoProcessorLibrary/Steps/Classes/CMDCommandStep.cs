using System.Diagnostics;

namespace AutoProcessor
{
    public class CMDCommandStep : Step
    {
        private ProcessStartInfo _startInfo;

        public CMDCommandStep(string command)
        {
            StepStatus = Status.NotStarted;

            _startInfo = 
                    new ProcessStartInfo("cmd.exe")
                    {
                        Arguments = "/c " + command,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
        }

        public override void Start()
        {
            StepStatus = Status.Launched;

            try
            {
                var process =
                    new System.Diagnostics.Process
                    {
                        StartInfo = _startInfo
                    };

                process.Start();

                StepStatus = Status.Finished;
            }
            catch
            {
                StepStatus = Status.Error;
            }
        }
    }
}
