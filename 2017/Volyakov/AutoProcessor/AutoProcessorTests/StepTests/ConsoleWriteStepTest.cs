using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoProcessor;

namespace AutoProcessorTests
{
    [TestClass]
    public class ConsoleWriteStepTest
    {
        [TestMethod]
        public void NotStartedStatusBeforeStart()
        {
            var step = new ConsoleWriteStep("Hello world!");

            var expectedStatus = Status.NotStarted;

            Assert.AreEqual(expectedStatus,step.StepStatus);
        }

        [TestMethod]
        public void FinishedStatusAfterStart()
        {
            var step = new ConsoleWriteStep("Hello world!");

            step.Start();

            var expectedStatus = Status.Finished;

            Assert.AreEqual(expectedStatus, step.StepStatus);
        }
    }
}
