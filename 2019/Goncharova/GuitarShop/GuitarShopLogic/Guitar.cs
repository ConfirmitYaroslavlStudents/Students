namespace GuitarShopLogic
{
    internal class Guitar : Product
    {
        public Guitar(string id, string price, string model, string builder, string type) : base(id, price)
        {
            Model = model;
            Builder = builder;
            Type = type;
        }

        public string Model { get; set; }
        public string Builder { get; set; }
        public string Type { get; set; }

        internal override bool ContainsTerm(string term)
        {
            return Builder.Contains(term) || Model.Contains(term) || Type.Contains(term);
        }
    }
}
