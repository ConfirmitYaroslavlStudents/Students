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
            var listOfTuples = new List<Tuple<IColorable, IColorable>>();
            var redColor = new RedColor();
            var greenColor = new GreenColor();
            listOfTuples.Add(Tuple.Create<IColorable, IColorable>(greenColor, redColor));
            listOfTuples.Add(Tuple.Create<IColorable, IColorable>(redColor, greenColor));
            listOfTuples.Add(Tuple.Create<IColorable, IColorable>(greenColor, greenColor));
            listOfTuples.Add(Tuple.Create<IColorable, IColorable>(redColor, redColor));

            var indexOfTypeCombination = 0;
            foreach (var tuple in listOfTuples)
            {
                dynamic firstColor = tuple.Item1;
                dynamic secondColor = tuple.Item2;
                var typeOfColorCombination = processor.Process(firstColor, secondColor);
                Assert.AreEqual((TypesOfColorCombination)indexOfTypeCombination, processor.Process(firstColor, secondColor), typeOfColorCombination.ToString());
                indexOfTypeCombination++;
            }
        }
    }
}
