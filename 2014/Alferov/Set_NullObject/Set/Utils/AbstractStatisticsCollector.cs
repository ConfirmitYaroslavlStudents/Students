namespace Set.Utils
{
    public abstract class AbstractStatisticsCollector
    {
        public abstract int Statistics { get; }
        internal abstract void ChangeStatistics(int delta);
    }
}