using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoProcessor;

namespace AutoProcessorTests
{
    [TestClass]
    public class StepProcessorTest
    {
        [TestMethod]
        public void StepsFinishedAfterStart()
        {
            var steps = new StepCollection
            {
                new EmptyStep(),
                new EmptyStep(),
                new EmptyStep()
            };

            StepProcessor.Start(steps);

            var expectedStatus = Status.Finished;

            foreach (StepStatusPair pair in steps)
                Assert.AreEqual(expectedStatus, pair.Status);
        }
    }
}
