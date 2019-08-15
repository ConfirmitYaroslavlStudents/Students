using System.Collections.Generic;

namespace ShopLib
{
    public class AddFromFile : ILoader
    {
        public GuitarMaker Maker { get => new GuitarMaker(); set => new GuitarMaker(); }

        public List<Guitar> Add(string fileName)
        {
            var newGuitars = new List<Guitar>();
            var lines = new FileWorker().ReadFile(fileName);

            for (int i = 1; i < lines.Length; i++)
            {
                newGuitars.Add(Maker.MakeGuitar(lines[i]));
            }

            return newGuitars;
        }
    }
}
