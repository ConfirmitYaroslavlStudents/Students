namespace AutoProcessor
{
    public abstract class Step
    {
        public Status StepStatus { get; protected set; }

        public abstract void Start();
    }
}
