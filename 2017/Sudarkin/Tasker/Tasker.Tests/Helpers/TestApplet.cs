using Tasker.Core;
using Tasker.Core.Applets;

namespace Tasker.Tests.Helpers
{
    public class TestApplet : IApplet<TestOptions>
    {
        private readonly State _returnedState;

        public bool Started { get; private set; }

        public ExecutionCondition Condition { get; }
        public TestOptions Options { get; }

        public TestApplet(ExecutionCondition condition, TestOptions options, State returnedState)
        {
            Condition = condition;
            Options = options;
            _returnedState = returnedState;
        }

        public State Execute()
        {
            Started = true;
            return _returnedState;
        }
    }
}