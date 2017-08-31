using System.Collections.Generic;
using Tasker.Core.BehaviourTree.Nodes;

namespace Tasker.Core.BehaviourTree.ExecutionBehaviours
{
    public class OrdinaryBehaviour : IExecutionBehaviour
    {
        public Status Execute(IEnumerable<INode> nodes)
        {
            foreach (INode node in nodes)
            {
                node.Execute();
            }

            return Status.Success;
        }
    }
}