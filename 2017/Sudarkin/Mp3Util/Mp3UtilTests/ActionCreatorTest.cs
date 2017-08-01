using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;
using Mp3UtilLib.Actions;

namespace Mp3UtilTests
{
    [TestClass]
    public class ActionCreatorTest
    {
        [TestMethod]
        public void GettingFileNameAction()
        {
            IActionStrategy action = new ActionCreator()
                .GetAction(ProgramAction.ToFileName);

            Assert.AreEqual(typeof(FileNameAction), action.GetType());
        }

        [TestMethod]
        public void GettingTagAction()
        {
            IActionStrategy action = new ActionCreator()
                .GetAction(ProgramAction.ToTag);

            Assert.AreEqual(typeof(TagAction), action.GetType());
        }
    }
}