namespace Set.Utils
{
    internal class NullStatisticsCollector : AbstractStatisticsCollector
    {
        public override int Statistics
        {
            get { return 0; }
        }

        internal override void ChangeStatistics(int delta)
        {
        }
    }
}