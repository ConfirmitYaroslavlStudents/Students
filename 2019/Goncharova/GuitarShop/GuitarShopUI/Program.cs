using System;
using System.Collections.Generic;
using GuitarShopLogic;

namespace GuitarShopUI
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
            DataBase guitars = new DataBase();

            AskForInput(guitars);
        }

        private static void AskForInput(DataBase guitars)
        {
            bool stop = false;

            do
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "s":
                        Search(guitars);
                        break;
                    case "a":
                        Add(guitars);
                        break;
                    case "e":
                        stop = true;
                        break;
                }
            }
            while (!stop);
        }

        private static void Add(DataBase items)
        {
            Console.Write("Enter guitar spec (ID Price Model Builder Type):");
            var line = Console.ReadLine();

            items.Add(line);
        }

        private static void Search(DataBase items)
        {
            Console.WriteLine("Enter search term:");
            var term = Console.ReadLine();

            List<string> idMatched = items.Search(term);

            foreach (var id in idMatched)
            {
                Console.WriteLine("Mathc found: {0}", id);
            }

        }
    }

    
}
