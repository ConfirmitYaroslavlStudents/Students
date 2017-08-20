using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoProcessor;

namespace AutoProcessorTests
{
    [TestClass]
    public class ProcessTest
    {
        [TestMethod]
        public void AddStep()
        {
            var process = new Process();

            Assert.AreEqual(0, process.Steps.Count);

            process.AddStep(new EmptyStep());

            Assert.AreEqual(1, process.Steps.Count);
        }
    }
}
