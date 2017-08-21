using Tasker.Core.BehaviourTree.ExecutionBehaviours;

namespace Tasker.Core.BehaviourTree.Nodes
{
    public class SequenceNode : INode
    {
        private readonly BehaviourTree _subTree;

        public SequenceNode(BehaviourTree subTree)
        {
            _subTree = subTree;
        }

        public Status Execute()
        {
            return _subTree.Start(new SequenceBehaviour());
        }
    }
}