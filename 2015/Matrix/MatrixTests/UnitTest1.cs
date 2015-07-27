using System;
using Matrix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var transposedMatrix = Matrix.Matrix.TransposeMatrix(2, 1, new[,]{{1}, {2}});
            Assert.AreEqual(1, transposedMatrix);
            Assert.AreEqual(2, transposedMatrix);

            Assert.AreEqual(1, transposedMatrix[0, 0]);
            Assert.AreEqual(2, transposedMatrix[0, 1]);
        }
    }
}
