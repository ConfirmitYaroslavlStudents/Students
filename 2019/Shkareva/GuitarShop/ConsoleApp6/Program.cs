using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp6
{
    // 1. User reflecion for search \ Use dictionary
    // 2. Big nesting
    // 3. Guitar constructor has huge param list
    // 4. SRP heavily violated
    // 5. 
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "s":
                        Search(guitars);
                        break;
                    case "a":
                        Add(guitars);
                        break;
                }
            }
        }

        private static void Add(List<Guitar> guitars)
        {
            Console.Write("Enter guitar spec (ID Price Model Builder Type):");
            var line = Console.ReadLine();
            guitars.Add(MakeGuitar(line));
            File.AppendAllText("guitars.txt", Environment.NewLine + line);
        }
        
    }

    
}