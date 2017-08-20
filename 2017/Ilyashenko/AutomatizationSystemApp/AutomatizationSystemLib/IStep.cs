namespace AutomatizationSystemLib
{
    public interface IStep
    {
        void Execute(Processor sender, int stepId);
    }
}
