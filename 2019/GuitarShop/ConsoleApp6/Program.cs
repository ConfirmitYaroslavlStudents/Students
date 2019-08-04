using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp6
{
    class Program
    {
        static void Main()
        {
            List<Item> item = ReadData();

            while (true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "search":
                        Search(item);
                        break;
                    case "add":
                        Add(item);
                        break;
                    case "exit":
                        return;
                }
            }
        }

        private static void Add(List<Item> item)
        {
            Console.Write("Enter item specification (ItemType, ID, Price, Model, Builder, GuitarType ):");
            var line = Console.ReadLine();
            item.Add(new Item(line));
            File.AppendAllText("item.txt", Environment.NewLine + line);
        }

        private static void Search(List<Item> item)
        {
            Console.WriteLine("Enter search term:");

            var term = Console.ReadLine();

            foreach (var guitar in item)
            {
                if (guitar.Contains(term))
                    Console.WriteLine("Mathc found: {0}", guitar.ID);
            }
        }

        private static List<Item> ReadData()
        {
            var lines = File.ReadAllLines("guitars.txt");
            var item = new List<Item>();

            foreach (var line in lines)
            {
                item.Add(new Item(line));
            }

            return item;
        }
    }

    public class Item
    {
        public Item(string line)
        {
            var parts = line.Split(' ');
            ID = parts[0];
            Price = parts[1];
            Model = parts[2];
            Builder = parts[3];
            GuitarType = parts[4];
            ItemType = parts[5];

        }

        public string ID { get; set; }
        public string Price { get; set; }
        public string Model{ get; set; }
        public string Builder { get; set; }
        public string GuitarType{ get; set; }
        public string ItemType { get; set; }

        public bool Contains(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || GuitarType.Contains(term);
        }
    }
}