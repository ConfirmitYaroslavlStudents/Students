using AutoProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoProcessorTests
{
    [TestClass]
    public class DependentProcessTest
    {
        private DependentProcess _dependentProcess;
        private Process _mainProcess;
        private Process _flagProcess;
        private Process _nextForMainProcess;

        [TestInitialize]
        public void Init()
        {
            _mainProcess = new Process();
            _nextForMainProcess = new Process();
            _flagProcess = new Process();
            _dependentProcess = new DependentProcess(_mainProcess, _flagProcess);

            _mainProcess.NextProcess = _nextForMainProcess;
        }

        [TestMethod]
        public void NotStartedStatusBeforeStart()
        {
            var expectedStatus = Status.NotStarted;

            Assert.AreEqual(expectedStatus, _dependentProcess.ProcessStatus);
        }

        [TestMethod]
        public void FinishedStatusAfterStart()
        {
            _dependentProcess.Start();

            var expectedStatus = Status.Finished;

            Assert.AreEqual(expectedStatus, _dependentProcess.ProcessStatus);
        }

        [TestMethod]
        public void MainProcessWillBeNextIfFlagProcessFinished()
        {
            _flagProcess.Start();

            _dependentProcess.Start();

            var expectedNext = _mainProcess;

            Assert.AreEqual(expectedNext, _dependentProcess.NextProcess);
        }

        [TestMethod]
        public void MainProcessWillBeSkippedIfFlagProcessNotFinished()
        {
            _dependentProcess.Start();

            var expectedNext = _nextForMainProcess;

            Assert.AreEqual(expectedNext, _dependentProcess.NextProcess);
        }
    }
}
