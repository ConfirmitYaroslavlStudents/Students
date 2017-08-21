using System.Collections.Generic;
using Tasker.Core.BehaviourTree.Nodes;

namespace Tasker.Core.BehaviourTree.ExecutionBehaviours
{
    public class SequenceBehaviour : IExecutionBehaviour
    {
        public Status Execute(IEnumerable<INode> nodes)
        {
            foreach (INode node in nodes)
            {
                Status status = node.Execute();
                if (status != Status.Success)
                {
                    return status;
                }
            }

            return Status.Success;
        }
    }
}