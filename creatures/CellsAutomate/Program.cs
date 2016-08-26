using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using CellsAutomate.Algorithms;
using CellsAutomate.Constants;
using CellsAutomate.Creatures;
using CellsAutomate.Food;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;

namespace CellsAutomate
{
    class Program
    {
        private static Random _random = new Random();

        private static void Main(string[] args)
        {
            var matrixSize = LogConstants.MatrixSize;
            int scale = 500 / matrixSize;

            var commandsForGetDirection = new GetDirectionAlgorithm().Algorithm;
            var commandsForGetAction = new GetActionAlgorithm().Algorithm;
            var creator = new CreatorOfCreature(commandsForGetAction, commandsForGetDirection);

            var matrix = new Matrix(matrixSize, matrixSize, creator, new FillingFromCornersByWavesStrategy());
            matrix.FillStartMatrixRandomly();
            CreateDirectory();
            Print(0, matrixSize, matrix, scale);
            Console.WriteLine("0:{0}", matrix.AliveCount);

            var log = new StringBuilder();

            for (int i = 0; i < LogConstants.CountOfTurns; i++)
            {
                if (matrix.AliveCount == 0)
                    break;
                matrix.MakeTurn();

                if (i % 100 == 0)
                {
                    MarkParts(matrixSize, matrix);
                }
                

                Print(i + 1, matrixSize, matrix, scale);
                Console.WriteLine("{0}:{1}", i + 1, matrix.AliveCount);
                var generationStat =
                    string.Join(" ",
                    matrix
                        .CreaturesAsEnumerable
                        .Select(x => x.Generation)
                        .GroupBy(x => x)
                        .OrderBy(x => x.Key)
                        .Select(x => $"{x.Key}:{x.Count()}")
                        .ToArray());

                log.AppendLine(generationStat);

                //PrintGeneration(matrix, i);
            }

            var maxGeneration = matrix.CreaturesAsEnumerable.Max(n => n.Generation);
            var averageGeneration = (int)matrix.CreaturesAsEnumerable.Average(n => n.Generation);
            var limit = (int)(0.75 * maxGeneration);
            var step = averageGeneration / 5;

            var youngCreatures =
                matrix.CreaturesAsEnumerable.Where(n => n.Generation > limit && n.Generation != maxGeneration).ToArray();
            var mediumAgeCreatures =
                matrix.CreaturesAsEnumerable.Where(
                    n => n.Generation >= averageGeneration - step && n.Generation <= averageGeneration + step).ToArray();

            var randomYoung = ChooseRandom(youngCreatures);
            var randomMedium = ChooseRandom(mediumAgeCreatures);

            var youngCreatureLog = CreateLogOfCreature(randomYoung.Creature as Creature, randomYoung.Generation);
            var mediumCreatureLog = CreateLogOfCreature(randomMedium.Creature as Creature, randomMedium.Generation);
            var originalCreatureLog = CreateLogOfCreature(creator.CreateAbstractCreature() as Creature, 1);
            File.WriteAllText(LogConstants.PathToLogDirectory + "\\randomYoungCommands.txt", youngCreatureLog);
            File.WriteAllText(LogConstants.PathToLogDirectory + "\\randomMediumCommands.txt", mediumCreatureLog);
            File.WriteAllText(LogConstants.PathToLogDirectory + "\\originalCommands.txt", originalCreatureLog);

            File.WriteAllText(LogConstants.PathToLogDirectory + "\\Log.txt", log.ToString());
            Console.WriteLine("Up: " + Stats.Up);
            Console.WriteLine("Right: " + Stats.Right);
            Console.WriteLine("Down: " + Stats.Down);
            Console.WriteLine("Left: " + Stats.Left);
            Console.WriteLine("Mutations: " + Mutator.Mutator.MUTATIONS);
            Console.WriteLine("Successful mutations: " + Mutator.Mutator.MUTATIONSREAL);
            Console.WriteLine("Exceptions: " + Matrix.EXCEPTIONS);
            Console.ReadKey();
        }

