using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_shop
{
    class Guitar: Instrument
    {
        public Guitar(string id, string name, string model, string builder, string type)
        {
            ID = id;
            Price = name;
            Model = model;
            Builder = builder;
            Type = type;
        }
        public Guitar() 
        {
            ID = null;
            Price = null;
            Model = null;
            Builder = null;
            Type = null;
        }
        public Guitar(string term) : base(term)
        {
            var terms = term.Split(' ');
            ID = terms[0];
            Price = terms[1];
            Model = terms[2];
            Builder = terms[3];
            Type = terms[4];
        }
        public string Model { get; set; }
        public string Builder { get; set; }
        public string Type { get; set; }

        public bool Contains(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }

        public override Instrument Make(string term)
        {
            var terms = term.Split(' ');
            return new Guitar(terms[0], terms[1], terms[2], terms[3], terms[4]);
        }

        public override bool ContainsTerm(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }

        public override void ConsoleWriteTerms()
        {
            Console.WriteLine("Enter guitar terms (ID Price Model Builder Type):");
        }
    }
}
