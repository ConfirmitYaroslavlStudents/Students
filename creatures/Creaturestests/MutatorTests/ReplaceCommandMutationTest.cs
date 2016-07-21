using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class ReplaceCommandMutationTest
    {
        private CommandsEqualityComparer _comparer = new CommandsEqualityComparer();

        [TestMethod]
        public void ReplaceNewInt()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("zero = 0");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 0;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);

            Assert.IsTrue(_comparer.IsEqual(commands[1], resultCommands[1]));
            Assert.IsFalse(_comparer.IsEqual(commands[0], resultCommands[0]));
            Assert.AreEqual(commands[0].GetType(), resultCommands[0].GetType());
        }

        [TestMethod]
        public void ReplaceSetValue()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("zero = 0");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 1;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);

            Assert.IsTrue(_comparer.IsEqual(commands[0], resultCommands[0]));
            Assert.AreEqual(commands[1].GetType(), resultCommands[1].GetType());
            Assert.IsFalse(_comparer.IsEqual(commands[1], resultCommands[1]));
        }

        [TestMethod]
        public void ReplacePlus()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("one = 1")
                    .AppendLine("int two")
                    .AppendLine("zero = 0")
                    .AppendLine("two = zero + one")
                    .AppendLine("int four");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 5;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);

            for (int i = 0; i < commands.Length - 2; i++)
            {
                Assert.IsTrue(_comparer.IsEqual(commands[i], resultCommands[i]));
            }
            Assert.IsTrue(_comparer.IsEqual(commands[commands.Length - 1], resultCommands[commands.Length - 1]));
            Assert.AreEqual(commands[indexToReplace].GetType(), resultCommands[indexToReplace].GetType());

            var command = resultCommands[indexToReplace] as Plus;
            var validNames = new[] { "zero", "one", "two" };
            var validFlag = validNames.Contains(command.FirstSource) &&
                            validNames.Contains(command.SecondSource) &&
                            validNames.Contains(command.TargetName);
            Assert.IsTrue(validFlag);
        }

        [TestMethod]
        public void ReplaceMinus()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("one = 1")
                    .AppendLine("int two")
                    .AppendLine("zero = 0")
                    .AppendLine("two = zero - one")
                    .AppendLine("int four");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 5;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);

            for (int i = 0; i < commands.Length - 2; i++)
            {
                Assert.IsTrue(_comparer.IsEqual(commands[i], resultCommands[i]));
            }
            Assert.IsTrue(_comparer.IsEqual(commands[commands.Length - 1], resultCommands[commands.Length - 1]));
            Assert.AreEqual(commands[indexToReplace].GetType(), resultCommands[indexToReplace].GetType());

            var command = resultCommands[indexToReplace] as Minus;
            var validNames = new[] { "zero", "one", "two" };
            var validFlag = validNames.Contains(command.FirstSource) &&
                            validNames.Contains(command.SecondSource) &&
                            validNames.Contains(command.TargetName);
            Assert.IsTrue(validFlag);
        }

        [TestMethod]
        public void ReplacePrint()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("one = 1")
                    .AppendLine("zero = 0")
                    .AppendLine("print zero")
                    .AppendLine("int four")
                    .AppendLine("int three");

            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 4;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);
            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(_comparer.IsEqual(commands[i], resultCommands[i]));
            }
            Assert.IsTrue(_comparer.IsEqual(commands[5], resultCommands[5]));
            Assert.IsTrue(_comparer.IsEqual(commands[6], resultCommands[6]));
            Assert.AreEqual(commands[indexToReplace].GetType(), commands[indexToReplace].GetType());

            var command = resultCommands[indexToReplace] as Print;
            var validNames = new[] { "zero", "one" };
            var validFlag = validNames.Contains(command.Variable);
            Assert.IsTrue(validFlag);
        }

        [TestMethod]
        public void ReplaceCloneValue()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("one = 1")
                    .AppendLine("int four")
                    .AppendLine("zero = one");

            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 4;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);
            for (int i = 0; i < commands.Length-1; i++)
            {
                Assert.IsTrue(_comparer.IsEqual(commands[i],resultCommands[i]));
            }
            Assert.AreEqual(commands[indexToReplace].GetType(), resultCommands[indexToReplace].GetType());

            var command = resultCommands[indexToReplace] as CloneValue;
            var validNames = new[] {"zero", "one", "four"};
            var validFlag = validNames.Contains(command.SourceName) && validNames.Contains(command.TargetName);
            Assert.IsTrue(validFlag);
        }

        [TestMethod]
        public void ReplaceCondition()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("zero = 0")
                    .AppendLine("if zero then")
                    .AppendLine("int four")
                    .AppendLine("endif");

            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 3;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);
            for (int i = 0; i < resultCommands.Length; i++)
            {
                if (i != indexToReplace)
                {
                    Assert.IsTrue(_comparer.IsEqual(commands[i], resultCommands[i]));
                }
            }
            Assert.AreEqual(commands[indexToReplace].GetType(), resultCommands[indexToReplace].GetType());
            var validNames = new[] {"zero", "one"};
            var command = resultCommands[indexToReplace] as Condition;
            var validFlag = validNames.Contains(command.ConditionName);
            Assert.IsTrue(validFlag);
        }

        [TestMethod]
        public void ReplaceCloseCondition()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("zero = 0")
                    .AppendLine("if zero then")
                    .AppendLine("int four")
                    .AppendLine("endif");

            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 5;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);
            for (int i = 0; i < commands.Length; i++)
            {
                Assert.IsTrue(_comparer.IsEqual(commands[i], resultCommands[i]));
            }
        }

        [TestMethod]
        public void ReplaceStop()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("zero = 0")
                    .AppendLine("if zero then")
                    .AppendLine("int four")
                    .AppendLine("stop")
                    .AppendLine("endif");

            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexToReplace = 5;

            var resultCommands = new ReplaceCommandMutation(new SpecialRandom(indexToReplace)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length);
            for (int i = 0; i < commands.Length; i++)
            {
                Assert.IsTrue(_comparer.IsEqual(commands[i], resultCommands[i]));
            }
        }

    }
}
