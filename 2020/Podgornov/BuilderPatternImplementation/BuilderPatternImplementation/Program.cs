using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            var addressBuilder = new AddressBuilder().AddCity("Moscow");
            var addr1 = addressBuilder.AddBuilding(1).AddFlat(1).Build();
            var addr2 = addressBuilder.AddFlat(2).Build();
            Console.WriteLine(addr1 != addr2);
            Console.ReadKey();
        }
    }
}
