using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;
using Mp3UtilLib.Arguments;

namespace Mp3UtilTests
{
    [TestClass]
    public class ArgsTest
    {
        [TestMethod]
        public void ParsingArguments()
        {
            string[] args = { "*.*", "-recursive", "-toTag" };
            Args parsedArgs = ArgumentsParser.Parse(args);

            Assert.AreEqual("*.*", parsedArgs.Mask);
            Assert.AreEqual(true, parsedArgs.Recursive);
            Assert.AreEqual(ProgramAction.ToTag, parsedArgs.Action);
        }

        [TestMethod]
        public void ParsingArgumentsWithDefaultValues()
        {
            string[] args = { "*.*" };
            Args parsedArgs = ArgumentsParser.Parse(args);

            Assert.AreEqual("*.*", parsedArgs.Mask);
            Assert.AreEqual(false, parsedArgs.Recursive);
            Assert.AreEqual(ProgramAction.ToFileName, parsedArgs.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParsingEmptyArguments()
        {
            string[] args = new string[0];
            ArgumentsParser.Parse(args);
        }

        [TestMethod]
        public void ParsingArgumentsWithMultiplyActionParam()
        {
            string[] args = { "*.*", "-toTag", "-toTag", "-toFileName", "-toTag", "-toFileName" };
            Args parsedArgs = ArgumentsParser.Parse(args);

            Assert.AreEqual("*.*", parsedArgs.Mask);
            Assert.AreEqual(false, parsedArgs.Recursive);
            Assert.AreEqual(ProgramAction.ToFileName, parsedArgs.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParsingWithWrongArgument()
        {
            string[] args = { "*.*", "-recursive", "-wrongArgument" };
            ArgumentsParser.Parse(args);
        }
    }
}