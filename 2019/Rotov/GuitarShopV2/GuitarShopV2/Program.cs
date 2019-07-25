using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp6
{
    // 1. User reflecion for search \ Use dictionary
    // 2. Big nesting
    // 3. Guitar constructor has huge param list
    // 4. SRP heavily violated
    class Program
    {
        static void Main()
        {
            GuitarCollection market = new GuitarCollection();
            while (true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "s":
                        Console.WriteLine("Enter search term:");
                        var term = Console.ReadLine();
                        foreach (var item in market.Search(term))
                            Console.WriteLine(item);
                        break;
                    case "a":
                        Console.Write("Enter guitar spec (ID,Price,Model,Builder,Type):");
                        var line = Console.ReadLine();
                        market.Add(line);
                        break;
                    case "r":
                        Console.WriteLine("Ented ID");
                        var id = Console.ReadLine();
                        bool flag = market.Remove(id);
                        if (flag)
                            Console.WriteLine("Guitar removed");
                        break;
                }
            }
        }
    }

    class Guitar
    {
        public Guitar(string id, string name, string model, string builder, string type)
        {
            ID = id;
            Price = name;
            Model = model;
            Builder = builder;
            Type = type;
        }

        public string ID { get; set; }
        public string Price { get; set; }
        public string Model { get; set; }
        public string Builder { get; set; }
        public string Type { get; set; }

        public bool Contains(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }

        public override string ToString()
        {
            return string.Format($"{ID},{Price},{Model},{Builder},{Type}");
        }
    }

    public class GuitarCollection
    {
        string DataBase = "guitars.txt";
        List<Guitar> Collections;
        public GuitarCollection()
        {
            Collections = new List<Guitar>();
            ReadData();
        }

        public void Add(string guitar)
        {
            var current = CreateGuitarFromString(guitar);
            Collections.Add(current);
            File.AppendAllText(DataBase, Environment.NewLine + current.ToString());
        }

        public List<string> Search(string term)
        {
            List<string> result = new List<string>();
            foreach (var guitar in Collections)
            {
                if (guitar.Contains(term))
                    result.Add(guitar.ID);
            }
            return result;
        }

        private Guitar CreateGuitarFromString(string guitar)
        {
            string[] fields = guitar.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return new Guitar(fields[0], fields[1], fields[2], fields[3], fields[4]);
        }

        private void ReadData()
        {
            var lines = File.ReadAllLines(DataBase);
            for(int i = 1; i < lines.Length; i++)
            {
                Collections.Add(CreateGuitarFromString(lines[i]));
            }
        }

        public bool Remove(string id)
        {
            int rmindex = -1;
            for(int i = 1; i < Collections.Count; i++)
            {
                if (id == Collections[i].ID)
                    rmindex = i;
            }
            if (rmindex == -1)
                return false;
            else
            {
                Collections.RemoveAt(rmindex);
                string[] rows = File.ReadAllLines(DataBase);
                StreamWriter sw = new StreamWriter(DataBase);
                for (int i = 0; i < rows.Length; i++)
                    if (i != rmindex + 1)
                        sw.WriteLine(rows[i]);
                sw.Close();
                return true;
            }
        }
    }
}