using System.Collections.Generic;

namespace ShopLib
{
    public static class Shop
    {
        static List<Guitar> _guitars = new List<Guitar>();
        static ILoader Loader = new AddFromFile();

        public static List<string> Search(string term)
        {
            var IdSuitablGuitars = new List<string>();

            foreach (var guitar in _guitars)
            {
                if (guitar.Contains(term))
                {
                    IdSuitablGuitars.Add(guitar.ID);
                }
            }

            return IdSuitablGuitars;
        }

        public static void AddSomeGuitars(string data)
        {
           _guitars.AddRange(Loader.Add(data));
        }        
    }
}
