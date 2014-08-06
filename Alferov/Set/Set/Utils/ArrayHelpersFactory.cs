namespace Set.Utils
{
    public class ArrayHelpersFactory<T>
    {
        public ArrayHelper<T> GetArrayHelper()
        {
            var arrayHelper = new ArrayHelper<T>();
            return arrayHelper;
        }

        public ArrayHelper<T> GetArrayHelper(StatisticsCollector statisticsCollector)
        {
            var arrayHelper = new ArrayHelper<T>();
            arrayHelper.OnOperationExecute += statisticsCollector.CollectStatistics;
            return arrayHelper;
        }
    }
}