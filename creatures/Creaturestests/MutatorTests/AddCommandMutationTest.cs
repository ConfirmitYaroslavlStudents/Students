using System.Linq;
using System.Text;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class AddCommandMutationTest
    {
        private CommandsEqualityComparer _comparer = new CommandsEqualityComparer();

        [TestMethod]
        public void AddNewInt()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("zero = 0");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexInAllTypes = 0;
            int indexToAdd = 2;

            var resultCommands = new AddCommandMutation(new SpecialRandom(indexInAllTypes, indexToAdd)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length + 1);
            for (int i = 0, j = 0; i < resultCommands.Length; i++)
            {
                if (i != indexToAdd)
                {
                    Assert.IsTrue(_comparer.IsEqual(commands[j], resultCommands[i]));
                    j++;
                }
            }
            var command = resultCommands[indexToAdd] as NewInt;
            Assert.IsNotNull(command);
            Assert.IsFalse(string.IsNullOrEmpty(command.Name));
        }

        [TestMethod]
        public void AddSetValue()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("int two")
                    .AppendLine("int four")
                    .AppendLine("zero = 0");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexInAllTypes = 9;
            int indexToAdd = 2;

            var resultCommands = new AddCommandMutation(new SpecialRandom(indexInAllTypes, indexToAdd)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length + 1);
            for (int i = 0, j = 0; i < resultCommands.Length; i++)
            {
                if (i != indexToAdd)
                {
                    Assert.IsTrue(_comparer.IsEqual(commands[j], resultCommands[i]));
                    j++;
                }
            }
            var command = resultCommands[indexToAdd] as SetValue;
            Assert.IsNotNull(command);
            var validNames = new[] { "zero", "one" };
            var validFlag = validNames.Contains(command.TargetName);
            Assert.IsTrue(validFlag);
        }

        [TestMethod]
        public void AddValidPlus()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("int two")
                    .AppendLine("int four");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexInAllTypes = 7;
            int indexToAdd = 4;
            var resultCommands = new AddCommandMutation(new SpecialRandom(indexInAllTypes, indexToAdd)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length + 1);
            for (int i = 0, j = 0; i < resultCommands.Length; i++)
            {
                if (i != indexToAdd)
                {
                    Assert.IsTrue(_comparer.IsEqual(commands[j], resultCommands[i]));
                    j++;
                }
            }
            var command = resultCommands[indexToAdd] as Plus;
            var validNames = new[] { "zero", "one", "two" };
            var validFlag = validNames.Contains(command.FirstSource) &&
                            validNames.Contains(command.SecondSource) &&
                            validNames.Contains(command.TargetName);
            Assert.IsTrue(validFlag);
        }

        [TestMethod]
        public void AddInvalidPlus()
        {
            var code =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("int two")
                    .AppendLine("int four");
            var commands = new Parser().ProcessCommands(code.ToString()).ToArray();
            int indexInAllTypes = 7;
            int indexToAdd = 0;
            var resultCommands = new AddCommandMutation(new SpecialRandom(indexInAllTypes, indexToAdd)).Transform(commands);
            Assert.AreEqual(resultCommands.Length, commands.Length + 1);
            for (int i = 0, j = 0; i < resultCommands.Length; i++)
            {
                if (i != indexToAdd)
                {
                    Assert.IsTrue(_comparer.IsEqual(commands[j], resultCommands[i]));
                    j++;
                }
            }
            var command = resultCommands[indexToAdd] as Plus;
            var validNames = new[] { "zero", "one", "two", "four" };
            var validFlag = !validNames.Contains(command.FirstSource) &&
                            !validNames.Contains(command.SecondSource) &&
                            !validNames.Contains(command.TargetName);
            Assert.IsTrue(validFlag);
        }
    }
}