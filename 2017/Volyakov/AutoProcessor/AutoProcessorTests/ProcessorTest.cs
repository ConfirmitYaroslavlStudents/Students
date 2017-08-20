using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoProcessor;

namespace AutoProcessorTests
{
    [TestClass]
    public class ProcessorTest
    {
        private Processor _processor;

        [TestInitialize]
        public void InitProcessor()
        {
            _processor = new Processor();
        }

        [TestMethod]
        public void StepsFinishedAfterStartProcess()
        {
            var steps = new Step[]
            {
                new EmptyStep(),
                new ConsoleWriteStep("Hello"),
                new EmptyStep()
            };

            var process = new Process(steps);

            _processor.StartProcess(process);

            var expectedStatus = Status.Finished;

            foreach (var step in process.Steps)
                Assert.AreEqual(expectedStatus, step.StepStatus);
        }
    }
}
