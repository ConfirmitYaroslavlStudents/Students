using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoProcessor
{
    public class ConsoleWriteStep:Step
    {
        private string _message;

        public ConsoleWriteStep(string message)
        {
            _message = message;
        }

        public override bool Start()
        {
            Console.WriteLine(_message);
            return true;
        }
    }
}
