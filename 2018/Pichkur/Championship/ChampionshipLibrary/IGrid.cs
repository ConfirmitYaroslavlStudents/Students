namespace Championship
{
    public interface IGrid
    {
        Team Champion { get; set; }

        void SetChampion();
        void StartNextTour();
    }
}
