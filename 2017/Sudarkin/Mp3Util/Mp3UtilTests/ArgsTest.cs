using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilConsole;
using Mp3UtilConsole.Arguments;

namespace Mp3UtilTests
{
    [TestClass]
    public class ArgsTest
    {
        [TestMethod]
        public void ParsingArguments()
        {
            string[] args = { "*.*", "-recursive", "-toTag" };
            Args parsedArgs = ArgumentsManager.Parse(args);

            Assert.AreEqual("*.*", parsedArgs.Mask);
            Assert.AreEqual(true, parsedArgs.Recursive);
            Assert.AreEqual(ProgramAction.ToTag, parsedArgs.Action);
        }

        [TestMethod]
        public void ParsingArgumentsWithDefaultValues()
        {
            string[] args = { "*.*" };
            Args parsedArgs = ArgumentsManager.Parse(args);

            Assert.AreEqual("*.*", parsedArgs.Mask);
            Assert.AreEqual(false, parsedArgs.Recursive);
            Assert.AreEqual(ProgramAction.ToFileName, parsedArgs.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParsingEmptyArguments()
        {
            string[] args = new string[0];
            ArgumentsManager.Parse(args);
        }

        [TestMethod]
        public void ParsingArgumentsWithMultiplyActionParam()
        {
            string[] args = { "*.*", "-toTag", "-toTag", "-toFileName", "-toTag", "-toFileName" };
            Args parsedArgs = ArgumentsManager.Parse(args);

            Assert.AreEqual("*.*", parsedArgs.Mask);
            Assert.AreEqual(false, parsedArgs.Recursive);
            Assert.AreEqual(ProgramAction.ToFileName, parsedArgs.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParsingWithWrongArgument()
        {
            string[] args = { "*.*", "-recursive", "-wrongArgument" };
            ArgumentsManager.Parse(args);
        }
    }
}