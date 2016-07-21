using System;
using System.Text;
using Creatures.Language.Executors;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class ValidationExecutorTest
    {
        [TestMethod]
        public void DeclareFewInt()
        {
            var executor = new ValidationExecutor();
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("int a");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void PrintNotInitInt()
        {
            var executor = new ValidationExecutor();
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("print a");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands,new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void PrintInitInt()
        {
            var executor = new ValidationExecutor();
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a = 1")
                   .AppendLine("print a");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsTrue(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void PrintNotDeclareInt()
        {
            var executor = new ValidationExecutor();
            var commands =
               new StringBuilder()
                   .AppendLine("int b")
                   .AppendLine("b = 1")
                   .AppendLine("print a");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void ConditionIgnoreTest()
        {
            var executor = new ValidationExecutor();
            var commands =
               new StringBuilder()
                   .AppendLine("int b")
                   .AppendLine("b = -1")
                   .AppendLine("if b then")
                   .AppendLine("print a")
                   .AppendLine("endif");
            

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void SkippedCloseCondition()
        {
            var executor = new ValidationExecutor();
            var commands =
               new StringBuilder()
                   .AppendLine("int b")
                   .AppendLine("b = 1")
                   .AppendLine("if b then")
                   .AppendLine("if b then")
                   .AppendLine("print b")
                   .AppendLine("endif");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void StopIgnoreTest()
        {
            var executor = new ValidationExecutor();
            var commands =
               new StringBuilder()
                   .AppendLine("int b")
                   .AppendLine("b = 1")
                   .AppendLine("if b then")
                   .AppendLine("stop")
                   .AppendLine("endif")
                   .AppendLine("print a");


            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void InitNotDeclaredInt()
        {
            var executor = new ValidationExecutor();
            var commands =
                new StringBuilder()
                    .AppendLine("b = 1")
                    .AppendLine("if b then")
                    .AppendLine("stop")
                    .AppendLine("endif");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void MinusUsingNotInitInt()
        {
            var executor = new ValidationExecutor();
            var commands =
                new StringBuilder()
                    .AppendLine("int b")
                    .AppendLine("b = 1")
                    .AppendLine("int a")
                    .AppendLine("int c")
                    .AppendLine("c=a-b");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void MinusInitNotDeclaredInt()
        {
            var executor = new ValidationExecutor();
            var commands =
                new StringBuilder()
                    .AppendLine("int b")
                    .AppendLine("b = 1")
                    .AppendLine("int a")
                    .AppendLine("a = 1")
                    .AppendLine("c=a-b");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void PlusUsingNotInitInt()
        {
            var executor = new ValidationExecutor();
            var commands =
                new StringBuilder()
                    .AppendLine("int b")
                    .AppendLine("b = 1")
                    .AppendLine("int a")
                    .AppendLine("int c")
                    .AppendLine("c=a+b");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void PlusInitNotDeclaredInt()
        {
            var executor = new ValidationExecutor();
            var commands =
                new StringBuilder()
                    .AppendLine("int b")
                    .AppendLine("b = 1")
                    .AppendLine("int a")
                    .AppendLine("a = 1")
                    .AppendLine("c=a+b");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }

        [TestMethod]
        public void SkippedCondtition()
        {
            var executor = new ValidationExecutor();
            var commands =
                new StringBuilder()
                    .AppendLine("b = 1")
                    .AppendLine("if b then")
                    .AppendLine("stop")
                    .AppendLine("endif")
                    .AppendLine("endif");

            var parsedcommands = new Parser().ProcessCommands(commands.ToString());
            Assert.IsFalse(executor.Execute(parsedcommands, new ExecutorToolset(new Random())));
        }
    }
}