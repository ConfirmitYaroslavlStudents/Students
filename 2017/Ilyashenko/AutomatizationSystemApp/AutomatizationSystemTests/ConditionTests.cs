using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    [TestClass]
    public class ConditionTests
    {
        [TestMethod]
        public void TryNotExecutableCondition()
        {
            var obj = new TestObject();
            var steps = new StepConfig() { Step = new TestStep(obj), Condition = new ExecutionCondition() };
            var processor = new Processor(new[] { steps });

            Assert.AreEqual(false, obj.Changed);
        }

        [TestMethod]
        public void TryConditionalStep()
        {
            var obj = new TestObject();
            var step = new ConditionalStepConfig
            (
                new StepConfig[]
                {
                    new StepConfig() { Step = new TestStep(obj), Condition = new ExecutionCondition() { Always = true } }
                },
                new StepConfig[] { }, 
                new TestRandom()
            )
            { Condition = new ExecutionCondition() { Always = true } };
            var processor = new Processor(new StepConfig[] { step });
            processor.Execute();

            Assert.AreEqual(true, obj.Changed);
            Assert.AreEqual(2, processor.Info.Steps.Count);
        }
    }
}
