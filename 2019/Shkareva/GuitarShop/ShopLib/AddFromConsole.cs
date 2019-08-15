using System.Collections.Generic;

namespace ShopLib
{
    public class AddFromConsole : ILoader
    {
        public GuitarMaker Maker { get => new GuitarMaker(); set => new GuitarMaker(); }

        public List<Guitar> Add(string data)
        {
            var newGuitars = new List<Guitar>();
            newGuitars.Add(Maker.MakeGuitar(data));
            return newGuitars;
        }
    }
}
