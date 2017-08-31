using Tasker.Core.Actions;

namespace Tasker.Core.BehaviourTree.Nodes
{
    public class ActionNode : INode
    {
        private readonly IAction _action;

        public ActionNode(IAction action)
        {
            _action = action;
        }

        public Status Execute()
        {
            try
            {
                return _action.Execute();
            }
            catch
            {
                return Status.Failed;
            }
        }
    }
}