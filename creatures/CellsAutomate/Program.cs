using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace CellsAutomate
{
    class Program
    {
        private static void Main(string[] args)
        {
            var length = 100;
            int scale = 500 / length;
            var matrix = new Matrix(length, length);
            matrix.FillStartMatrixRandomly();
            Print(0, length, matrix, scale);
            Console.WriteLine("0:{0}", matrix.AliveCount);
            var log = new StringBuilder();

            for (int i = 0; i < 100; i++)
            {
                matrix.MakeTurn();
                Print(i + 1, length, matrix, scale);
                if(matrix.AliveCount != 0)
                    Console.WriteLine("{0}:{1}", i + 1, matrix.AliveCount);
                var generationStat =
                    string.Join(" ",
                    matrix
                        .CellsAsEnumerable
                        .Select(x => x.Generation)
                        .GroupBy(x => x)
                        .OrderBy(x => x.Key)
                        .Select(x => string.Format("{0}:{1}", x.Key, x.Count()))
                        .ToArray());

                log.AppendLine(generationStat);
            }

            File.WriteAllText(@"C:\Confirmit\Log\Log.txt", log.ToString());
            Console.WriteLine(Stats.Up);
            Console.WriteLine(Stats.Right);
            Console.WriteLine(Stats.Down);
            Console.WriteLine(Stats.Left);
            Console.ReadKey();
        }

        private static void Print(int id, int length, Matrix matrix, int scale)
        {
            //if (id % 10 != 0) return;

            int newLength = length * scale;

            var bitmap = new Bitmap(newLength*2, newLength);

            for (int i = 0; i < newLength; i+=scale)
            {
                for(int k = 0; k < scale; k++)
                    for (int j = 0; j < newLength; j+=scale)
                    {
                        for(int l = 0; l < scale; l++)
                            bitmap.SetPixel(i + k, j + l, matrix.EatMatrix.HasFood(new Point(i / scale , j / scale)) ? Color.Green : Color.White);
                    }
            }

            for (int i = 0; i < newLength; i+=scale)
            {
                for (int k = 0; k < scale; k++)
                    for (int j = 0; j < newLength; j+=scale)
                    {
                        for (int l = 0; l < scale; l++)
                        {
                            var x = i + newLength;
                            var y = j;

                            bitmap.SetPixel(x + k, y + l, matrix.Cells[i / scale, j / scale] == null ? Color.White : Color.Red);
                        }
                    }
            }

            bitmap.Save($@"C:\Confirmit\Log\{id}.bmp", ImageFormat.Bmp);
        }
    }
}