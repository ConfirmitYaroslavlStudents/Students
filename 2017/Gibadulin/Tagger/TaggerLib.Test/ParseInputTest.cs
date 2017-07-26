using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaggerLib.Test
{
    [TestClass]
    public class ParseInputTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WrongRecursiveConst_Exception()
        {
            string[] args = {"path", "mask", "toName", "WRONG_recursive"};

            var actual = ParseInput.Parse(args);
        }
    }
}
