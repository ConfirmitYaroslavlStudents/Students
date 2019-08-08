namespace ShopLib
{
    public class Guitar: Instrument
    {
        public Guitar(string id, string name, string model, string builder, string type)
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

        public bool Contains(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }

    }
}
