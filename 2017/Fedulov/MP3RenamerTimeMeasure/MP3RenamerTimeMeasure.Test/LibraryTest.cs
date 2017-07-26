using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3RenamerLib;

namespace MP3RenamerTimeMeasure.Test
{
    [TestClass]
    public class MP3FileTest
    {
        [TestMethod]
        public void CreateFile()
        {
            MP3File file = new MP3File("test.mp3", Permitions.Administrator);

            Assert.AreEqual("test.mp3", file.Path);
            Assert.AreEqual(Permitions.Administrator, file.FilePermitions);
        }

        [TestMethod]
        public void FileMove()
        {
            MP3File file = new MP3File("test.mp3", Permitions.Administrator);

            file.Move("test_(new).mp3");

            Assert.AreNotEqual("test.mp3", file.Path);
            Assert.AreEqual("test_(new).mp3", file.Path);
        }

        [TestMethod]
        public void FileChangePermitions()
        {
            MP3File file = new MP3File("test.mp3", Permitions.Administrator);

            file.FilePermitions = Permitions.Guest;

            Assert.AreNotEqual(Permitions.Administrator, file.FilePermitions);
            Assert.AreEqual(Permitions.Guest, file.FilePermitions);
        }
    }

    [TestClass]
    public class PermitionsTest
    {
        [TestMethod]
        public void CheckPermitionsAdministrator()
        {
            MP3File file1 = new MP3File("temp1.mp3", Permitions.Guest);
            MP3File file2 = new MP3File("temp2.mp3", Permitions.User);
            MP3File file3 = new MP3File("temp3.mp3", Permitions.Administrator);

            PermitionsChecker permitionsChecker = new PermitionsChecker();

            Assert.IsTrue(permitionsChecker.Check(file1, Permitions.Administrator));
            Assert.IsTrue(permitionsChecker.Check(file2, Permitions.Administrator));
            Assert.IsTrue(permitionsChecker.Check(file3, Permitions.Administrator));
        }

        [TestMethod]
        public void CheckPermitionsUser()
        {
            MP3File file1 = new MP3File("temp1.mp3", Permitions.Guest);
            MP3File file2 = new MP3File("temp2.mp3", Permitions.User);
            MP3File file3 = new MP3File("temp3.mp3", Permitions.Administrator);

            PermitionsChecker permitionsChecker = new PermitionsChecker();

            Assert.IsTrue(permitionsChecker.Check(file1, Permitions.User));
            Assert.IsTrue(permitionsChecker.Check(file2, Permitions.User));
            Assert.IsFalse(permitionsChecker.Check(file3, Permitions.User));
        }

        [TestMethod]
        public void CheckPermitionsGuest()
        {
            MP3File file1 = new MP3File("temp1.mp3", Permitions.Guest);
            MP3File file2 = new MP3File("temp2.mp3", Permitions.User);
            MP3File file3 = new MP3File("temp3.mp3", Permitions.Administrator);

            PermitionsChecker permitionsChecker = new PermitionsChecker();

            Assert.IsTrue(permitionsChecker.Check(file1, Permitions.Guest));
            Assert.IsFalse(permitionsChecker.Check(file2, Permitions.Guest));
            Assert.IsFalse(permitionsChecker.Check(file3, Permitions.Guest));
        }
    }

    [TestClass]
    public class ArgumentsParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ParseNullArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            parser.ParseArguments(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseUnknownArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            parser.ParseArguments(new string[] {"-fake"});
        }

        [TestMethod]
        public void ParseArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            string[] args = new string[] { "-timer", "-permitions", "-normal" };

            Arguments arguments = parser.ParseArguments(args);

            Assert.IsTrue(arguments.IsTimeMeasure);
            Assert.IsTrue(arguments.IsCheckPermitions);
            Assert.IsTrue(arguments.IsNormalExecution);
        }
    }

    [TestClass]
    public class FileRenamerTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SimpleRenamerWithNullFile()
        {
            FileRenamer renamer = new FileRenamer();
            renamer.Rename(null);
        }

        [TestMethod]
        public void SimpleRenamer()
        {
            FileRenamer renamer = new FileRenamer();
            MP3File file = new MP3File("test.mp3");

            renamer.Rename(file);

            Assert.AreEqual("test_(new).mp3", file.Path);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void PermitionsCheckerRenamerWithNullFile()
        {
            FileRenamer baseRenamer = new FileRenamer();
            FileRenamerPermitionsChecker renamer = 
                new FileRenamerPermitionsChecker(baseRenamer, new PermitionsChecker());
            renamer.Rename(null);
        }

        [TestMethod]
        public void PermitionsCheckerRenamerSuccessful()
        {
            FileRenamer baseRenamer = new FileRenamer();
            FileRenamerPermitionsChecker renamer = new FileRenamerPermitionsChecker(baseRenamer, 
                new PermitionsChecker(), Permitions.Administrator);
            MP3File file = new MP3File("test.mp3");
            file.FilePermitions = Permitions.Guest;

            renamer.Rename(file);

            Assert.AreEqual("test_(new).mp3", file.Path);
        }

        [TestMethod]
        public void PermitionsCheckerRenamerNoRenamed()
        {
            FileRenamer baseRenamer = new FileRenamer();
            FileRenamerPermitionsChecker renamer = new FileRenamerPermitionsChecker(baseRenamer,
                new PermitionsChecker(), Permitions.User);
            MP3File file = new MP3File("test.mp3");
            file.FilePermitions = Permitions.Administrator;

            renamer.Rename(file);

            Assert.AreNotEqual("test_(new).mp3", file.Path);
            Assert.AreEqual("test.mp3", file.Path);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TimeMeasureRenamerWithNullFile()
        {
            FileRenamer baseRenamer = new FileRenamer();
            FileRenamerTimeMeasure renamer = new FileRenamerTimeMeasure(baseRenamer);
            renamer.Rename(null);
        }

        [TestMethod]
        public void TimeMeasureRenamerSuccessful()
        {
            FileRenamer baseRenamer = new FileRenamer();
            FileRenamerTimeMeasure renamer = new FileRenamerTimeMeasure(baseRenamer);
            MP3File file = new MP3File("test.mp3");

            Assert.AreEqual(new TimeSpan(0L), renamer.ElapsedTime);

            renamer.Rename(file);

            Assert.AreEqual("test_(new).mp3", file.Path);
            Assert.IsNotNull(renamer.ElapsedTime);
            Assert.AreNotEqual(new TimeSpan(0L), renamer.ElapsedTime);
        }
    }
}

