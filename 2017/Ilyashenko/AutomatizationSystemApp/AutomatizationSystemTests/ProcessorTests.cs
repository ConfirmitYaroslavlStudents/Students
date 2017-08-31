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
            var steps = new StepConfig() { Step = new TestStep(obj), Condition = new ExecutionCondition() { Always = true } };
            var processor = new Processor(new[] { steps });

            processor.Execute();

            Assert.AreEqual(true, obj.Changed);
        }

        [TestMethod]
        public void TryAddStep()
        {
            var obj = new TestObject();
            var processor = new Processor(new StepConfig[] { });
            processor.AddStep(new StepConfig() { Step = new TestStep(obj), Condition = new ExecutionCondition() { Always = true } });

            processor.Execute();

            Assert.AreEqual(true, obj.Changed);
        }
    }
}
