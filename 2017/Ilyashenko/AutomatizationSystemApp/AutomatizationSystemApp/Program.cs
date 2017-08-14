using System.Collections.Generic;
using AutomatizationSystemLib;

namespace AutomatizationSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var steps = new List<IStep>()
            {
                new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "Hello! This is done to introduce the work of conditional steps."}),
                new RandomConditionStep(),
                new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "Random condition led us to the FIRST part of the code!"}),
                new NextStep(new NextStepOptions() { NextStep = 5}),
                new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "Random condition led us to the SECOND part of the code!"}),
                new ConsoleWriteStep(new ConsoleWriteOptions() { Message = "The End!"})
            };
            var processor = new Processor(steps);
            processor.Execute();
        }
    }
}
