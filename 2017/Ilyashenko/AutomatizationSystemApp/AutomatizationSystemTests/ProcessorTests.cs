using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    [TestClass]
    public class ProcessorTests
    {
        [TestMethod]
        public void TryProcessViaConstructor()
        {
            var obj = new TestObject();
            var steps = new IStep[] { new TestStep(obj) };
            var processor = new Processor(steps);

            processor.Execute();

            Assert.AreEqual(true, obj.Changed);
        }

        [TestMethod]
        public void TryAddStep()
        {
            var obj = new TestObject();
            var processor = new Processor(new IStep[] { });
            processor.AddStep(new TestStep(obj));

            processor.Execute();

            Assert.AreEqual(true, obj.Changed);
        }

        [TestMethod]
        public void TryConditionStepUsingFirstBranch()
        {
            var obj = new TestObject();
            var steps = new IStep[]
            {
                new TestConditionStepUsingFirstBranch(),
                new TestStatusStep(obj),
                new NextStep(new NextStepOptions() { NextStep = 4}),
                new TestStep(obj)
            };
            var processor = new Processor(steps);

            processor.Execute();

            Assert.AreEqual(Status.Successful, obj.Status);
            Assert.AreNotEqual(true, obj.Changed);
        }

        [TestMethod]
        public void TryConditionStepUsingSecondBranch()
        {
            var obj = new TestObject();
            var steps = new IStep[]
            {
                new TestConditionStepUsingSecondBranch(),
                new TestStatusStep(obj),
                new NextStep(new NextStepOptions() { NextStep = 4}),
                new TestStep(obj)
            };
            var processor = new Processor(steps);

            processor.Execute();

            Assert.AreNotEqual(Status.Successful, obj.Status);
            Assert.AreEqual(true, obj.Changed);
        }
    }
}
