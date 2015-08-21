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
            IWorker worker = new ConsoleWorker();

            try
            {
                var app = new Application();
                app.Execute(args, worker);               
            }
            catch (Exception e)
            {
                worker.WriteLine(e.Message);
            }
        }
    }
}
