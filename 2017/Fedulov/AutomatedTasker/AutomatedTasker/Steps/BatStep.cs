using System.Diagnostics;

namespace AutomatedTasker.Steps
{
    public class BatStep : IStep
    {
        public string BatFile { set; get; }
        public string Arguments { set; get; }

        public BatStep(string batFile) : this(batFile, "")
        {
        }

        public BatStep(string batFile, string arguments)
        {
            BatFile = batFile;
            Arguments = arguments;
        }

        public void Execute()
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo(BatFile, Arguments)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
        }
    }
}