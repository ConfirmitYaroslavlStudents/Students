using System;
using System.Collections.Generic;

// 1. User reflecion for search \ Use dictionary
// 2. Big nesting
// 3. Guitar constructor has huge param list
// 4. SRP heavily violated

// 5. case sensetive

namespace GuitarShop
{
    class Program
    {
        static void Main()
        {
            Shop.AddFromFile("guitars.txt");

            while (true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();

                switch (command)
                {
                    case "s":
                        Console.WriteLine("Enter search term:");
                        var term = Console.ReadLine();
                        var searchList = Shop.Search(term);
                        Print(searchList);
                        break;
                    case "a":
                        Console.Write("Enter guitar spec (ID Price Model Builder Type):");
                        var guitarsData = Console.ReadLine();
                        Shop.AddFromConsole("guitars.txt", guitarsData);
                        break;
                }
            }
        }

        static void Print(List<string> list)
        {
            foreach (string element in list)
            {
                Console.WriteLine(element);
            }
        }
    }
}
