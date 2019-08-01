using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_shop
{
    public abstract class Instrument
    {
        public Instrument(string line)
        {

        }
        public string ID { get; set; }
        public string Price { get; set; }
        public abstract bool ContainsTerm(string term);
        public abstract Instrument Make(string term);
        public abstract void ConsoleWriteTerms();
    }
}
