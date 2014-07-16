namespace Set.Utils
{
    public class StatisticsCollector
    {
        public int Statistics { get; private set; }

        internal void ChangeStatistics(int delta)
        {
            Statistics += delta;
        }
    }
}
