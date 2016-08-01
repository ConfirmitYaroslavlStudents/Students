using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using CellsAutomate.Constants;
using CellsAutomate.Mutator.Mutations.Logging;
using Creatures.Language.Parsers;

namespace CellsAutomate
{
    class Program
    {
        private static void Main(string[] args)
        {
            //var commands = new SeedGenerator().StartAlgorithm;
            //var newCommands = commands.ToArray();
            //var mutator = new Mutator.Mutator(new Random());
            //var logger = new Logger();

            //for (int i = 0; i < 10; i++)
            //{
            //    newCommands = mutator.Mutate(newCommands, logger);
            //}
            //var str = logger.Builder.ToString();
            //Console.WriteLine(str);
            //var elderCommands = new CommandToStringParser().ParseCommands(commands);
            //var nowCommands = new CommandToStringParser().ParseCommands(newCommands);
            //Console.WriteLine(elderCommands + "\n");
            //Console.WriteLine(nowCommands);
            //Console.ReadKey();

            var length = LogConstants.Length;
            int scale = 500 / length;
            var matrix = new Matrix(length, length);
            matrix.FillStartMatrixRandomly();
            CreateDirectory();
            Print(0, length, matrix, scale);
            Console.WriteLine("0:{0}", matrix.AliveCount);
            var log = new StringBuilder();

            long i;
            for (i = 0; i < LogConstants.CountOfTurns; i++)
            {
                if (matrix.AliveCount == 0)
                    break;
                matrix.MakeTurn();
                Print(i + 1, length, matrix, scale);
                Console.WriteLine("{0}:{1}", i + 1, matrix.AliveCount);
                var generationStat =
                    string.Join(" ",
                    matrix
                        .CreaturesAsEnumerable
                        .Select(x => x.GetGeneration())
                        .GroupBy(x => x)
                        .OrderBy(x => x.Key)
                        .Select(x => $"{x.Key}:{x.Count()}")
                        .ToArray());

                log.AppendLine(generationStat);

                //PrintGeneration(matrix, i);
            }
            PrintGeneration(matrix, i);

            File.WriteAllText(LogConstants.Log + "\\Log.txt", log.ToString());
            Console.WriteLine("Up: " + Stats.Up);
            Console.WriteLine("Right: " + Stats.Right);
            Console.WriteLine("Down: " + Stats.Down);
            Console.WriteLine("Left: " + Stats.Left);
            Console.ReadKey();
        }

        private static void PrintGeneration(Matrix creatures, long turn)
        {
            for (int i = 1; i <= turn + 1; i++)
            {
                var g = (from x in creatures.CreaturesAsEnumerable where x.GetGeneration() == i select x).Count();
                if (g != 0)
                    Console.WriteLine(i + "=> " + g);
            }
        }

        private static void Print(long id, int length, Matrix matrix, int scale)
        {
            if (id % 50 != 0) return;

            int newLength = length * scale;

            var bitmap = new Bitmap(newLength * 2, newLength);

            for (int i = 0; i < newLength; i += scale)
            {
                for (int k = 0; k < scale; k++)
                    for (int j = 0; j < newLength; j += scale)
                    {
                        for (int l = 0; l < scale; l++)
                            bitmap.SetPixel(i + k, j + l, matrix.EatMatrix.HasFood(new Point(i / scale, j / scale)) ? Color.Green : Color.White);
                    }
            }

            for (int i = 0; i < newLength; i += scale)
            {
                for (int k = 0; k < scale; k++)
                    for (int j = 0; j < newLength; j += scale)
                    {
                        for (int l = 0; l < scale; l++)
                        {
                            var x = i + newLength;
                            var y = j;

                            bitmap.SetPixel(x + k, y + l, matrix.Creatures[i / scale, j / scale] == null ? Color.White : Color.Red);
                        }
                    }
            }
            bitmap.Save(LogConstants.Log + String.Format($"\\{id}.bmp"), ImageFormat.Bmp);
        }

        public static void CreateDirectory()
        {
            if(Directory.Exists(LogConstants.Log))
                Directory.Delete(LogConstants.Log, true);
            Directory.CreateDirectory(LogConstants.Log);
        }
    }
}