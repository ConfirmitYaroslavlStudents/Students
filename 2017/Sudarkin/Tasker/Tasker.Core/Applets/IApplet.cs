using Tasker.Core.Options;

namespace Tasker.Core.Applets
{
    public interface IApplet<out T> where T : IOptions
    {
        ExecutionCondition Condition { get; }
        T Options { get; }

        State Execute();
    }
}