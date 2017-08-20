using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasker.Core;
using Tasker.Core.Applets;
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
                ExecutionCondition.Always, State.Failed);
            TestApplet secondApplet = new TestApplet(
                ExecutionCondition.IfPreviousIsSuccessful, State.Failed);

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
                ExecutionCondition.Always, State.Failed);
            TestApplet secondApplet = new TestApplet(
                ExecutionCondition.Always, State.Failed);

            Processor processor = new Processor();
            processor.AddApplet(firstApplet);
            processor.AddApplet(secondApplet);

            processor.Start();

            Assert.AreEqual(true, firstApplet.Started);
            Assert.AreEqual(true, secondApplet.Started);
        }

        [TestMethod]
        public void TriggerApplet()
        {
            TestApplet positiveApplet = new TestApplet();
            TestApplet negativeApplet = new TestApplet();

            TriggerApplet trigger 
                = new TriggerApplet(State.Successful, new TestApplet(ExecutionCondition.Always, State.Successful));
            trigger.AddPositiveApplet(positiveApplet);
            trigger.AddNegativeApplet(negativeApplet);

            State resultState = trigger.Execute();

            Assert.AreEqual(State.Successful, resultState);
            Assert.AreEqual(true, positiveApplet.Started);
            Assert.AreEqual(false, negativeApplet.Started);
        }

        /// <summary>
        /// if(mainTrigger)
        ///     if(positiveTrigger) 
        ///         pPositiveApplet
        ///     else
        ///         pNegativeApplet (launched)
        /// else
        ///     negativeApplet
        /// </summary>
        [TestMethod]
        public void NestedTriggerApplet()
        {
            TriggerApplet mainTrigger 
                = new TriggerApplet(State.Successful, new TestApplet());
            TriggerApplet positiveTrigger 
                = new TriggerApplet(State.Successful, new TestApplet(ExecutionCondition.Always, State.Failed));
            TestApplet pPositiveApplet = new TestApplet();
            TestApplet pNegativeApplet = new TestApplet();
            TestApplet negativeApplet = new TestApplet();

            mainTrigger.AddPositiveApplet(positiveTrigger);
            positiveTrigger.AddPositiveApplet(pPositiveApplet);
            positiveTrigger.AddNegativeApplet(pNegativeApplet);
            mainTrigger.AddNegativeApplet(negativeApplet);

            mainTrigger.Execute();

            Assert.AreEqual(false, pPositiveApplet.Started);
            Assert.AreEqual(true, pNegativeApplet.Started);
            Assert.AreEqual(false, negativeApplet.Started);
        }
    }
}