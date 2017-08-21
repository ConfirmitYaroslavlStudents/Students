using System.Collections.Generic;
using Tasker.Core.BehaviourTree.Nodes;

namespace Tasker.Core.BehaviourTree.ExecutionBehaviours
{
    public interface IExecutionBehaviour
    {
        Status Execute(IEnumerable<INode> nodes);
    }
}