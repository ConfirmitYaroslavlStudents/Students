namespace AutoProcessor
{
    public class StepStatusPair
    {
        public IStep Step { get; private set; }

        public Status Status { get; set; }

        public StepStatusPair(IStep step, Status status)
        {
            Step = step;
            Status = status;
        }
    }
}
