using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class SwapCommandMutationTest
    {
        private CommandsEqualityComparer _comparer = new CommandsEqualityComparer();

        [TestMethod]
        public void SwapCommands()
        {
            var precommands =
               new StringBuilder()
                   .AppendLine("int zero")
                   .AppendLine("zero = 0")
                   .AppendLine("if zero then")
                   .AppendLine("stop")
                   .AppendLine("endif");
            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();

            var firstSwapIndex = 1;
            var secondSwapIndex = 3;

            var newCommands = new SwapCommandMutation(
                new SpecialRandom(firstSwapIndex, secondSwapIndex)
                ).Transform(commands);

            var newPreCommands =
                new StringBuilder()
                   .AppendLine("int zero")
                   .AppendLine("stop")
                   .AppendLine("if zero then")
                   .AppendLine("zero = 0")
                   .AppendLine("endif");
            var parsedNewCommands = new Parser().ProcessCommands(newPreCommands.ToString()).ToArray();

            Assert.AreEqual(parsedNewCommands.Length, newCommands.Length);
            for (int i = 0; i < newCommands.Length; i++)
            {
                Assert.IsTrue(_comparer.IsEqual(newCommands[i], parsedNewCommands[i]));
            }
        }
    }
}
