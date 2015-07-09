using System.Collections.Generic;
using Colors.Utils;
using Xunit;
using Xunit.Extensions;

namespace Colors.Tests
{
    public class ColorsTests
    {
        public static IEnumerable<object[]> ColorsTestData
        {
            get
            {
                yield return new object[] { true, new[] { new Red(), new Red() } };
                yield return new object[] { true, new[] { new Blue(), new Blue() } };
                yield return new object[] { false, new IColored[] { new Red(), new Blue() } };
                yield return new object[] { true, new[] { new Red() } };
                yield return new object[] { true, new[] { new Blue() } };
                yield return new object[] { false, new[] { new Blue(), new Blue(), new Blue() } };
            }
        }

        [Theory]
        [PropertyData("ColorsTestData")]
        public void ProcessColored_ColoredValues_ShouldPass(bool isProcessed, IColored[] colored)
        {
            ColorHelper colorHelper = ColorHelpersFactory.GetColorHelper();
            bool actual = colorHelper.ProcessColored(colored);
            Assert.Equal(isProcessed, actual);
        }

        private static class ColorHelpersFactory
        {
            public static ColorHelper GetColorHelper()
            {
                var processor = new Processor();
                return new ColorHelper(processor);
            }
        }
    }
}