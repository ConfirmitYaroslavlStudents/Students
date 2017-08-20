namespace AutoProcessor
{
    public class Processor
    {
        public void StartProcess(Process currentProcess)
        {
            var steps = currentProcess.Steps;

            foreach (var currentStep in steps)
            {
                currentStep.Start();
            }
        }
    }
}
