using System;

namespace AutoProcessor
{
    public class ConsoleWriteStep: Step
    {
        private string _message;
        
        public ConsoleWriteStep(string message)
        {
            _message = message;

            StepStatus = Status.NotStarted;
        }


        public override void Start()
        {
            StepStatus = Status.Launched;

            try
            {
                Console.Write(_message);

                StepStatus = Status.Finished;
            }
            catch
            {
                StepStatus = Status.Error;
            }
        }
    }
}
