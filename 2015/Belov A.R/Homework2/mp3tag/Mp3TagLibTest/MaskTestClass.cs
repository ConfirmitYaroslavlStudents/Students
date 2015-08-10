using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3TagLib;

namespace Mp3TagTest
{
    [TestClass]
    public class MaskTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncorectParenthesisBalance()
        {
            var testMask = new Mask("a{x}bz{sd{y}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoClosingParenthesis()
        {
            var testMask = new Mask("a{x}bzsd{y");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenParenthesisCantBeLast()
        {
            var testMask = new Mask("{a{x}bzsd{y}{");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CloseParenthesisCantBeFirst()
        {
            var testMask = new Mask("{a{x}bzsd{y}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CloseParenthesisCantBeLast()
        {
            var testMask = new Mask("a{x}bzsd{y}}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullMaskNotAllow()
        {
            var testMask = new Mask(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MaskWithoutParenthesisNotAllow()
        {
            var testMask = new Mask("badmask");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TagsCantRepeated()
        {
            var testMask = new Mask("asds{a}asdasd{a}dasd");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BehaviorWithIncorrectMaskTest()
        {
            var testMask = new Mask("a{x}ss{y}");
            var testString = "aaaabbbb";
            testMask.GetTagValuesFromString(testString);
        }

        [TestMethod]
        public void GetTagValuesTest()
        {
            var testMask=new Mask("a{x}b{y}");
            var testString = "aaaabbbb";
            var posibleValues = new Dictionary<string, string>
            {
                {"aaa", "bbb"},
                {"aaab", "bb"},
                {"aaabb", "b"},
                {"aaabbb", ""}
            };
            foreach (var posibleValue in posibleValues)
            {
                bool flag = false;
                foreach (var result in testMask.GetTagValuesFromString(testString).Where(result => result.ContainsValue(posibleValue.Key)))
                {
                    flag = result["x"] == posibleValue.Key && result["y"] == posibleValue.Value;
                    if (flag)
                        break;
                }
                Assert.AreEqual(true,flag);
            }

        }

        [TestMethod]
        public void TwoTagWithoutDelimiterTest()
        {
            var testMask = new Mask("somestring1{x}{y}somestring2");
            var testString = "somestring1aabbsomestring2";
            var posibleValues = new Dictionary<string, string>
            {
                {"", "aabb"},
                {"a", "abb"},
                {"aa", "bb"},
                {"aab", "b"},
                {"aabb", ""}
            };
            var results = testMask.GetTagValuesFromString(testString);
            foreach (var posibleValue in posibleValues)
            {
                bool flag = false;
                foreach (var result in results.Where(result=>result.ContainsValue(posibleValue.Key)))
                {
                    flag = result["x"] == posibleValue.Key && result["y"] == posibleValue.Value;
                    if (flag)
                        break;
                }
                Assert.AreEqual(true, flag);
            }
        }
    }
}
