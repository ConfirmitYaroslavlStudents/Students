using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp6
{
    class Program
    {
        static void Main()
        {
            GuitarCollection market = new GuitarCollection("guitars.txt");
            while(true)
            {
                Console.Write("Enter command:");
                var command = Console.ReadLine();
                switch(command)
                {
                    case "s":
                        Console.WriteLine("Enter search term:");
                        var term = Console.ReadLine();
                        foreach(var item in market.Search(term))
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
                        if(flag)
                            Console.WriteLine("Guitar removed");
                        break;
                }
            }
        }
    }

    public class Guitar
    {
        public Guitar(string id , string name , string model , string builder , string type)
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

        public static Guitar CreateGuitarFromString(string guitar)
        {
            string[] fields = guitar.Split(new char[] { ',' } , StringSplitOptions.RemoveEmptyEntries);
            return new Guitar(fields[0] , fields[1] , fields[2] , fields[3] , fields[4]);
        }
    }

    public class GuitarCollection
    {
        FileInteracting file;
        List<Guitar> Collections;
        public GuitarCollection(string source)
        {
            file = new FileInteracting(source);
            Collections = file.Initialize();
        }

        public void Add(string guitar)
        {
            var current = CreateGuitarFromString(guitar);
            Collections.Add(current);
            file.AddToFile(current);
        }

        public List<string> Search(string term)
        {
            List<string> result = new List<string>();
            foreach(var guitar in Collections)
            {
                if(guitar.Contains(term))
                    result.Add(guitar.ID);
            }
            return result;
        }

        private Guitar CreateGuitarFromString(string guitar)
        {
            string[] fields = guitar.Split(new char[] { ',' } , StringSplitOptions.RemoveEmptyEntries);
            return new Guitar(fields[0] , fields[1] , fields[2] , fields[3] , fields[4]);
        }

        public bool Remove(string id)
        {
            int rmindex = -1;
            for(int i = 1; i < Collections.Count; i++)
            {
                if(id == Collections[i].ID)
                    rmindex = i;
            }
            if(rmindex == -1)
                return false;
            else
            {
                Collections.RemoveAt(rmindex);
                file.RemoveFromFile(rmindex);
                return true;
            }
        }
    }

    public class FileInteracting
    {
        public string Path { get; set; }

        public FileInteracting(string path)
        {
            Path = path;
        }

        public void AddToFile(Guitar guitar)
        {
            File.AppendAllText(Path , Environment.NewLine + guitar.ToString());
        }

        public void RemoveFromFile(int rmindex)
        {
            string[] rows = File.ReadAllLines(Path);
            StreamWriter sw = new StreamWriter(Path);
            for(int i = 0; i < rows.Length; i++)
                if(i != rmindex + 1)
                    sw.WriteLine(rows[i]);
            sw.Close();
        }

        public List<Guitar> Initialize()
        {
            List<Guitar> result = new List<Guitar>();
            var lines = File.ReadAllLines(Path);
            for(int i = 1; i < lines.Length; i++)
                result.Add(Guitar.CreateGuitarFromString(lines[i]));
            return result;
        }
    }
}