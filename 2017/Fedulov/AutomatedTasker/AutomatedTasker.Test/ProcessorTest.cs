using System.Collections.Generic;
using AutomatedTasker.StepConfig;
using AutomatedTasker.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomatedTasker.Test
{
    [TestClass]
    public class ProcessorTest
    {
        private List<IStepConfig> _steps;
        private List<IStepConfig> _stepsWithCondition;

        [TestInitialize]
        public void InitializeSteps()
        {
            _steps = new List<IStepConfig>();

            for (int i = 0; i < 10; ++i)
            {
                IStepConfig config = new EmptyStepConfig { Step = new EmptyStep() };

                config.Condition = i % 2 == 0 ? new ExecutionCondition {Always = true} : 
                    new ExecutionCondition {IfPreviousSucceded = true};

                _steps.Add(config);
            }

            _stepsWithCondition = new List<IStepConfig>();

            for (int i = 0; i < 3; ++i)
            {
                IStepConfig config = new EmptyStepConfig { Step = new EmptyStep() };

                config.Condition = i % 2 == 0 ? new ExecutionCondition { Always = true } :
                    new ExecutionCondition { IfPreviousSucceded = true };

                _stepsWithCondition.Add(config);
            }

            List<IStepConfig> stepsIfTrue = new List<IStepConfig>();
            for (int i = 0; i < 5; ++i)
            {
                IStepConfig config = new EmptyStepConfig { Step = new EmptyStep() };

                config.Condition = new ExecutionCondition {Always = true};

                stepsIfTrue.Add(config);
            }

            List<IStepConfig> stepsIfFalse = new List<IStepConfig>();
            for (int i = 0; i < 5; ++i)
            {
                IStepConfig config = new EmptyStepConfig { Step = new EmptyStep() };

                config.Condition = new ExecutionCondition { Always = true };

                stepsIfTrue.Add(config);
            }

            ConditionalStepConfig conditionalStepConfig = new ConditionalStepConfig(_stepsWithCondition[0], 
                stepsIfTrue, stepsIfFalse);
            _stepsWithCondition.Add(conditionalStepConfig);
        }

        [TestMethod]
        public void TestProcess()
        {
            Processor processor = new Processor(_steps);
            
            processor.Process();

            Assert.IsFalse(processor.Info.HasFailed);

            foreach (var step in _steps)
            {
                var emptyStepConfig = step as EmptyStepConfig;
                Assert.IsTrue(emptyStepConfig != null);
                Assert.IsTrue(emptyStepConfig.ExecutionStatus == Status.Success);
                Assert.IsTrue(emptyStepConfig.Processed);
            }
        }

        [TestMethod]
        public void TestProcessWithConditionalStep()
        {
            Processor processor = new Processor(_steps);

            processor.Process();

            Assert.IsFalse(processor.Info.HasFailed);

            foreach (var step in _steps)
            {
                if (step is EmptyStepConfig)
                {
                    var emptyStepConfig = step as EmptyStepConfig;

                    Assert.IsTrue(emptyStepConfig.ExecutionStatus == Status.Success);
                    Assert.IsTrue(emptyStepConfig.Processed);
                }
                else if (step is ConditionalStepConfig)
                {
                    var conditionalStepConfig = step as ConditionalStepConfig;

                    Assert.IsTrue(conditionalStepConfig.ExecutionStatus == Status.Success);

                    foreach (var stepIfTrue in conditionalStepConfig.StepsIfTrue)
                    {
                        Assert.IsTrue(stepIfTrue.ExecutionStatus == Status.Success);
                    }

                    foreach (var stepIfFalse in conditionalStepConfig.StepsIfFalse)
                    {
                        Assert.IsTrue(stepIfFalse.ExecutionStatus == Status.NotStarted);
                    }
                }
            }
        }
    }
}
