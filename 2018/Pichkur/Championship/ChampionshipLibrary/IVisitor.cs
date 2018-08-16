namespace Championship
{
    public interface IVisitor
    {
        void Visit(SingleChampionshipManager singleChampionship);
        void Visit(DoubleChampionshipManager doubleChampionship);
    }
}
