using System;
using System.Collections.Generic;
using AutomatedTasker.StepConfig;
using AutomatedTasker.Steps;

namespace AutomatedTasker.App
{
    class Program
    {
        public static List<IStepConfig> Steps;

        public static void ListInitialize()
        {
            Steps = new List<IStepConfig>();

            for (int i = 0; i < 10; ++i)
            {
                CMDStep step = new CMDStep(i % 2 == 0 ? "blah" : "ping yandex.ru");
                IStepConfig config = new OrdinaryStepConfig(step,
                    i % 2 == 0 ? new ExecutionCondition { Always = true } :
                        new ExecutionCondition { IfPreviousSucceded = true });

                Steps.Add(config);
            }
        }

        static void Main(string[] args)
        {
            ListInitialize();

            Processor processor = new Processor(Steps);

            processor.Process();

            Console.WriteLine(processor.Info.HasFailed.ToString());
            foreach (var step in Steps)
            {
                Console.WriteLine((step.Step as CMDStep)?.Command + ": " + step.ExecutionStatus);
            }
        }
    }
}
