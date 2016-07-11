using Creatures.Language.Executors;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Creaturestests
{
    public class TestsBase
    {
        protected void Check(string commands, params int[] values)
        {
            Check(commands, new ExecutorToolset(new Random()), values);
        }

        protected void Check(string commands, IExecutorToolset executorToolset, params int[] values)
        {
            var parsedCommands = new Parser().ProcessCommands(commands).ToArray();
            var output =
                new Executor().Execute(parsedCommands, executorToolset)
                    .Replace("\r\n", "\n")
                    .Split('\n')
                    .Where(item => !string.IsNullOrEmpty(item))
                    .Select(int.Parse)
                    .ToList();

            Assert.AreEqual(values.Count(), output.Count());

            for (var i = 0; i < values.Count(); i++)
            {
                Assert.AreEqual(values[i], output[i]);
            }
        }
    }
}
