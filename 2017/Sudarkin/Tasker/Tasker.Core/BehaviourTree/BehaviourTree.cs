using System;
using System.Collections.Generic;
using Tasker.Core.Actions;
using Tasker.Core.BehaviourTree.ExecutionBehaviours;
using Tasker.Core.BehaviourTree.Nodes;

namespace Tasker.Core.BehaviourTree
{
    public class BehaviourTree
    {
        private readonly Queue<INode> _items;

        public BehaviourTree()
        {
            _items = new Queue<INode>();
        }

        public void Add(INode node)
        {
            _items.Enqueue(node);
        }

        public Status Start()
        {
            return Start(new OrdinaryBehaviour());
        }

        public Status Start(IExecutionBehaviour behaviour)
        {
            return behaviour.Execute(_items);
        }

        public class Builder
        {
            private readonly BehaviourTree _tree;

            public Builder()
            {
                _tree = new BehaviourTree();
            }

            public Builder Do(IAction action)
            {
                if (action == null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                _tree.Add(new ActionNode(action));
                return this;
            }

            public Builder Condition(Func<bool> condition, BehaviourTree positiveBranch, BehaviourTree negativeBranch)
            {
                _tree.Add(new ConditionNode(condition,
                    positiveBranch ?? new BehaviourTree(),
                    negativeBranch ?? new BehaviourTree()));

                return this;
            }

            public Builder Sequence(BehaviourTree subTree)
            {
                if (subTree == null)
                {
                    throw new ArgumentNullException(nameof(subTree));
                }

                _tree.Add(new SequenceNode(subTree));
                return this;
            }

            public BehaviourTree Build()
            {
                return _tree;
            }
        }
    }
}