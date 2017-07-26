using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MP3RenamerTimeMeasure.Test
{
    [TestClass]
    public class ProcessTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProcessWithNullFiles()
        {
            string[] args = null;
            MP3File[] files = null;

            Processor processor = new Processor();
            processor.Process(args, files);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProcessWithIncorrectArguments()
        {
            string[] args = new string[] {"-argument"};
            MP3File[] files = new MP3File[] { new MP3File("test.mp3") };

            Processor processor = new Processor();
            processor.Process(args, files);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ProcessWithNullArguments()
        {
            string[] args = null;
            MP3File[] files = new MP3File[] { new MP3File("test.mp3") };

            Processor processor = new Processor();
            processor.Process(args, files);
        }

        [TestMethod]
        public void ProcessWithNoArguments()
        {
            string[] args = new string[] {"-normal"};
            MP3File[] files = new MP3File[] { new MP3File("test.mp3") };

            Processor processor = new Processor();
            processor.Process(args, files);

            Assert.AreEqual("test_(new).mp3", files[0].Path);
        }

        [TestMethod]
        public void ProcessWithUserPermitions()
        {
            string[] args = new string[] { "-permitions" };
            MP3File[] files = new MP3File[] { new MP3File("test.mp3") };
            files[0].FilePermitions = Permitions.Administrator;

            Processor processor = new Processor();
            processor.Process(args, files);

            Assert.AreEqual("test.mp3", files[0].Path);
        }

        [TestMethod]
        public void ProcessWithTimer()
        {
            string[] args = new string[] { "-timer" };
            MP3File[] files = new MP3File[] { new MP3File("test.mp3") };
            files[0].FilePermitions = Permitions.Administrator;

            Processor processor = new Processor();
            processor.Process(args, files);

            Assert.AreEqual("test_(new).mp3", files[0].Path);
        }
    }
}
