namespace CellsAutomate.Food
{
    public interface IStrategyForBuild
    {
        void Build(bool[,] creatures, FoodMatrix eatMatrix);
    }
}
