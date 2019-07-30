using System;
using System.Collections.Generic;
using System.IO;

namespace GuitarShopLogic
{
    public class DataBase
    {
        private List<Product> items = new List<Product>();
        private string fileName = "guitars.txt";

        public DataBase(string fileName)
        {
            try
            {
                GetDataFromFile(fileName);
                this.fileName = fileName;
            }
            catch(FileNotFoundException)
            {
                throw new ArgumentException($"File with given {nameof(fileName)} can not be found");
            }
            
        }

        public void Add(string line)
        {
            var creator = new GuitarCreator();

            items.Add(creator.CreateProduct(line));
            File.AppendAllText(fileName, Environment.NewLine + line);
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

        private void GetDataFromFile(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var creator = new GuitarCreator();

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if(!string.IsNullOrWhiteSpace(line))
                {
                    items.Add(creator.CreateProduct(lines[i]));
                }
            }
        }
    }
}
