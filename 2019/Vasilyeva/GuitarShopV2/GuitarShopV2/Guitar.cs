using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarShopV2
{
    public class Guitar : Instrument
    {
        public Guitar(string id, string name, string model, string builder, string type) : base(id, name, model, builder, type) { }

    }
}
