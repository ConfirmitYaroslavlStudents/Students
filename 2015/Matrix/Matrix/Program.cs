using System;
using System.Linq;
using Matrix.Writers;

namespace Matrix
{
    public class Matrix
    {
        public Matrix(int n, int m, int[,] values)
        {
            N = n;
            M = m;
            Value = values;
        }

        public int N { get; private set; }
        public int M { get; private set; }
        public int[,] Value { get; private set; }

        public Matrix TransposeMatrix()
        {
            var transposeMatrix = new int[M, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    transposeMatrix[j, i] = Value[i, j];
                }
            }

            return new Matrix(M, N, transposeMatrix);
        }

        public void PrintMatrix(IWriter writer)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    writer.Write(Value[i, j].ToString());
                }
                writer.WriteLine();
            }
        }

        public static Matrix InputMatrix(int n, int m)
        {
            var matrix = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    var itemInput = Console.ReadLine();
                    var item = Int32.Parse(itemInput);
                    matrix[i, j] = item;
                }
            }

            return new Matrix(n, m, matrix);
        }
    }

    public class Writersfactory
    {
        public IWriter CreateWriter()
        {
            return new CompositeWriter(
                new IWriter[] {new ConsoleWriter(), new BufferWriter(new FileWriter())});
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input n:");
            var nInput = Console.ReadLine();
            var n = int.Parse(nInput);

            Console.WriteLine("Input m:");
            var mInput = Console.ReadLine();
            var m = int.Parse(mInput);
            
            var matrix = Matrix.InputMatrix(n, m);

            using (var writer = new Writersfactory().CreateWriter())
            {
                Console.WriteLine();
                matrix.PrintMatrix(writer);

                Console.WriteLine();
                matrix.TransposeMatrix().PrintMatrix(writer);
            }
        }
    }
}
