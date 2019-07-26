using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuitarShopV2
{
    public class Stock
    {
        private List<Instrument> item = new List<Instrument>();

        public void ReadData()
        {
            var lines = File.ReadAllLines("guitars.txt");
            var guitars = new List<Guitar>();

            foreach (var line in lines)
            {
                item.Add(MakeGuitar(line));
            }
        }
        private Guitar MakeGuitar(string line)
        {
            var parts = line.Split(' ');
            return new Guitar(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }
        public void Add()
        {
            Console.Write("Enter guitar spec (ID Price Model Builder Type):");
            var line = Console.ReadLine();
            item.Add(MakeGuitar(line));
            File.AppendAllText("guitars.txt", Environment.NewLine + line);
        }

        public void Search()
        {
            Console.WriteLine("Enter search term:");

            var term = Console.ReadLine();

            foreach (var guitar in item)
            {
                if (guitar.Contains(term))
                    Console.WriteLine("Mathc found: {0}", guitar.ID);
            }
        }

    }
}
