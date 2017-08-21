using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoProcessor;
using System;

namespace AutoProcessorTests
{
    [TestClass]
    public class ProcessorTest
    {
        private Processor _processor;
        private Process _headProcess;
        private Process _secondProcess;
        private Process _thirdProcess;

        [TestInitialize]
        public void ProcessorInit()
        {
            _headProcess = new Process(
                new IStep[]
                {
                    new EmptyStep(),
                    new EmptyStep(),
                    new EmptyStep()
                }
            );

            _secondProcess = new Process(
                new IStep[]
                {
                    new EmptyStep(),
                    new EmptyStep(),
                    new EmptyStep()
                }
            );

            _thirdProcess = new Process(
                new IStep[]
                {
                    new EmptyStep(),
                    new EmptyStep(),
                    new EmptyStep()
                }
            );

            _headProcess.NextProcess = _secondProcess;
            _secondProcess.NextProcess = _thirdProcess;

            _processor = new Processor(_headProcess);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionIfStartedWithHeadProcessNull()
        {
            _processor = new Processor(null);
            _processor.Start();
        }

        [TestMethod]
        public void ProcessesHaveStatusNotStartedBeforeStart()
        {
            var expectedProcessStatus = Status.NotStarted;

            Assert.AreEqual(expectedProcessStatus, _headProcess.ProcessStatus);
            Assert.AreEqual(expectedProcessStatus, _secondProcess.ProcessStatus);
            Assert.AreEqual(expectedProcessStatus, _thirdProcess.ProcessStatus);
        }

        [TestMethod]
        public void ProcessesHaveStatusFinishedAfterStart()
        {
            _processor.Start();

            var expectedProcessStatus = Status.Finished;

            Assert.AreEqual(expectedProcessStatus, _headProcess.ProcessStatus);
            Assert.AreEqual(expectedProcessStatus, _secondProcess.ProcessStatus);
            Assert.AreEqual(expectedProcessStatus, _thirdProcess.ProcessStatus);
        }
    }
}
