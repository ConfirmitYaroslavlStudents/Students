using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TimeMeasurer.Tests
{
    [TestClass]
    public class RenamerTest
    {
        [TestMethod]
        public void RenameWithoutPermissionChecker()
        {
            Mp3File mp3File = new Mp3File();
            IMp3Renamer renamer = new Mp3Renamer(new TimeMeasurer());

            renamer.Rename(mp3File);

            Assert.AreEqual("NewName", mp3File.FullName);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            IPermissionsChecker checker = new PermissionsChecker(new TimeMeasurer());

            Assert.AreEqual(true, checker.CheckPermission(new Mp3File(), UserRole.Administrator));
            Assert.AreEqual(false, checker.CheckPermission(new Mp3File(), UserRole.Guest));
        }

        [TestMethod]
        public void RenameWithPermissionCheckerWithoutAccess()
        {
            Mp3File mp3File = new Mp3File();
            IMp3Renamer renamer = new WithPermissionDecorator(
                new Mp3Renamer(new TimeMeasurer()), new PermissionsChecker(new TimeMeasurer()), UserRole.Guest);

            renamer.Rename(mp3File);

            Assert.AreEqual("DefaultPath", mp3File.FullName);
        }

        [TestMethod]
        public void RenameWithPermissionCheckerWithAccess()
        {
            Mp3File mp3File = new Mp3File();
            IMp3Renamer renamer = new WithPermissionDecorator(
                new Mp3Renamer(new TimeMeasurer()), new PermissionsChecker(new TimeMeasurer()), UserRole.Administrator);

            renamer.Rename(mp3File);

            Assert.AreEqual("NewName", mp3File.FullName);
        }
    }
}
