using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaggerLib.Test
{
    [TestClass]
    public class ActingTest
    {
        [TestMethod]
        public void Act_ToTagConst_ToTag()
        {
            var modifier = Consts.ToTag;
            var expected = new ToTag();

            var actual = Acting.Act(modifier);

            if (expected.GetType()!=actual.GetType())
                Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Act_WrongModifier_Exception()
        {
            var modifier = "wrong mod";

            var actual = Acting.Act(modifier);
        }
    }
}
