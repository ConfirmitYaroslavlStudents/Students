using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Creaturestests;

namespace CreaturesTests
{
    [TestClass]
    public class LanguageTests : TestsBase
    {
        [TestMethod]
        public void SetValue()
        {
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a=1")
                   .AppendLine("print a");

            Check(commands.ToString(), 1);
        }

        [TestMethod]
        public void Plus()
        {
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a=1")
                   .AppendLine("int b")
                   .AppendLine("b=2")
                   .AppendLine("a=a+b")
                   .AppendLine("print a");

            Check(commands.ToString(), 3);
        }

        [TestMethod]
        public void Minus()
        {
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a=1")
                   .AppendLine("int b")
                   .AppendLine("b=2")
                   .AppendLine("a=a-b")
                   .AppendLine("print a");

            Check(commands.ToString(), -1);
        }

        [TestMethod]
        public void CloneValue()
        {
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a=1")
                   .AppendLine("int b")
                   .AppendLine("b=a")
                   .AppendLine("print b");

            Check(commands.ToString(), 1);
        }

        [TestMethod]
        public void TrueCondition()
        {
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a=1")
                   .AppendLine("if a then")
                   .AppendLine("print a")
                   .AppendLine("endif")
                   .AppendLine("print a");

            Check(commands.ToString(), 1, 1);
        }

        [TestMethod]
        public void FalseCondition()
        {
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a=1")
                   .AppendLine("int b")
                   .AppendLine("b=2")
                   .AppendLine("a=a-b")
                   .AppendLine("if a then")
                   .AppendLine("print a")
                   .AppendLine("endif")
                   .AppendLine("print a");

            Check(commands.ToString(), -1);
        }

        [TestMethod]
        public void EmbeddedCondition()
        {
            var commands =
               new StringBuilder()
                   .AppendLine("int a")
                   .AppendLine("a=1")
                   .AppendLine("int b")
                   .AppendLine("b=2")
                   .AppendLine("int c")
                   .AppendLine("c=a-b")
                   .AppendLine("if a then")
                   
                   .AppendLine("if b then")
                   .AppendLine("print b")
                   .AppendLine("endif")

                   .AppendLine("if c then")
                   .AppendLine("print c")
                   .AppendLine("endif")

                   .AppendLine("print a")
                   .AppendLine("endif")
                   .AppendLine("print a");

            Check(commands.ToString(), 2, 1, 1);
        }
    }
}
