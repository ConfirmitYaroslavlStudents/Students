namespace CellsAutomate.Food
{
    public interface IFoodDistributionStrategy
    {
        void Build(bool[,] creatures, FoodMatrix eatMatrix);
    }
}
