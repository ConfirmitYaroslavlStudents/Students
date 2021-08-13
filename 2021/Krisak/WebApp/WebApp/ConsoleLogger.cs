using System;

namespace WebApp
{
    public class ConsoleLogger
    {
        public void LogGet(int length)
        {
            Console.WriteLine($"GET >> {length}");
        }

        public void LogPost(string message)
        {
            Console.WriteLine($"POST >> {message} >> {message.Length}");
        }
    }
}