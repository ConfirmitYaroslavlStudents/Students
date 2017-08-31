namespace AutoProcessor
{
    public static class StepProcessor
    {
        public static void Start(StepCollection collection)
        {
            foreach(StepStatusPair item in collection)
            {
                try
                {
                    item.Status = Status.Launched;

                    item.Step.Start();

                    item.Status = Status.Finished;
                }
                catch
                {
                    item.Status = Status.Error;
                }
            }
        }
    }
}
