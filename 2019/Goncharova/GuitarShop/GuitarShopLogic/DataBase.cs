using System;
using System.Collections.Generic;
using System.IO;

namespace GuitarShopLogic
{
    public class DataBase
    {
        private List<Guitar> Guitars = new List<Guitar>();

        public DataBase()
        {
            var lines = File.ReadAllLines("guitars.txt");

            foreach (var line in lines)
            {
                Guitars.Add(new Guitar(line));
            }

        }

        public void Add(string line)
        {
            File.AppendAllText("guitars.txt", Environment.NewLine + line);
            Guitars.Add(new Guitar(line));
        }

        public List<string> Search(string term)
        {
            List<string> output = new List<string>();

            foreach (var guitar in Guitars)
            {
                if (guitar.Contains(term))
                {
                    output.Add(guitar.ID);
                }
            }

            return output;
        }
    }
}
