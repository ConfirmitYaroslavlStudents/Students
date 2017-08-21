using System.Diagnostics;

namespace AutomatizationSystemLib
{
    public class BatFileStep : IStep
    {
        private BatFileOptions _options;

        public BatFileStep(BatFileOptions options)
        {
            _options = options;
        }

        public void Execute()
        {
            var processInfo = new ProcessStartInfo(_options.FileName, _options.Arguments)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = new Process { StartInfo = processInfo };
            process.Start();
        }
    }
}
