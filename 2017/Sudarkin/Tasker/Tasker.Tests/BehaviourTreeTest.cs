using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasker.Core.BehaviourTree;
using Tasker.Tests.Helpers;
using Tasker.Core;

namespace Tasker.Tests
{
    [TestClass]
    public class BehaviourTreeTest
    {

        [TestMethod]
        public void ActionNode()
        {
            TestAction action1 = new TestAction(Status.Failed);
            TestAction action2 = new TestAction(Status.Success);

            new BehaviourTree.Builder()
                .Do(action1)
                .Do(action2)
                .Build()
                .Start();

            Assert.AreEqual(true, action1.Started);
            Assert.AreEqual(true, action2.Started);
        }

        [TestMethod]
        public void SequenceNode()
        {
            TestAction action1 = new TestAction(Status.Failed);
            TestAction action2 = new TestAction(Status.Success);

            var subTree = new BehaviourTree.Builder()
                .Do(action1)
                .Do(action2)
                .Build();

            new BehaviourTree.Builder()
                .Sequence(subTree)
                .Build()
                .Start();

            Assert.AreEqual(true, action1.Started);
            Assert.AreEqual(false, action2.Started);
        }

        [TestMethod]
        public void ConditionNode()
        {
            TestAction action1 = new TestAction(Status.Failed);
            TestAction action2 = new TestAction(Status.Success);

            new BehaviourTree.Builder()
                .Condition(() => true,
                    new BehaviourTree.Builder().Do(action1).Build(),
                    new BehaviourTree.Builder().Do(action2).Build())
                .Build()
                .Start();

            Assert.AreEqual(true, action1.Started);
            Assert.AreEqual(false, action2.Started);
        }

        [TestMethod]
        public void ActionNodeAfterSequenceNode()
        {
            TestAction action1 = new TestAction(Status.Failed);
            TestAction action2 = new TestAction(Status.Success);
            TestAction action3 = new TestAction(Status.Success);

            var subTree = new BehaviourTree.Builder()
                .Do(action1)
                .Do(action2)
                .Build();

            new BehaviourTree.Builder()
                .Sequence(subTree)
                .Do(action3)
                .Build()
                .Start();

            Assert.AreEqual(true, action1.Started);
            Assert.AreEqual(false, action2.Started);
            Assert.AreEqual(true, action3.Started);
        }

        //if(true) {
        //  if(false) {
        //      action1();
        //  } else {
        //      action2(); - launched
        //  }
        //} else {
        //}
        [TestMethod]
        public void NestedConditionNode()
        {
            TestAction action1 = new TestAction(Status.Failed);
            TestAction action2 = new TestAction(Status.Success);

            var positiveBranch1 = new BehaviourTree.Builder().Do(action1).Build();
            var negativeBranch2 = new BehaviourTree.Builder().Do(action2).Build();
            var nestedCondition = new BehaviourTree.Builder()
                .Condition(() => false, positiveBranch1, negativeBranch2)
                .Build();

            new BehaviourTree.Builder()
                .Condition(() => true, nestedCondition, null)
                .Build()
                .Start();

            Assert.AreEqual(false, action1.Started);
            Assert.AreEqual(true, action2.Started);
        }
    }
}