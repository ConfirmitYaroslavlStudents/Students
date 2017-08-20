namespace Tasker.Core.Applets
{
    public interface IApplet
    {
        int Condition { get; }

        State Execute();
    }
}