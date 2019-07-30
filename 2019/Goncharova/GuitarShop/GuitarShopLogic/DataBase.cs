using System;
using System.Collections.Generic;
using System.IO;

namespace GuitarShopLogic
{
    public class DataBase
    {
        private List<Product> items = new List<Product>();

        public DataBase()
        {
            var lines = File.ReadAllLines("guitars.txt");
            var creator = new GuitarCreator();

            foreach (var line in lines)
            {
                items.Add(creator.CreateProduct(line));
            }

        }

        public void Add(string line)
        {
            File.AppendAllText("guitars.txt", Environment.NewLine + line);

            var creator = new GuitarCreator();
            items.Add(creator.CreateProduct(line));
        }

        public List<string> Search(string term)
        {
            List<string> output = new List<string>();

            foreach (var item in items)
            {
                if (item.ContainsTerm(term))
                {
                    output.Add(item.ID);
                }
            }

            return output;
        }
    }
}
