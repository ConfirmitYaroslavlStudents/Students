namespace GuitarShopLogic
{
    public abstract class Product
    {
        public Product(string id, string price)
        {
            ID = id;
            Price = price;
        }

        public string ID { get; set; }
        public string Price { get; set; }

        internal abstract bool ContainsTerm(string term);
    }
}
