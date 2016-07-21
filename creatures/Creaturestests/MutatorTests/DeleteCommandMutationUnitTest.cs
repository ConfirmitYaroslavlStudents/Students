using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellsAutomate;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class DeleteCommandMutationUnitTest
    {
        [TestMethod]
        public void DeleteIfCommand()
        {
            var precommands =
                new StringBuilder()
                    .AppendLine("int zero")
                    .AppendLine("zero = 0")
                    .AppendLine("if zero then")
                    .AppendLine("stop")
                    .AppendLine("endif");
            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(2)).Transform(commands);

            Assert.AreEqual(newCommands.Length, 3);

            Assert.AreEqual((newCommands[0] as NewInt).Name,"zero");
            Assert.AreEqual((newCommands[1] as SetValue).TargetName, "zero");
            Assert.IsTrue(newCommands[2] is Stop);

        }

        [TestMethod]
        public void DeleteEndIfCommand()
        {
            var precommands =
               new StringBuilder()
                   // зададим константы
                   .AppendLine("int zero")
                   .AppendLine("zero = 0")
                   .AppendLine("if zero then")
                   .AppendLine("stop")
                   .AppendLine("endif");
            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(4)).Transform(commands);

            Assert.AreEqual(newCommands.Length, 3);

            Assert.AreEqual((newCommands[0] as NewInt).Name, "zero");
            Assert.AreEqual((newCommands[1] as SetValue).TargetName, "zero");
            Assert.IsTrue(newCommands[2] is Stop);
        }

        [TestMethod]
        public void DeleteIfWithNestedIf()
        {
            var precommands =
               new StringBuilder()
                   // зададим константы
                   .AppendLine("int zero")
                   .AppendLine("int one")
                   .AppendLine("one = 1")
                   .AppendLine("int two")
                   .AppendLine("zero = 0")
                   .AppendLine("two = 2")
                   .AppendLine("if zero then")
                   .AppendLine("one = 1")
                   .AppendLine("if one then")
                   .AppendLine("if two then")
                   .AppendLine("endif")
                   .AppendLine("stop")
                   .AppendLine("endif")
                   .AppendLine("endif");

            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(8)).Transform(commands);

            Assert.AreEqual(newCommands.Length,12);
            Assert.AreEqual((newCommands[6] as Condition).ConditionName,"zero");
            Assert.AreEqual((newCommands[7] as SetValue).TargetName,"one");
            Assert.AreEqual((newCommands[8] as Condition).ConditionName, "two");
            Assert.IsTrue(newCommands[9] is CloseCondition);
            Assert.IsTrue(newCommands[10] is Stop);
            Assert.IsTrue(newCommands[11] is CloseCondition);
        }

        [TestMethod]
        public void DeleteEndIfWithNestedEndIf()
        {
            var precommands =
               new StringBuilder()
                   .AppendLine("int zero")
                   .AppendLine("int one")
                   .AppendLine("one = 1")
                   .AppendLine("int two")
                   .AppendLine("zero = 0")
                   .AppendLine("two = 2")
                   .AppendLine("if zero then")
                   .AppendLine("one = 1")
                   .AppendLine("if one then")
                   .AppendLine("if two then")
                   .AppendLine("endif")
                   .AppendLine("stop")
                   .AppendLine("endif")
                   .AppendLine("endif");

            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(12)).Transform(commands);

            Assert.AreEqual(newCommands.Length, 12);
            Assert.AreEqual((newCommands[6] as Condition).ConditionName, "zero");
            Assert.AreEqual((newCommands[7] as SetValue).TargetName, "one");
            Assert.AreEqual((newCommands[8] as Condition).ConditionName, "two");
            Assert.IsTrue(newCommands[9] is CloseCondition);
            Assert.IsTrue(newCommands[10] is Stop);
            Assert.IsTrue(newCommands[11] is CloseCondition);
        }

        [TestMethod]
        public void DeleteDeclarationCommand()
        {
            var precommands =
               new StringBuilder()
                   // зададим константы
                   .AppendLine("int zero")
                   .AppendLine("int one")
                   .AppendLine("one = 1")
                   .AppendLine("zero = 0")
                   .AppendLine("if zero then")
                   .AppendLine("one = 1")
                   .AppendLine("if one then")
                   .AppendLine("stop")
                   .AppendLine("endif")
                   .AppendLine("endif");

            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(1)).Transform(commands);
            Assert.AreEqual(newCommands.Length,5);

            Assert.AreEqual((newCommands[0] as NewInt).Name,"zero");
            Assert.AreEqual((newCommands[1] as SetValue).TargetName,"zero");
            Assert.AreEqual((newCommands[2] as Condition).ConditionName, "zero");
            Assert.IsTrue(newCommands[3] is Stop);
            Assert.IsTrue(newCommands[4] is CloseCondition);

        }

        [TestMethod]
        public void DeleteSetterShouldNotDeleteDeclaration()
        {
            var precommands =
                new StringBuilder()
                    // зададим константы
                    .AppendLine("int zero")
                    .AppendLine("int one")
                    .AppendLine("one = 1")
                    .AppendLine("zero = 0")
                    .AppendLine("if zero then")
                    .AppendLine("stop")
                    .AppendLine("endif");
            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(2)).Transform(commands);
            Assert.AreEqual(newCommands.Length,6);
            Assert.AreEqual((newCommands[1] as NewInt).Name, "one");
            Assert.AreEqual((newCommands[2] as SetValue).TargetName, "zero");
        }

        [TestMethod]
        public void DeleteNotInitializationSetter()
        {
            var precommands =
                new StringBuilder()
                    // зададим константы
                    .AppendLine("int zero")
                    .AppendLine("zero = 0")
                    .AppendLine("zero = 0")
                    .AppendLine("if zero then")
                    .AppendLine("stop")
                    .AppendLine("endif");
            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(2)).Transform(commands);
            Assert.AreEqual(newCommands.Length, 5);
            Assert.AreEqual((newCommands[2] as Condition).ConditionName, "zero");
            Assert.AreEqual((newCommands[1] as SetValue).TargetName,"zero");
        }

        [TestMethod]
        public void DeleteInitializationSetter()
        {
            var precommands =
                new StringBuilder()
                    // зададим константы
                    .AppendLine("int zero")
                    .AppendLine("zero = 0")
                    .AppendLine("if zero then")
                    .AppendLine("stop")
                    .AppendLine("endif");
            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(1)).Transform(commands);
            Assert.AreEqual(newCommands.Length, 2);
            Assert.AreEqual((newCommands[0] as NewInt).Name,"zero");
            Assert.IsTrue(newCommands[1] is Stop);
        }

        [TestMethod]
        public void DeleteCloneValueSetterNotInit()
        {
            var precommands =
                new StringBuilder()
                    // зададим константы
                    .AppendLine("int zero")
                    .AppendLine("one = 1")
                    .AppendLine("zero = one")
                    .AppendLine("if zero then")
                    .AppendLine("stop")
                    .AppendLine("endif");

            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(1)).Transform(commands);
            Assert.AreEqual(newCommands.Length, 2);
            Assert.AreEqual((newCommands[0] as NewInt).Name, "zero");
            Assert.IsTrue(newCommands[1] is Stop);
        }

        [TestMethod]
        public void DeleteStop()
        {
            var precommands =
                new StringBuilder()
                    // зададим константы
                    .AppendLine("int zero")
                    .AppendLine("zero = 0")
                    .AppendLine("stop");

            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(2)).Transform(commands);
            Assert.AreEqual(newCommands.Length, 2);
            Assert.AreEqual((newCommands[0] as NewInt).Name, "zero");
            Assert.AreEqual((newCommands[1] as SetValue).TargetName,"zero");

        }

        [TestMethod]
        public void DeletePrint()
        {
            var precommands =
                new StringBuilder()
                    // зададим константы
                    .AppendLine("int zero")
                    .AppendLine("zero = 0")
                    .AppendLine("print zero");

            var commands = new Parser().ProcessCommands(precommands.ToString()).ToArray();
            var newCommands = new DeleteCommandMutation(new SpecialRandom(2)).Transform(commands);
            Assert.AreEqual(newCommands.Length, 2);
            Assert.AreEqual((newCommands[0] as NewInt).Name, "zero");
            Assert.AreEqual((newCommands[1] as SetValue).TargetName, "zero");
        }

    }
}