        private static string CreateLogOfCreature(Creature creature, int generation)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Generation: {generation}");
            builder.AppendLine("- - - - - ActionCommands - - - - -");
            builder.AppendLine(CommandsToString(creature.CommandsForGetAction));
            builder.AppendLine("- - - - - DirectionCommands - - - - -");
            builder.AppendLine(CommandsToString(creature.CommandsForGetDirection));
            return builder.ToString();
        }

        private static string CommandsToString(ICommand[] commands)
        {
            var builder=new StringBuilder();
            var commandsToString = new CommandToStringParser();
            for (int num = 0; num < commands.Length; num++)
            {
                builder.AppendLine($"{num}) "+commandsToString.ParseCommand(commands[num]));
            }
            return builder.ToString();
        }

        private static T ChooseRandom<T>(IList<T> collection)
        {
            return collection[_random.Next(collection.Count)];
        }

        private static void MarkParts(int matrixSize, Matrix matrix)
        {
            var list = new List<Membrane>();
            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    var creature = matrix.Creatures[i, j];

                    if (creature != null)
                    {
                        list.Add(creature);
                    }
                }
            }

            int n = 0;

            if (list.Select(x => x.ParentMark).Distinct().Count() == 1)
            {
                for (var i = 0; i < matrixSize; i++)
                {
                    for (var j = 0; j < matrixSize; j++)
                    {
                        var creature = matrix.Creatures[i, j];

                        if (creature != null)
                        {
                            creature.ParentMark = n;
                            n++;
                            //creature.ParentMark = matrixSize / 2 > i
                            //    ? (matrixSize / 2 > j ? 4 : 3)
                            //    : (matrixSize / 2 > j ? 2 : 1);
                        }
                    }
                }
            }
        }

        private static void PrintGeneration(Matrix creatures, int turn)
        {
            for (int i = 1; i <= turn + 1; i++)
            {
                var g = (from x in creatures.CreaturesAsEnumerable where x.Generation == i select x).Count();
                if (g != 0)
                    Console.WriteLine(i + "=> " + g);
            }
        }

        private static void Print(int id, int length, Matrix matrix, int scale)
        {
            //if (id % 50 != 0) return;

            int newLength = length * scale;

            var bitmap = new Bitmap(newLength * 2, newLength);

            for (int i = 0; i < newLength; i += scale)
            {
                for (int k = 0; k < scale; k++)
                    for (int j = 0; j < newLength; j += scale)
                    {
                        for (int l = 0; l < scale; l++)
                            bitmap.SetPixel(i + k, j + l, matrix.EatMatrix.HasOneBite(new Point(i / scale, j / scale)) ? Color.Green : Color.White);
                    }
            }


            var colors = new Dictionary<int, Color>()
            {
                {0, Color.Red},
                { 1, Color.Blue },
                { 2, Color.Yellow },
                { 3, Color.Brown },
                { 4, Color.CadetBlue }
            };

            var random = new Random();
            

            for (int i = 0; i < newLength; i += scale)
            {
                for (int k = 0; k < scale; k++)
                    for (int j = 0; j < newLength; j += scale)
                    {
                        for (int l = 0; l < scale; l++)
                        {
                            var x = i + newLength;
                            var y = j;

                            var creature = matrix.Creatures[i/scale, j/scale];

                            var color = Color.White;
                            if (creature != null)
                            {
                                if (!colors.ContainsKey(creature.ParentMark))
                                {
                                    colors[creature.ParentMark] = Color.FromArgb(
                                        random.Next(255),
                                        random.Next(255),
                                        random.Next(255));
                                }
                                color = colors[creature.ParentMark];
                            }

                            bitmap.SetPixel(x + k, y + l, color);
                        }
                    }
            }
            bitmap.Save(LogConstants.PathToLogDirectory + $"\\{id}.bmp", ImageFormat.Bmp);
        }

        public static void CreateDirectory()
        {
            if (Directory.Exists(LogConstants.PathToLogDirectory))
            {
                var files = new DirectoryInfo(LogConstants.PathToLogDirectory).GetFiles();

                foreach (var file in files)
                    file.Delete();
                return;
            }
            Directory.CreateDirectory(LogConstants.PathToLogDirectory);
        }
    }
}