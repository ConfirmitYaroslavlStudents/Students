using Tasker.Core;
using Tasker.Core.Applets;

namespace Tasker.Tests.Helpers
{
    public class TestApplet : IApplet
    {
        private readonly State _returnedState;

        public bool Started { get; private set; }

        public int Condition { get; }

        public TestApplet() 
            : this(ExecutionCondition.Always, State.Successful)
        {
            
        }

        public TestApplet(int condition, State returnedState)
        {
            Condition = condition;
            _returnedState = returnedState;
        }

        public State Execute()
        {
            Started = true;
            return _returnedState;
        }
    }
}