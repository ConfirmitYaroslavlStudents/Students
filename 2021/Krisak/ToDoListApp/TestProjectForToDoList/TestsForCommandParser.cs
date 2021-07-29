using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;

namespace TestsForToDoList
{
    [TestClass]
    public class TestsForCommandParser
    {
        [TestMethod]
        public void CorrectParseCommandAddOneWord()
        {
            var command = new string[] { "add", "world" };
            var result = CommandParser.ParseCommandAdd(command);
            Assert.AreEqual((string)result[0],"add");
            Assert.AreEqual((string)result[1], "world");
        }

        [TestMethod]
        public void CorrectParseCommandAddSeveralWords()
        {
            var command = new string[] { "add", "world", "or", "war" };
            var result = CommandParser.ParseCommandAdd(command);
            Assert.AreEqual((string)result[0], "add");
            Assert.AreEqual((string)result[1], "world or war");
        }

        [TestMethod]
        public void CorrectParseCommandEditOneWord()
        {
            var command = new string[] { "edit", "1", "world" };
            var result = CommandParser.ParseCommandEdit(command);
            Assert.AreEqual((string)result[0], "edit");
            Assert.AreEqual((int)result[1], 0);
            Assert.AreEqual((string)result[2], "world");
        }

        [TestMethod]
        public void CorrectParseCommandEditSeveralWords()
        {
            var command = new string[] { "edit", "1", "world","or","war" };
            var result = CommandParser.ParseCommandEdit(command);
            Assert.AreEqual((string)result[0], "edit");
            Assert.AreEqual((int)result[1], 0);
            Assert.AreEqual((string)result[2], "world or war");
        }

        [TestMethod]
        public void CorrectParseCommandToggle()
        {
            var command = new string[] { "toggle", "1" };
            var result = CommandParser.ParseCommandToggle(command);
            Assert.AreEqual((string)result[0], "toggle");
            Assert.AreEqual((int)result[1], 0);
        }

        [TestMethod]
        public void CorrectParseCommandDelete()
        {
            var command = new string[] { "delete", "1" };
            var result = CommandParser.ParseCommandDelete(command);
            Assert.AreEqual((string)result[0], "delete");
            Assert.AreEqual((int)result[1], 0);
        }

        [TestMethod]
        public void CorrectParseCommandRollback()
        {
            var command = new string[] { "rollback", "1" };
            var result = CommandParser.ParseCommandRollback(command);
            Assert.AreEqual((string)result[0], "rollback");
            Assert.AreEqual((int)result[1], 1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandEdit()
        {
            var command = new string[] { "edit", "odin" };
            CommandParser.ParseCommandEdit(command);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandToggle()
        {
            var command = new string[] { "toggle", "odin" };
            CommandParser.ParseCommandToggle(command);
        }
        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandDelete()
        {
            var command = new string[] { "delete", "odin" };
            CommandParser.ParseCommandDelete(command);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandRollback()
        {
            var command = new string[] { "rollback", "odin" };
            CommandParser.ParseCommandRollback(command);
        }
    }
}
