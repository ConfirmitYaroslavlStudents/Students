using System;

namespace Mp3Tager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var app = new Application();
                app.Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
