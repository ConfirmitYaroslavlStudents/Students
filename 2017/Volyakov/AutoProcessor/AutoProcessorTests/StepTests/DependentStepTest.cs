using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoProcessor;

namespace AutoProcessorTests
{
    [TestClass]
    public class DependentStepTest
    {
        [TestMethod]
        public void NotStartedStatusBeforeStart()
        {
            var dependentStep = new DependentStep(
                new ConsoleWriteStep("Dependent step not started"),
                new StatusStep(Status.Finished)
                );
            
            var expectedStatus = Status.NotStarted;

            Assert.AreEqual(expectedStatus, dependentStep.StepStatus);
        }

        [TestMethod]
        public void MainStepStartedIfFlagStepFinished()
        {
            var dependentStep = new DependentStep(
                new ConsoleWriteStep("Dependent step started"),
                new StatusStep(Status.Finished)
                );

            dependentStep.Start();

            var expectedStatus = Status.Finished;

            Assert.AreEqual(expectedStatus, dependentStep.StepStatus);
        }

        [TestMethod]
        public void MainStepNotStartedIfFlagStepError()
        {
            var dependentStep = new DependentStep(
                new ConsoleWriteStep("Dependent step not started"),
                new StatusStep(Status.Error)
                );

            dependentStep.Start();

            var expectedStatus = Status.NotStarted;

            Assert.AreEqual(expectedStatus, dependentStep.StepStatus);
        }

        [TestMethod]
        public void DependentStepErrorIfMainStepError()
        {
            var dependentStep = new DependentStep(
                new StatusStep(Status.Error),
                new StatusStep(Status.Finished)
                );

            dependentStep.Start();

            var expectedStatus = Status.Error;

            Assert.AreEqual(expectedStatus, dependentStep.StepStatus);
        }
    }
}
