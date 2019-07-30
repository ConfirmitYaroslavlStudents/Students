using System;
using System.Collections.Generic;
using System.IO;

namespace GuitarShopV2
{
    public class Stock
    {
        private List<Instrument> items = new List<Instrument>();

        public void ReadData()
        {
            var lines = File.ReadAllLines("guitars.txt");

            foreach (var line in lines)
            {
                items.Add(MakeGuitar(line));
            }
        }
        private Guitar MakeGuitar(string line)
        {
            var parts = line.Split(' ');
            return new Guitar(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }
        public void Add(string propetry)
        {
            items.Add(MakeGuitar(propetry));
            File.AppendAllText("guitars.txt", Environment.NewLine + propetry);
        }

        public IEnumerable<Instrument> Search(string term)
        {
            return items.FindAll(x => x.Contains(term));
        }

        public bool Remove(string term)
        {
            var instrumentIndex = items.FindIndex(x => x.Contains(term));

            if (instrumentIndex != -1)
            {
                string[] lines = File.ReadAllLines("guitars.txt");

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i != instrumentIndex)
                    {
                        File.AppendAllText("guitars.txt", Environment.NewLine + lines[i]);
                    }
                }

                items.RemoveAt(instrumentIndex);

                return true;
            }

            return false;
        }
    }
}
