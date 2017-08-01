using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;
using Mp3UtilLib.Arguments;
using Mp3UtilTests.Helpers;
using System.Linq;

namespace Mp3UtilTests
{
    [TestClass]
    public class ProcessorTest
    {
        [TestMethod]
        public void ExecuteWithToFileNameAction()
        {
            List<string> expectedFiles = new List<string>
            {
                @"Bullet For My Valentine - Cries in Vain.mp3",
                @"Anne-Marie - Ciao Adios.mp3"
            };

            MockFileSystem mockFileSystem = new MockFileSystem();
            mockFileSystem.Add("file1", new TestableMp3File("file1", mockFileSystem)
                {Artist = "Bullet For My Valentine", Title = "Cries in Vain"});
            mockFileSystem.Add("file2", new TestableMp3File("file2", mockFileSystem)
                {Artist = "Anne-Marie", Title = "Ciao Adios"});

            Args args = new Args("*.*", true, ProgramAction.ToFileName);

            Processor processor = new Processor(args, mockFileSystem);
            processor.Execute();

            Assert.AreEqual(0, mockFileSystem.AllFiles.Except(expectedFiles).Count());
        }

        [TestMethod]
        public void ExecuteWithToTagAction()
        {
            List<string[]> expectedValues = new List<string[]>
            {
                new[] {"Bullet For My Valentine", "Cries in Vain"},
                new[] {"Ciao Adios", "Anne-Mari"}
            };

            MockFileSystem mockFileSystem = new MockFileSystem();
            mockFileSystem.Add("Bullet For My Valentine - Cries in Vain.mp3",
                new TestableMp3File("Bullet For My Valentine - Cries in Vain.mp3", mockFileSystem));
            mockFileSystem.Add("Ciao Adios - Anne-Mari.mp3",
                new TestableMp3File("Ciao Adios - Anne-Mari.mp3", mockFileSystem));

            Args args = new Args("*.*", true, ProgramAction.ToTag);

            Processor processor = new Processor(args, mockFileSystem);
            processor.Execute();

            int i = 0;
            foreach (var audioFile in 
                mockFileSystem.GetAudioFilesFromCurrentDirectory("", false))
            {
                TestableMp3File mp3File = audioFile as TestableMp3File;
                Assert.AreEqual(expectedValues[i][0], mp3File.Artist);
                Assert.AreEqual(expectedValues[i][1], mp3File.Title);
                Assert.AreEqual(true, mp3File.Saved);
                i++;
            }
        }
    }
}