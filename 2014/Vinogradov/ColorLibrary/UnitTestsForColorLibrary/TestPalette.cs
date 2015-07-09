using System.Collections.Generic;
using ColorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsForColorLibrary
{
    [TestClass]
    public class TestPalette
    {
        [TestMethod]
        public void TestFactory()
        {
            var IntelCorei7 = new Processor();
            var colorPairs = new List<ColorPair>();
            colorPairs.Add(new ColorPair(new Red(), new Green(), "red_green"));
            colorPairs.Add(new ColorPair(new Green(), new Red(), "green_red"));
            colorPairs.Add(new ColorPair(new Red(), new Red(), "red_red"));
            colorPairs.Add(new ColorPair(new Green(), new Green(), "green_green"));
            foreach (var item in colorPairs)
            {
                Assert.AreEqual(Shake(item.FirstColor, item.SecondColor,IntelCorei7), item.Answer);
            }
        }

        public string Shake(IColored a, IColored b, Processor intel)
        {
            var typeOfFirstColor = a.GetType();
            var typeOfSecondColor = b.GetType();
            var names = typeOfFirstColor.Name.ToLower() + typeOfSecondColor.Name.ToLower();
            switch (names)
            {
                case "redred":
                    return intel.Mix(new Red(a), new Red(b));

                case "greengreen":
                    return intel.Mix(new Green(a), new Green(b));

                case "redgreen":
                    return intel.Mix(new Red(a), new Green(b));

                case "greenred":
                    return intel.Mix(new Green(a), new Red(b));
                default:
                    return default(string);
            }
        }
    }
}
