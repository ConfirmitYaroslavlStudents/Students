using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using CellsAutomate;
using CellsAutomate.Algorithms;
using CellsAutomate.Constants;
using CellsAutomate.Creatures;
using CellsAutomate.Food;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;
using Matrix = CellsAutomate.Matrix;

namespace ImpossibleCreatures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private Matrix _matrix;
        private Random _random = new Random();
        private int step = 0;

        private readonly Rectangle[,] _squares;

        public MainWindow()
        {
            InitializeComponent();
            var width = LogConstants.MatrixSize;
            var height = LogConstants.MatrixSize;

            _squares = new Rectangle[width, height];
            WorkWirhGrid.InitalizeGrid(MainGrid, _squares, width, height);
            WorkWirhGrid.MarkTable(MainGrid, width, height);

            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };
            _timer.Tick += MakeTurn;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var matrixSize = LogConstants.MatrixSize;
            int scale = 500 / matrixSize;

            var commandsForGetDirection = new GetDirectionAlgorithm().Algorithm;
            var commandsForGetAction = new GetActionAlgorithm().Algorithm;
            var creator = new CreatorOfCreature(commandsForGetAction, commandsForGetDirection);

            var matrix = new Matrix(matrixSize, matrixSize, creator, new FillingFromCornersByWavesStrategy());
            matrix.FillStartMatrixRandomly();
            Print(step, matrixSize, matrix);

            _timer.Start();
        }

        private void MakeTurn(object sender, object o)
        {
            step++;
            if (_matrix.AliveCount == 0)
            {
                _timer.Stop();
            }

            _matrix.MakeTurn();

            Print(step, LogConstants.MatrixSize, _matrix);
            _timer.Stop();
        }

        private string CreateLogOfCreature(Creature creature, int generation)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Generation: {generation}");
            builder.AppendLine("- - - - - ActionCommands - - - - -");
            builder.AppendLine(CommandsToString(creature.CommandsForGetAction));
            builder.AppendLine("- - - - - DirectionCommands - - - - -");
            builder.AppendLine(CommandsToString(creature.CommandsForGetDirection));
            return builder.ToString();
        }

        private string CommandsToString(ICommand[] commands)
        {
            var builder = new StringBuilder();
            var commandsToString = new CommandToStringParser();
            for (int num = 0; num < commands.Length; num++)
            {
                builder.AppendLine($"{num}) " + commandsToString.ParseCommand(commands[num]));
            }
            return builder.ToString();
        }

        private T ChooseRandom<T>(IList<T> collection)
        {
            return collection[_random.Next(collection.Count)];
        }

        private void PrintGeneration(CellsAutomate.Matrix creatures, int turn)
        {
            for (int i = 1; i <= turn + 1; i++)
            {
                var g = (from x in creatures.CreaturesAsEnumerable where x.Generation == i select x).Count();
                if (g != 0)
                    Console.WriteLine(i + "=> " + g);
            }
        }

        private void Print(int id, int length, Matrix matrix)
        {
            for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                            PaintSquare(i, j, matrix.EatMatrix.HasOneBite(new System.Drawing.Point(i, j)) ? Colors.Green : Colors.Blue, _squares);
                        //bitmap.SetPixel(i + k, j + l, matrix.EatMatrix.HasOneBite(new Point(i / scale, j / scale)) ? Color.Green : Color.White);

            //for (int i = 0; i < newLength; i += scale)
            //{
            //    for (int k = 0; k < scale; k++)
            //        for (int j = 0; j < newLength; j += scale)
            //        {
            //            for (int l = 0; l < scale; l++)
            //            {
            //                var x = i + newLength;
            //                var y = j;

            //                //bitmap.SetPixel(x + k, y + l, matrix.Creatures[i / scale, j / scale] == null ? Color.White : Color.Red);
            //            }
            //        }
            //}
        }

        public void CreateDirectory()
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
