using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    [TestClass]
    public class ConditionCheckerTests
    {
        [TestMethod]
        public void CheckAlwaysCondition()
        {
            var step = new StepConfig() { Step = new EmptyStep(), Condition = new ExecutionCondition { Always = true } };
            Assert.AreEqual(true, ExecutionConditionChecker.CheckCondition(step.Condition, new ProcessorInfo(), 0));
        }

        [TestMethod]
        public void CheckPreviousCondition()
        {
            var step = new StepConfig() { Step = new EmptyStep(), Condition = new ExecutionCondition { IfPreviousSucceded = true } };
            var info = new ProcessorInfo() { Steps = new List<StepConfig>() { step, step }, StepStatus = new List<Status>() { Status.Successful, Status.Waiting } };
            Assert.AreEqual(true, ExecutionConditionChecker.CheckCondition(step.Condition, info, 1));
        }

        [TestMethod]
        public void CheckPreviousSteps()
        {
            var info = new ProcessorInfo()
            {
                Steps = new List<StepConfig>()
                {
                    new StepConfig() { Step = new EmptyStep(), Condition = new ExecutionCondition { Always = true } },
                    new StepConfig() { Step = new EmptyStep(), Condition = new ExecutionCondition { Always = true } },
                    new StepConfig() { Step = new EmptyStep(), Condition = new ExecutionCondition { Always = true } },
                    new StepConfig() { Step = new EmptyStep(), Condition = new ExecutionCondition { Always = true } },
                    new StepConfig() { Step = new EmptyStep(), Condition = new ExecutionCondition { StepsSucceded = new [] { 0, 1, 2, 3} } }
                },
                StepStatus = new List<Status>()
                {
                    Status.Successful,
                    Status.Successful,
                    Status.Successful,
                    Status.Successful,
                    Status.Waiting
                }
            };
            Assert.AreEqual(true, ExecutionConditionChecker.CheckCondition(info.Steps[4].Condition, info, 4));
        }
    }
}
