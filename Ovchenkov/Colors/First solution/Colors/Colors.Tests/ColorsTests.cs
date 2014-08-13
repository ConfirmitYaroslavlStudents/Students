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
                yield return new object[] { TypeOfProcess.RedAndRed,  new Red(), new Red() };
                yield return new object[] { TypeOfProcess.BlueAndBlue, new Blue(), new Blue() };
                yield return new object[] { TypeOfProcess.RedAndBlue, new Red(), new Blue() };
                yield return new object[] { TypeOfProcess.BlueAndRed, new Blue(), new Red() };
                yield return new object[] { TypeOfProcess.GreenAndRed, new Green(), new Red() };
                yield return new object[] { TypeOfProcess.BlueAndGreen, new Blue(), new Green() };
                yield return new object[] { TypeOfProcess.GreenAndGreen, new Green(), new Green() };
                yield return new object[] { TypeOfProcess.RedAndGreen, new Red(), new Green() };
                yield return new object[] { TypeOfProcess.GreenAndBlue, new Green(), new Blue() };
            }
        }

        public static IEnumerable<object[]> OneColorTestData
        {
            get
            {
                yield return new object[] { TypeOfProcess.Red, new Red() };
                yield return new object[] { TypeOfProcess.Blue, new Blue()};
                yield return new object[] { TypeOfProcess.Green, new Green() };
            }
        }

       [Theory]
       [PropertyData("TwoColorsTestData")]
        public void Processor_TwoColorValues_ShouldPass(TypeOfProcess typeCode, IColor colorOne, IColor colorTwo)
        {
            var processor = new Processor();
            var processHelper = new ProcessHelper<IProcessor>(processor);

            processHelper.Process(colorOne, colorTwo);

            Assert.Equal(typeCode, processor.LastProcess);
        }

       [Theory]
       [PropertyData("OneColorTestData")]
       public void Processor_OneColorValue_ShouldPass(TypeOfProcess typeCode, IColor color)
       {
           var processor = new Processor();
           var processHelper = new ProcessHelper<IProcessor>(processor);

           processHelper.Process(color);

           Assert.Equal(typeCode, processor.LastProcess);
       }
    }
}
