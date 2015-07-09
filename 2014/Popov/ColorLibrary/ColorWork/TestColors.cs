using System;
using ColorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorWork
{
    class FlagColors : IProcessor
    {
        //only for tests
        public bool IsRedRed { get; private set; }
        public bool IsRedGreen { get; private set; }
        public bool IsGreenRed { get; private set; }
        public bool IsGreenGreen { get; private set; }

        public void Work(Green first, Green second)
        {
            IsGreenGreen = true;
        }

        public void Work(Red first, Red second)
        {
            IsRedRed = true;
        }

        public void Work(Red first, Green second)
        {
            IsRedGreen = true;
        }

        public void Work(Green first, Red second)
        {
            IsGreenRed = true;
        }
    }

    [TestClass]
    public class TestColors
    {
        [TestMethod]
        public void GreenWithRed()
        {
            var flags = new FlagColors();
            IColor greenColor = new Green(flags);
            IColor redColor = new Red(flags);
            greenColor.DoWith(redColor);

            Assert.IsTrue(flags.IsGreenRed);

            Assert.IsFalse(flags.IsGreenGreen);
            Assert.IsFalse(flags.IsRedGreen);
            Assert.IsFalse(flags.IsRedRed);
        }

        [TestMethod]
        public void RedWithGreen()
        {
            var flags = new FlagColors();
            IColor redColor = new Red(flags);
            IColor greenColor = new Green(flags);
            
            redColor.DoWith(greenColor);

            Assert.IsTrue(flags.IsRedGreen);

            Assert.IsFalse(flags.IsGreenGreen);
            Assert.IsFalse(flags.IsGreenRed);
            Assert.IsFalse(flags.IsRedRed);
        }
    }
}
