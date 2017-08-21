using AutomatizationSystemLib;

namespace AutomatizationSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var steps = new StepConfig[]
            {
                new StepConfig() { Step = new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "Hello! This is the test application."}), Condition = new ExecutionCondition() {Always = true}},
                new ConditionalStepConfig(new StepConfig[]
                {
                    new StepConfig() { Step = new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "First branch succeded!"}), Condition = new ExecutionCondition() {Always = true} }
                },
                new StepConfig[]
                {
                    new StepConfig() { Step = new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "Second branch succeded!"}), Condition = new ExecutionCondition() {Always = true} }
                },
                new System.Random()
                ) { Condition = new ExecutionCondition() { Always = true } },
                new StepConfig() { Step = new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "The end!"}), Condition = new ExecutionCondition() {Always = true}},
            };
            var processor = new Processor(steps);
            processor.Execute();
        }
    }
}
