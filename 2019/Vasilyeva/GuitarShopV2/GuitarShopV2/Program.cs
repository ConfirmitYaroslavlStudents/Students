using System;
using System.Collections.Generic;
using System.IO;

namespace GuitarShopV2
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
            Stock guitars = new Stock();
            guitars.ReadData();

            while (true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "s":
                        guitars.Search();
                        break;
                    case "a":
                        guitars.Add();
                        break;
                }
            }
        }
    }


}