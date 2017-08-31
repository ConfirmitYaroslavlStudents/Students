using System;

namespace AutoProcessor
{
    public class ConsoleWriteStep: IStep
    {
        private string _message;
        
        public ConsoleWriteStep(string message)
        {
            _message = message;
        }


        public void Start()
        {
            Console.Write(_message);
        }
    }
}
