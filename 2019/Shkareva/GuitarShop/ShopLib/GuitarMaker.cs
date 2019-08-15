using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLib
{
    public class GuitarMaker
    {
        public Guitar MakeGuitar(string line)
        {
            var parts = line.Split(' ');
            return new Guitar(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }
    }
}
