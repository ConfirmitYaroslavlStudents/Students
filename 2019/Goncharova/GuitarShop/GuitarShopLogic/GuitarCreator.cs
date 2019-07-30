namespace GuitarShopLogic
{
    public class GuitarCreator : Creator
    {
        public override Product CreateProduct(string line)
        {
            var parts = line.Split(' ');
            return new Guitar(parts[0], parts[1], parts[2], parts[3], parts[4]);
        }
    }
}
