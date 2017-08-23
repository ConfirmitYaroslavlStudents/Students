using RenamerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamerLib.Arguments;

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
                new MockMp3File("file1.mp3", fileSystem) { Artist = "Bon Jovi", Title = "It's my life" });
            fileSystem.AddFile("file2.mp3", 
                new MockMp3File("file2.mp3", fileSystem) { Artist = "Imagine Dragons", Title = "Believer" });

            RenamerArguments renamerArguments = new RenamerArguments
            {
                IsRecursive = false,
                Action = AllowedActions.ToFileName,
                Mask = "*.mp3"
            };

            Processor processor = new Processor(renamerArguments, fileSystem);
            processor.Process();

            Assert.IsTrue(fileSystem.Exist("\\Bon Jovi - It's my life.mp3"));
            Assert.IsTrue(fileSystem.Exist("\\Imagine Dragons - Believer.mp3"));

            Assert.AreEqual(2, fileSystem.Files.Count);
        }

        [TestMethod]
        public void ExecuteToTag()
        {
            MockFileManager fileSystem = new MockFileManager();
            fileSystem.AddFile("Bon Jovi - It's my life.mp3",
                new MockMp3File("Bon Jovi - It's my life.mp3", fileSystem));
            fileSystem.AddFile("Imagine Dragons - Believer.mp3",
                new MockMp3File("Imagine Dragons - Believer.mp3", fileSystem));

            RenamerArguments renamerArguments = new RenamerArguments
            {
                IsRecursive = false,
                Action = AllowedActions.ToTag,
                Mask = "*.mp3"
            };

            Processor processor = new Processor(renamerArguments, fileSystem);
            processor.Process();

            Assert.AreEqual("Bon Jovi", fileSystem.Files["Bon Jovi - It's my life.mp3"].Artist);
            Assert.AreEqual("It's my life", fileSystem.Files["Bon Jovi - It's my life.mp3"].Title);

            Assert.AreEqual("Imagine Dragons", fileSystem.Files["Imagine Dragons - Believer.mp3"].Artist);
            Assert.AreEqual("Believer", fileSystem.Files["Imagine Dragons - Believer.mp3"].Title);

            Assert.AreEqual(2, fileSystem.Files.Count);
        }
    }
}
