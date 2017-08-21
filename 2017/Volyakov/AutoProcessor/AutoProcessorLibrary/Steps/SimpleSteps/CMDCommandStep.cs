using System.Diagnostics;

namespace AutoProcessor
{
    public class CMDCommandStep : IStep
    {
        private ProcessStartInfo _startInfo;

        public CMDCommandStep(string command)
        {
            _startInfo = 
                    new ProcessStartInfo("cmd.exe")
                    {
                        Arguments = "/c " + command,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
        }

        public void Start()
        {
            var process =
                    new System.Diagnostics.Process
                    {
                        StartInfo = _startInfo
                    };

            process.Start();
        }
    }
}
