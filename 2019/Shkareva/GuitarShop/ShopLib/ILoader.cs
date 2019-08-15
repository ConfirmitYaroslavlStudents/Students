using System.Collections.Generic;

namespace ShopLib
{
    internal interface ILoader
    {
        List<Guitar> Add(string data);
        GuitarMaker Maker { get; set; }
    }
}