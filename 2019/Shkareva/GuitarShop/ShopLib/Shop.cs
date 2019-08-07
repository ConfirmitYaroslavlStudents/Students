using System;
using System.Collections.Generic;
using System.IO;

namespace ShopLib
{
    public static class Shop
    {
        static List<Guitar> guitars = new List<Guitar>();

        public static void AddFromConsole(string fileName, string guitarsData)
        {
            guitars.Add(MakeGuitar(guitarsData));
            File.AppendAllText(fileName, Environment.NewLine + guitarsData);
        }

        private static Guitar MakeGuitar(string line)
        {
            var parts = line.Split(' ');
            return new Guitar(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }

        public static List<string> Search(string term)
        {
            var IdSuitablGuitars = new List<string>();

            foreach (var guitar in guitars)
            {
                if (guitar.Contains(term))
                {
                    IdSuitablGuitars.Add(guitar.ID);
                }
            }

            return IdSuitablGuitars;
        }

        public static void AddFromFile(string fileName)
        {
            var lines = new FileWorker().ReadFile(fileName);

            for(int i=1;i<lines.Length;i++)
            {
                guitars.Add(MakeGuitar(lines[i]));
            }

        }
    }
}
