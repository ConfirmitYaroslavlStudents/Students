using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            while (true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "s":
                        shop.Search(Console.ReadLine());
                        break;
                    case "a":
                        shop.AddFromConsole();
                        break;
                }
            }
        }
    }
}
