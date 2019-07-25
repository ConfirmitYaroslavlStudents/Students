using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Shop
    {
        List<Guitar> allInstruments;
        
        public List<Guitar> Search(string searchCondition)
        {
            var suitableGuitars = new List<Guitar>();

            foreach (Guitar guitar in allInstruments)
            {
                if (guitar.Contains(searchCondition))
                {
                    suitableGuitars.Add(guitar);
                }
            }
            return suitableGuitars;
        }

        public void AddGuitarsFromFile()
        {
            var lines = File.ReadAllLines("guitars.txt");

            for(int i=1; i<lines.length;i++)
            {
                allInstruments.Add(MakeGuitar(lines[i]));
            }
        }

        public void Add(Guitar guitar)
        {
            allInstruments.Add(guitar);
        }

        private Guitar MakeGuitar(string line)
        {
            var parts = line.Split(' ');
            return new Guitar(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }

       public void DeleteGuitar(string ID)
        {
            foreach( Guitar guitar in allInstruments)
            {
                if (guitar.ID == ID)
                {
                    allInstruments.Remove(guitar);
                }
            }
        }

    }
}
