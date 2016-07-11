using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellsAutomate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests
{
    [TestClass]
    public class ReachMatrixTests
    {
        [TestMethod]
        public void EmptyMatrix()
        {
            var matrix =
                new StringBuilder()
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ");

            var result = new ReachMatrixBuilder().Build(ToBools(matrix), 0, 0);

            var expectedResult =
                new StringBuilder()
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********");

            GetSure(expectedResult.ToString(), result);
        }

        [TestMethod]
        public void NoChance()
        {
            var matrix =
              new StringBuilder()
                  .AppendLine("*");

            var result = new ReachMatrixBuilder().Build(ToBools(matrix), 0, 0);

            var expectedResult =
                new StringBuilder()
                    .AppendLine(" ");

            GetSure(expectedResult.ToString(), result);
        }

        [TestMethod]
        public void Circle()
        {
            var matrix =
                new StringBuilder()
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("   ****   ")
                    .AppendLine("   *  *   ")
                    .AppendLine("   *  *   ")
                    .AppendLine("   ****   ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ");

            var result = new ReachMatrixBuilder().Build(ToBools(matrix), 0, 0);

            var expectedResult =
                new StringBuilder()
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("***    ***")
                    .AppendLine("***    ***")
                    .AppendLine("***    ***")
                    .AppendLine("***    ***")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********");

            GetSure(expectedResult.ToString(), result);
        }

        [TestMethod]
        public void Line()
        {
            var matrix =
                new StringBuilder()
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("**********")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ");

            var result = new ReachMatrixBuilder().Build(ToBools(matrix), 0, 0);

            var expectedResult =
                new StringBuilder()
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("**********")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ")
                    .AppendLine("          ");

            GetSure(expectedResult.ToString(), result);
        }

        private bool[,] ToBools(string matrix)
        {
            var expectedBools =
                matrix.Split(Environment.NewLine.ToArray())
                    .Where(x => x != string.Empty)
                    .Select(x => x.Select(y => y == '*').ToArray()).ToArray();

            var n = expectedBools.GetLength(0);
            var m = expectedBools[0].GetLength(0);

            var result = new bool[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = expectedBools[i][j];
                }
            }

            return result;
        }

        private bool[,] ToBools(StringBuilder matrix)
        {
            return ToBools(matrix.ToString());
        }

        private void GetSure(string expected, bool[,] actual)
        {
            var expectedBools = ToBools(expected);


            var n = expectedBools.GetLength(0);
            var m = expectedBools.GetLength(1);

            var n2 = actual.GetLength(0);
            var m2 = actual.GetLength(1);

            Assert.AreEqual(n, n2);
            Assert.AreEqual(m, m2);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Assert.AreEqual(expectedBools[i, j], actual[i, j]);
                }
            }
        }
    }
}
