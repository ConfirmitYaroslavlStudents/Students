using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamersLib;

namespace RenamersTests
{
    [TestClass]
    public class RenamersTests
    {
        [TestMethod]
        public void CheckRenamer()
        {
            var renamer = new FileRenamer();
            var file = new Mp3File("SampleFile.mp3");

            renamer.Rename(file);

            Assert.AreEqual("NewSampleFile.mp3", file.Path);
        }

        [TestMethod]
        public void CheckRenamerWithPermissionCheckAsGuest()
        {
            var renamer = new RenamerWithPermissionCheck(new FileRenamer(), new PermissionChecker(), UserRole.Guest);
            var file = new Mp3File("SampleFile.mp3");

            renamer.Rename(file);

            Assert.AreEqual("SampleFile.mp3", file.Path);
        }

        [TestMethod]
        public void CheckRenamerWithPermissionCheckAsUser()
        {
            var renamer = new RenamerWithPermissionCheck(new FileRenamer(), new PermissionChecker(), UserRole.User);
            var file = new Mp3File("SampleFile.mp3");

            renamer.Rename(file);

            Assert.AreEqual("NewSampleFile.mp3", file.Path);
        }

        [TestMethod]
        public void CheckRenamerWithTimeCounter()
        {
            var renamer = new RenamerWithTimeCounter(new FileRenamer());
            var file = new Mp3File("SampleFile.mp3");

            renamer.Rename(file);

            Assert.AreNotEqual((new TimeSpan()).ToString(), renamer.Elapsed.ToString());
        }

        [TestMethod]
        public void CheckRenamerWithTimeCounterAndPrmissionChecker()
        {
            var renamer = new RenamerWithTimeCounter(new RenamerWithPermissionCheck(new FileRenamer(), new PermissionChecker(), UserRole.User));
            var file = new Mp3File("SampleFile.mp3");

            renamer.Rename(file);

            Assert.AreEqual("NewSampleFile.mp3", file.Path);
            Assert.AreNotEqual((new TimeSpan()).ToString(), renamer.Elapsed.ToString());
        }
    }
}
