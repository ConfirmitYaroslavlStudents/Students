using System;
using System.Threading;

namespace Cashe
{
    class Program
    {
        static void Main()
        {
            const int length = 20;
            var temp = new Cashe<int, string>();
            for (var i = 0; i < length; ++i)
            {
                temp.Add(i, i + " letter");
            }
            Console.WriteLine(temp.ContainsKey(2) ? "Cashe contains 2" : "Cashe don't contains 2");
            temp.Remove(2);
            Console.WriteLine(temp.ContainsKey(2) ? "Cashe contains 2" : "Cashe don't contains 2");
            
            Console.ReadKey();
        }
    }
}
