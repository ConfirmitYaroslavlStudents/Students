using System;

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
                        Console.WriteLine("Enter search term:");
                        var term = Console.ReadLine();

                        var searching = guitars.Search(term);
                        foreach (var item in searching)
                            Console.WriteLine(item.ID);

                        break;
                    case "a":
                        Console.Write("Enter guitar spec (ID Price Model Builder Type):");
                        var line = Console.ReadLine();
                        guitars.Add(line);
                        break;
                    case "r":
                        Console.WriteLine("Enter remove term:");
                        var removeterm = Console.ReadLine();
                        guitars.Remove(removeterm);
                        break;
                }
            }
        }
    }


}