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

            var matrix = new Matrix();
            matrix.Cells = new Creature[length, length];
            matrix.N = length;
            matrix.M = length;

            matrix.FillStartMatrixRandomly();
            Print(0, length, matrix);

            Console.WriteLine("0:{0}", matrix.AliveCount);
            var log = new StringBuilder();

            for (int i = 0; i < 1000; i++)
            {
                matrix.MakeTurn();
                Print(i + 1, length, matrix);
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

            File.WriteAllText(@"C:\Temp\Creatures\Log.txt", log.ToString());
            Console.WriteLine(Stats.Up);
            Console.WriteLine(Stats.Right);
            Console.WriteLine(Stats.Down);
            Console.WriteLine(Stats.Left);
        }

        private static void Print(int id, int length, Matrix matrix)
        {
            if (id % 10 != 0) return;

            var bitmap = new Bitmap(length*2, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    bitmap.SetPixel(i, j, matrix.Eat[i, j] ? Color.Green : Color.White);
                }
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    var x = i + length;
                    var y = j;

                    bitmap.SetPixel(x, y, matrix.Cells[i, j] == null ? Color.White : Color.Red);

                    // bitmap.SetPixel(x, y, matrix.Cells[i, j] == null ? (eat[i, j] ? Color.White : Color.Green) : Color.Red);
                }
            }

            bitmap.Save($@"C:\temp\creatures\bitmaps\{id}.bmp", ImageFormat.Bmp);
        }
    }
}