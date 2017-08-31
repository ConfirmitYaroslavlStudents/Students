using System;

namespace Tasker.Core.BehaviourTree.Nodes
{
    public class ConditionNode : INode
    {
        private readonly Func<bool> _condition;
        private readonly BehaviourTree _positiveBrach;
        private readonly BehaviourTree _negativeBrach;

        public ConditionNode(Func<bool> condition, 
            BehaviourTree positive, BehaviourTree negative)
        {
            _condition = condition;
            _positiveBrach = positive;
            _negativeBrach = negative;
        }

        public Status Execute()
        {
            if (_condition.Invoke())
            {
                _positiveBrach.Start();
                return Status.Success;
            }

            _negativeBrach.Start();
            return Status.Failed;
        }
    }
}