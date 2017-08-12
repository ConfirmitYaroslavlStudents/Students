namespace AutomatizationSystemLib
{
    public interface IStep
    {
        void Execute(bool previousStepsExecutedCorrectly);
    }
}
