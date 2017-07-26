using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamersLib;

namespace RenamersTests
{
    [TestClass]
    public class PermissionCheckerTests
    {
        [TestMethod]
        public void ChechGuestPermissions()
        {
            var checker = new PermissionChecker();
            Assert.AreEqual(false, checker.CheckPermissions(new Mp3File("sample.mp3"), UserRole.Guest));
        }
        
        [TestMethod]
        public void ChechUserPermissions()
        {
            var checker = new PermissionChecker();
            Assert.AreEqual(true, checker.CheckPermissions(new Mp3File("sample.mp3"), UserRole.User));
        }

        [TestMethod]
        public void ChechAdministratorPermissions()
        {
            var checker = new PermissionChecker();
            Assert.AreEqual(true, checker.CheckPermissions(new Mp3File("sample.mp3"), UserRole.Administrator));
        }
    }
}
