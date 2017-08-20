using System;
using System.Collections.Generic;
using System.IO;
using RenamerLib;
using System.IO.Abstractions;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MP3Renamer.Tests
{
    [TestClass]
    public class ProcessorTest
    {
        [TestMethod]
        public void ExecuteToFileName()
        {
            MockFileManager fileSystem = new MockFileManager();
            fileSystem.AddFile("file1.mp3", 
                new MockMP3File("file1.mp3", fileSystem) { Artist = "Bon Jovi", Title = "It's my life" });
            fileSystem.AddFile("file2.mp3", 
                new MockMP3File("file2.mp3", fileSystem) { Artist = "Imagine Dragons", Title = "Believer" });

            Arguments arguments = new Arguments();
            arguments.IsRecursive = false;
            arguments.Action = AllowedActions.toFileName;
            arguments.Mask = "*.mp3";

            Processor processor = new Processor(arguments, fileSystem);
            processor.Process();

            Assert.IsTrue(fileSystem.Exist("\\Bon Jovi - It's my life.mp3"));
            Assert.IsTrue(fileSystem.Exist("\\Imagine Dragons - Believer.mp3"));

            Assert.AreEqual(2, fileSystem.files.Count);
        }

        [TestMethod]
        public void ExecuteToTag()
        {
            MockFileManager fileSystem = new MockFileManager();
            fileSystem.AddFile("Bon Jovi - It's my life.mp3",
                new MockMP3File("Bon Jovi - It's my life.mp3", fileSystem));
            fileSystem.AddFile("Imagine Dragons - Believer.mp3",
                new MockMP3File("Imagine Dragons - Believer.mp3", fileSystem));

            Arguments arguments = new Arguments();
            arguments.IsRecursive = false;
            arguments.Action = AllowedActions.toTag;
            arguments.Mask = "*.mp3";

            Processor processor = new Processor(arguments, fileSystem);
            processor.Process();

            Assert.AreEqual("Bon Jovi", fileSystem.files["Bon Jovi - It's my life.mp3"].Artist);
            Assert.AreEqual("It's my life", fileSystem.files["Bon Jovi - It's my life.mp3"].Title);

            Assert.AreEqual("Imagine Dragons", fileSystem.files["Imagine Dragons - Believer.mp3"].Artist);
            Assert.AreEqual("Believer", fileSystem.files["Imagine Dragons - Believer.mp3"].Title);

            Assert.AreEqual(2, fileSystem.files.Count);
        }
    }
}
