using Tasker.Core;
using Tasker.Core.Actions;

namespace Tasker.Tests.Helpers
{
    public class TestAction : IAction
    {
        private readonly Status _returnedStatus;

        public bool Started { get; private set; }

        public TestAction(Status returnedStatus)
        {
            _returnedStatus = returnedStatus;
        }

        public Status Execute()
        {
            Started = true;
            return _returnedStatus;
        }
    }
}