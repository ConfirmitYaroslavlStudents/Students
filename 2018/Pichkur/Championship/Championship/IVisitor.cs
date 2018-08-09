namespace Championship
{
    public interface IVisitor
    {
        void VisitSingleChampionship(SingleChampionship singleChampionship);
        void VisitDoubleChampionship(DoubleChampionship doubleChampionship);
    }
}
