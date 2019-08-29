using System;

namespace Logging
{
    public class ConsoleTarget : ITarget
    {
        public ConsoleTarget(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void Write(string messsage)
        {
            var output = Console.Out;

            output.WriteLine(messsage);
        }
    }
}
