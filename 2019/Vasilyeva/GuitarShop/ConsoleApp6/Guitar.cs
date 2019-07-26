using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Guitar : Instrument
    {
        public Guitar(string id, string name, string model, string builder, string type) : base(id, name, model, builder, type) { }

        public bool Contains(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }
    }
}
