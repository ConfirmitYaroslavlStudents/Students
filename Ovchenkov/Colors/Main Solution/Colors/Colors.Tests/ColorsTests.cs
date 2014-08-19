using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace Colors.Tests
{
    public class ColorsTests
    {
        public static IEnumerable<object[]> TwoColorsTestData
        {
            get
            {
                yield return new object[] { TypeOfProcess.RedAndRed, new Red(), new Red() };
                yield return new object[] { TypeOfProcess.GreenAndRed, new Green(), new Red() };
                yield return new object[] { TypeOfProcess.GreenAndGreen, new Green(), new Green() };
                yield return new object[] { TypeOfProcess.RedAndGreen, new Red(), new Green() };
            }
        }

        [Theory]
        [PropertyData("TwoColorsTestData")]
        public void Processor_TwoColorValues_ShouldPass(TypeOfProcess typeCode, IColor colorOne, IColor colorTwo)
        {
            var processor = new ColorsProcessor();
            processor.Process(colorOne, colorTwo);

            Assert.Equal(typeCode, processor.LastProcess);
        }
    }
}
