namespace GuitarShopLogic
{
    internal class Guitar : DataBaseItem
    {
        public Guitar(string line) : base(line)
        {
            var parts = line.Split(' ');

            ID = parts[0];
            Price = parts[1];
            Model = parts[2];
            Builder = parts[3];
            Type = parts[4];
        }

        public string ID { get; set; }
        public string Price { get; set; }
        public string Model { get; set; }
        public string Builder { get; set; }
        public string Type { get; set; }

        internal override bool Contains(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }
    }
}
