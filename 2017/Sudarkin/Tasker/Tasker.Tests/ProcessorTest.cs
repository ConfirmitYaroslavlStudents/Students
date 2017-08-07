using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasker.Core;
using Tasker.Tests.Helpers;

namespace Tasker.Tests
{
    [TestClass]
    public class ProcessorTest
    {
        [TestMethod]
        public void NotRunIfPreviousIsNotSuccessful()
        {
            TestApplet firstApplet = new TestApplet(
                ExecutionCondition.Always, new TestOptions(), State.Failed);
            TestApplet secondApplet = new TestApplet(
                ExecutionCondition.IfPreviousIsSuccessful, new TestOptions(), State.Failed);

            Processor processor = new Processor();
            processor.AddApplet(firstApplet);
            processor.AddApplet(secondApplet);

            processor.Start();

            Assert.AreEqual(true, firstApplet.Started);
            Assert.AreEqual(false, secondApplet.Started);
        }

        [TestMethod]
        public void RunAlways()
        {
            TestApplet firstApplet = new TestApplet(
                ExecutionCondition.Always, new TestOptions(), State.Failed);
            TestApplet secondApplet = new TestApplet(
                ExecutionCondition.Always, new TestOptions(), State.Failed);

            Processor processor = new Processor();
            processor.AddApplet(firstApplet);
            processor.AddApplet(secondApplet);

            processor.Start();

            Assert.AreEqual(true, firstApplet.Started);
            Assert.AreEqual(true, secondApplet.Started);
        }
    }
}