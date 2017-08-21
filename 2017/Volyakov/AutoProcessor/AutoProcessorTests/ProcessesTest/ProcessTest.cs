using AutoProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoProcessorTests
{
    [TestClass]
    public class ProcessTest
    {
        private Process _process;

        [TestInitialize]
        public void ProcessInit()
        {
            _process = new Process();
        }

        [TestMethod]
        public void ProcessHasNotStartedStatusBeforeStart()
        {
            var expectedProcessStatus = Status.NotStarted;

            Assert.AreEqual(expectedProcessStatus, _process.ProcessStatus);
        }

        [TestMethod]
        public void ProcessHasFinishedStatusAfterStart()
        {
            _process.Start();

            var expectedProcessStatus = Status.Finished;

            Assert.AreEqual(expectedProcessStatus, _process.ProcessStatus);
        }

        [TestMethod]
        public void ProcessHasErrorStatusAfterStartIfAllStepsHaveErrorStatus()
        {
            _process.Steps = new StepCollection
                (
                    new IStep[]
                    {
                        new ErrorStep(),
                        new ErrorStep()
                    }
                );


            _process.Start();

            var expectedProcessStatus = Status.Error;

            Assert.AreEqual(expectedProcessStatus, _process.ProcessStatus);
        }
    }
}
