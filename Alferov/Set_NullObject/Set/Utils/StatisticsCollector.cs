namespace Set.Utils
{
    public class StatisticsCollector : AbstractStatisticsCollector
    {
        private int _statistics;

        public override int Statistics
        {
            get { return _statistics; }
        }

        internal override void ChangeStatistics(int delta)
        {
            _statistics += delta;
        }
    }
}