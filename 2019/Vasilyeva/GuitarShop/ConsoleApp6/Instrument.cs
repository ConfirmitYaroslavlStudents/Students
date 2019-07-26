using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    public class Instrument
    {
        public Instrument(string id, string name, string model, string builder, string type)
        {
            ID = id;
            Price = name;
            Model = model;
            Builder = builder;
            Type = type;
        }
        public string ID { get; set; }
        public string Price { get; set; }
        public string Model { get; set; }
        public string Builder { get; set; }
        public string Type { get; set; }
    }
}
