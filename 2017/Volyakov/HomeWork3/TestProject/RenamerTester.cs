using System;
using HomeWork3;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class RenamerTester
    {
        private MP3File[] files = new MP3File[]
        {
            new MP3File("PermissionZero",0),
            new MP3File("PermissionOne",1),
            new MP3File("PermissionTwo",2)
        };

        [TestMethod]
        public void CorrectMP3Rename()
        {
            var Renamer = new MP3Renamer();
            foreach(var file in files)
            {
                Renamer.Rename(file);
            }
            var expectedNames = new string[]
            {
                "NewPermissionZero",
                "NewPermissionOne",
                "NewPermissionTwo"
            };
            for (int i = 0; i < expectedNames.Length; i++)
            {
                Assert.AreEqual(expectedNames[i], files[i].FileName);
            }
        }

        [TestMethod]
        public void CorrectRenameWithPermissionCheck()
        {
            var userWithPemissionOne = new User(1);
            var Renamer = new RenamerWithPermissionCheck(
                new MP3Renamer(), userWithPemissionOne);

            foreach(var file in files)
            {
                Renamer.Rename(file);
            }
            var expectedNames = new string[]
            {
                "NewPermissionZero",
                "NewPermissionOne",
                "PermissionTwo"
            };
            for (int i = 0; i < expectedNames.Length; i++)
            {
                Assert.AreEqual(expectedNames[i], files[i].FileName);
            }
        }

        [TestMethod]
        public void CorrectRenameWithTimer()
        {
            var Renamer = new RenamerWithTimer(
                new MP3Renamer());

            Assert.AreEqual(0, Renamer.Elapsed.TotalMilliseconds);

            foreach (var file in files)
            {
                Renamer.Rename(file);
            }

            Assert.AreNotEqual(0, Renamer.Elapsed.TotalMilliseconds);
        }
    }
}
