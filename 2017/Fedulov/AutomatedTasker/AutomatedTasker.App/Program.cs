using System;
using System.Collections.Generic;
using AutomatedTasker.StepConfig;
using AutomatedTasker.Steps;
using AutomatedTasker.UI;

namespace AutomatedTasker.App
{
    class Program
    {
        /*public static List<IStepConfig> Steps;

        public static void ListInitialize()
        {
            var steps1 = new List<IStepConfig>();
            var steps2 = new List<IStepConfig>();

            for (int i = 0; i < 10; ++i)
            {
                CMDStep step = new CMDStep(i % 2 == 1 ? "blah" : "ping yandex.ru");
                if (i%2 == 0)
                    steps1.Add(new OrdinaryStepConfig(step, new ExecutionCondition { Always = true }));
                else
                    steps2.Add(new OrdinaryStepConfig(step, new ExecutionCondition { IfPreviousSucceded = true}));
            }

            Steps = new List<IStepConfig>();
            for (int i = 0; i < 10; ++i)
            {
                CMDStep step = new CMDStep("ping google.com");
                Steps.Add(new OrdinaryStepConfig(step, new ExecutionCondition { IfPreviousSucceded = true }));
            }
            Steps.Add(new ConditionalStepConfig(Steps[0], steps1, steps2));
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
        }*/

        public static void Main(string[] args)
        {
            UserInterface userInterface = new UserInterface();

            Console.WriteLine(userInterface.HelpMessage());
            Console.Write("Enter command: ");
            string command = Console.ReadLine();

            var steps = userInterface.ParseArguments(command);
            Processor processor = new Processor(steps);

            processor.Process();
            Console.WriteLine(processor.Info.HasFailed ? "Some commands failed!" : "Execution succeded!");
        }
    }
}
