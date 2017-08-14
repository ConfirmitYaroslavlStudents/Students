using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoProcessor;
using System.Collections.Generic;

namespace AutoProcessorTests
{
    [TestClass]
    public class BranchingStepTest
    {
        private List<Step> _stepsIfFinished;
        private List<Step> _stepsIfError;

        [TestInitialize]
        public void InitLists()
        {
            _stepsIfFinished = new List<Step>
            {
                new EmptyStep(),
                new EmptyStep(),
                new EmptyStep()
            };

            _stepsIfError = new List<Step>
            {
                new EmptyStep(),
                new EmptyStep(),
                new EmptyStep()
            };
            
        }

        [TestMethod]
        public void NotStartedStatusBeforeStart()
        {
            var flagStep = new StatusStep(Status.Finished);

            var branchingStep = new BranchingStep(flagStep, _stepsIfFinished, _stepsIfError);
            
            var expectedStatus = Status.NotStarted;

            Assert.AreEqual(expectedStatus, branchingStep.StepStatus);
        }

        [TestMethod]
        public void ErrorStatusIfFlagStepNotStarted()
        {
            var flagStep = new StatusStep(Status.NotStarted);

            var branchingStep = new BranchingStep(flagStep, _stepsIfFinished, _stepsIfError);

            branchingStep.Start();

            var expectedStatus = Status.Error;

            Assert.AreEqual(expectedStatus, branchingStep.StepStatus);
        }

        [TestMethod]
        public void ErrorStatusIfFlagStepLaunched()
        {
            var flagStep = new StatusStep(Status.Launched);

            var branchingStep = new BranchingStep(flagStep, _stepsIfFinished, _stepsIfError);

            branchingStep.Start();

            var expectedStatus = Status.Error;

            Assert.AreEqual(expectedStatus, branchingStep.StepStatus);
        }

        [TestMethod]
        public void FinishedStatusIfFlagStepFinished()
        {
            var flagStep = new StatusStep(Status.Finished);

            var branchingStep = new BranchingStep(flagStep, _stepsIfFinished, _stepsIfError);

            branchingStep.Start();

            var expectedStatus = Status.Finished;

            Assert.AreEqual(expectedStatus, branchingStep.StepStatus);
        }

        [TestMethod]
        public void FinishedStatusIfFlagStepError()
        {
            var flagStep = new StatusStep(Status.Error);

            var branchingStep = new BranchingStep(flagStep, _stepsIfFinished, _stepsIfError);

            branchingStep.Start();

            var expectedStatus = Status.Finished;

            Assert.AreEqual(expectedStatus, branchingStep.StepStatus);
        }

        [TestMethod]
        public void StepsFinishedIfFlagStepFinished()
        {
            var flagStep = new StatusStep(Status.Finished);

            var branchingStep = new BranchingStep(flagStep, _stepsIfFinished, _stepsIfError);

            branchingStep.Start();

            var expectedStatusForStepsIfFinished = Status.Finished;

            var expectedStatusForStepsIfError = Status.NotStarted;

            foreach (var ifFinishedStep in _stepsIfFinished)
                Assert.AreEqual(expectedStatusForStepsIfFinished, ifFinishedStep.StepStatus);

            foreach (var ifErrorStep in _stepsIfError)
                Assert.AreEqual(expectedStatusForStepsIfError, ifErrorStep.StepStatus);
        }

        [TestMethod]
        public void StepsFinishedIfFlagStepError()
        {
            var flagStep = new StatusStep(Status.Error);

            var branchingStep = new BranchingStep(flagStep, _stepsIfFinished, _stepsIfError);

            branchingStep.Start();

            var expectedStatusForStepsIfFinished = Status.NotStarted;

            var expectedStatusForStepsIfError = Status.Finished;

            foreach (var ifFinishedStep in _stepsIfFinished)
                Assert.AreEqual(expectedStatusForStepsIfFinished, ifFinishedStep.StepStatus);

            foreach (var ifErrorStep in _stepsIfError)
                Assert.AreEqual(expectedStatusForStepsIfError, ifErrorStep.StepStatus);
        }
    }
}
