using AutoProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoProcessorTests
{
    [TestClass]
    public class BranchingProcessTest
    {
        private BranchingProcess _branchingProcess;
        private Process _flagProcess;
        private Process _ifFinished;
        private Process _ifNotFinished;

        [TestInitialize]
        public void Init()
        {
            _flagProcess = new Process();

            _ifFinished = new Process();

            _ifNotFinished = new Process();

            _branchingProcess = new BranchingProcess(_flagProcess, _ifFinished, _ifNotFinished);
        }

        [TestMethod]
        public void NotStartedStatusBeforeStart()
        {
            var expectedStatus = Status.NotStarted;

            Assert.AreEqual(expectedStatus, _branchingProcess.ProcessStatus);
        }

        [TestMethod]
        public void FinishedStatusAfterStart()
        {
            _branchingProcess.Start();

            var expectedStatus = Status.Finished;

            Assert.AreEqual(expectedStatus, _branchingProcess.ProcessStatus);
        }

        [TestMethod]
        public void IfFinishedProcessWillBeNextIfFlagProcessFinished()
        {
            _flagProcess.Start();

            _branchingProcess.Start();

            var expectedNext = _ifFinished;

            Assert.AreEqual(expectedNext, _branchingProcess.NextProcess);
        }

        [TestMethod]
        public void IfNotFinishedProcessWillBeNextIfFlagProcessNotFinished()
        {
            _branchingProcess.Start();

            var expectedNext = _ifNotFinished;

            Assert.AreEqual(expectedNext, _branchingProcess.NextProcess);
        }
    }
}
