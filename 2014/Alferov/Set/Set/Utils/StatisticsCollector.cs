namespace Set.Utils
{
    public class StatisticsCollector
    {
        public int Statistics { get; private set; }

        internal void CollectStatistics(int value)
        {
            Statistics += value;
        }
    }
}