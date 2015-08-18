using System;
using CommandCreation;

namespace Mp3Tager
{
    // TODO: Task: plan of operations

    // TODO: switch count? visitor
    class Program
    {
        static void Main(string[] args)
        {
            IWriter writer = new ConsoleWriter();

            try
            {
                var app = new Application();
                app.Execute(args, writer);
                writer.WriteLine("Command successfully executed.");
            }
            catch (Exception e)
            {
                writer.WriteLine(e.Message);
            }
        }
    }
}
