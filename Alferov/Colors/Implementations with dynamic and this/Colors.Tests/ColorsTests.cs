using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace Colors.Tests
{
    public class ColorsTests
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { ColorCombination.RedRed, new Red(), new Red() };
                yield return new object[] { ColorCombination.BlueBlue, new Blue(), new Blue() };
                yield return new object[] { ColorCombination.RedBlue, new Red(), new Blue() };
            }
        }

        [Theory]
        [PropertyData("TestData")]
        public void Process_TwoColorValues_ShouldPass(ColorCombination expected, IColor color1, IColor color2)
        {
            var processor = new Processor();
            color1.AcceptProcessor(color2, processor);
            Assert.Equal(expected, processor.LastProcessed);
        }

        [Fact]
        public void Process_IncorrectCombination_ArgumentExceptionThrown()
        {
            var processor = new Processor();
            IColor color1 = new Blue();
            IColor color2 = new Red();

            Assert.Throws(typeof (ArgumentException), () => color1.AcceptProcessor(color2, processor));
        }

        [Theory]
        [PropertyData("TestData")]
        public void ProcessWithColorHelper_TwoColorValues_ShouldPass(ColorCombination expected, IColor color1, IColor color2)
        {
            var processor = new Processor();
            new ColorHelper().Process(color1, color2, processor);
            Assert.Equal(expected, processor.LastProcessed);
        }

        [Fact]
        public void ProcessWithColorhelper_IncorrectCombination_ArgumentExceptionThrown()
        {
            var processor = new Processor();
            IColor color1 = new Blue();
            IColor color2 = new Red();

            Assert.Throws(typeof(ArgumentException), () => new ColorHelper().Process(color1, color2, processor));
        }
    }
}
