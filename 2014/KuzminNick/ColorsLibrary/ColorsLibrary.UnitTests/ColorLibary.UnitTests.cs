using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorsLibrary.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Process_ProperOverloadOfMethodIsChoosen()
        {
            var processor = new Processor();
            var listOfTuples = new List<Tuple<IColorable, IColorable, string>>();
            IColorable redColor = new RedColor();
            IColorable greenColor = new GreenColor();
            listOfTuples.Add(Tuple.Create(greenColor, redColor, "GreenRed"));
            listOfTuples.Add(Tuple.Create(redColor, greenColor, "RedGreen"));
            listOfTuples.Add(Tuple.Create(greenColor, greenColor, "GreenGreen"));
            listOfTuples.Add(Tuple.Create(redColor, redColor, "RedRed"));

            foreach (var tuple in listOfTuples)
            {
                var firstColor = tuple.Item1;
                var secondColor = tuple.Item2;
                var expectedColotCombination = tuple.Item3;
                var actualColorCombination = processor.Process(firstColor, secondColor);
                Assert.AreEqual(expectedColotCombination, actualColorCombination);
            }
        }
    }
}
