using Microsoft.VisualStudio.TestTools.UnitTesting;
using Colors;

namespace ColorsTests
{
    [TestClass]
    public class ColorsTests
    {
        [TestMethod]
        public void SimpleTest()
        {
            var red = (IColor)new Red();
            var green = (IColor)new Green();

            ColorsOperator.Do(red, red);
            Assert.IsTrue(ColorsOperator.RedRed);

            ColorsOperator.Do(red, green);
            Assert.IsTrue(ColorsOperator.RedGreen);

            ColorsOperator.Do(green, red);
            Assert.IsTrue(ColorsOperator.GreenRed);

            ColorsOperator.Do(green, green);
            Assert.IsTrue(ColorsOperator.GreenGreen);
        }
    }
}
