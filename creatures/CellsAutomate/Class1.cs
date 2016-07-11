using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;
using Creatures.Language.Executors;

namespace CellsAutomate
{
    public class SeedGenerator
    {
        public ICommand[] StartAlgorithm
        {
            get
            {
                return new Parser().ProcessCommands(GetAlgorithm()).ToArray();
            }
        }

        public string GetAlgorithm()
        {
            var commands =
            new StringBuilder()
                // зададим константы
                .AppendLine("int zero")
                .AppendLine("int one")
                .AppendLine("int two")
                .AppendLine("int three")
                .AppendLine("int four")

                .AppendLine("zero  = 0")
                .AppendLine("one   = 1")
                .AppendLine("two   = 2")
                .AppendLine("three = 3")
                .AppendLine("four  = 4")

                // возьмём состояние вокруг
                .AppendLine("int upState")
                .AppendLine("int rightState")
                .AppendLine("int downState")
                .AppendLine("int leftState")
                .AppendLine("upState    = getState 0")
                .AppendLine("rightState = getState 1")
                .AppendLine("downState  = getState 2")
                .AppendLine("leftState  = getState 3")

                // посчитаем сколько свободных клеток
                .AppendLine("int directionsToGo")
                .AppendLine("directionsToGo = 0")

                .AppendLine("int upState_isEmpty")
                .AppendLine("int rightState_isEmpty")
                .AppendLine("int downState_isEmpty")
                .AppendLine("int leftState_isEmpty")

                .AppendLine("upState_isEmpty    = upState    - four")
                .AppendLine("rightState_isEmpty = rightState - four")
                .AppendLine("downState_isEmpty  = downState  - four")
                .AppendLine("leftState_isEmpty  = leftState  - four")

                .AppendLine("if upState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                .AppendLine("if rightState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                .AppendLine("if downState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                .AppendLine("if leftState_isEmpty then")
                .AppendLine("directionsToGo = directionsToGo + one")
                .AppendLine("endif")

                //Если некуда идти, выведем 0, т.е. "стой на месте"
                .AppendLine("int minus_directionsToGo")
                .AppendLine("minus_directionsToGo = zero - directionsToGo")
                .AppendLine("if directionsToGo then")
                .AppendLine("if minus_directionsToGo then")
                .AppendLine("print zero")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                //Решим куда шагнуть
                .AppendLine("int selectedCell")
                .AppendLine("selectedCell = random directionsToGo")

                //Шагнём
                .AppendLine("int counter")
                .AppendLine("counter = zero")
                .AppendLine("int isThisCell")

                .AppendLine("if upState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print one")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if rightState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print two")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if downState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print three")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif")

                .AppendLine("if leftState_isEmpty then")
                .AppendLine("counter = counter + one")
                .AppendLine("isThisCell = counter - selectedCell")
                .AppendLine("if isThisCell then")
                .AppendLine("print four")
                .AppendLine("stop")
                .AppendLine("endif")
                .AppendLine("endif");

            return commands.ToString();
        }
    }
    public enum ActionEnum
    {
        Die,
        Left,
        Right,
        Up,
        Down,
        Stay
    }

    static class ActionEx
    {
        public static Point PointByAction(ActionEnum actionEnum, Point start)
        {
            switch (actionEnum)
            {
                case ActionEnum.Die:
                    throw new ArgumentException();
                case ActionEnum.Left:
                    return new Point(start.X - 1, start.Y);
                case ActionEnum.Right:
                    return new Point(start.X + 1, start.Y);
                case ActionEnum.Up:
                    return new Point(start.X, start.Y - 1);
                case ActionEnum.Down:
                    return new Point(start.X, start.Y + 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionEnum), actionEnum, null);
            }
        }

        public static ActionEnum ActionByPoint(Point start, Point finish)
        {
            var xOffset = finish.X - start.X;
            var yOffset = finish.Y - start.Y;

            if (xOffset == -1 && yOffset == 0) return ActionEnum.Left;
            if (xOffset == 1 && yOffset == 0) return ActionEnum.Right;
            if (xOffset == 0 && yOffset == 1) return ActionEnum.Down;
            if (xOffset == 0 && yOffset == -1) return ActionEnum.Up;

            throw new ArgumentException();
        }

        public static Point[] GetPoints(int i, int j)
        {
            return new[] { new Point(i + 1, j), new Point(i, j + 1), new Point(i - 1, j), new Point(i, j - 1) };
        }

        public static int DirectionByPoint(Point start, Point finish)
        {
            var xOffset = finish.X - start.X;
            var yOffset = finish.Y - start.Y;

            if (xOffset == 0 && yOffset == -1) return 0;
            if (xOffset == 1 && yOffset == 0) return 1;
            if (xOffset == 0 && yOffset == 1) return 2;
            if (xOffset == -1 && yOffset == 0) return 3;

            throw new ArgumentException();
        }
    }

    public class Matrix
    {
        public int N;
        public int M;

        public Creature[,] Cells { get; set; }

        public IEnumerable<Creature> CellsAsEnumerable
        {
            get
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        if (Cells[i,j] != null)
                            yield return Cells[i, j];
                    }
                }
            }
        }

        public bool[,] Eat { get; private set; }

        public int AliveCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        if (Cells[i, j] !=null) count++;
                    }
                }

                return count;
            }
        }

        public bool[,] CanBeReached()
        {
            var reachingMatrix = new bool[N, M];
            var placeHoldersMatrix = new bool[N, M];

            for (int  i = 0;  i < N;  i++)
            {
                for (int j = 0; j < M; j++)
                {
                    placeHoldersMatrix[i, j] = Cells[i, j] != null;
                }
            }


            var reachMatrixBuilder = new ReachMatrixBuilder();

            reachMatrixBuilder.Build(placeHoldersMatrix, reachingMatrix, 0, 0);
            reachMatrixBuilder.Build(placeHoldersMatrix, reachingMatrix, 0, M - 1);
            reachMatrixBuilder.Build(placeHoldersMatrix, reachingMatrix, N - 1, 0);
            reachMatrixBuilder.Build(placeHoldersMatrix, reachingMatrix, N - 1, M - 1);

            return reachingMatrix;
        }

        public void FillStartMatrixRandomly()
        {
            var random = new Random();
            //var i = random.Next(N);
            //var j = random.Next(M);

            var executor = new Executor();
            var commands = new SeedGenerator().StartAlgorithm;


            //Cells[20, 20] = new Creature(new Point(20, 20), executor, commands, random);

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Cells[i, j] = random.Next(100) % 4 == 0 ? new Creature(new Point(i, j), executor, commands, random) : null;
                }
            }

            Eat = CanBeReached();
        }

        public string PrintStartMatrix()
        {
            var result = new bool[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    result[i, j] = Cells[i, j] != null;
                }
            }

            return PrintMatrix(result);
        }

        private string PrintMatrix(bool[,] matrix)
        {
            var result = new StringBuilder();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    result.Append(matrix[i, j] ? "*" : " ");
                }
                result.AppendLine("|");
            }

            return result.ToString();
        }

        public void MakeTurn()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    MakeTurn(Cells[i, j], Eat, i, j);
                }
            }

            Eat = CanBeReached();
        }

        private void MakeTurn(Creature simpleCreature, bool[,] eat, int i, int j)
        {
            if (simpleCreature == null) return;
            
            var resultTuple = simpleCreature.MyTurn(eat);

            var result = resultTuple.Item2;

            if (resultTuple.Item1)
            {
                var newPosition = ActionEx.PointByAction(result, new Point(i, j));

                Cells[newPosition.X, newPosition.Y] = simpleCreature.MakeChild(newPosition);

                return;
            }

            if (result == ActionEnum.Stay) return;
            if (result == ActionEnum.Die) Cells[i, j] = null;
            else
            {
                var newPosition = ActionEx.PointByAction(result, new Point(i, j));
                simpleCreature.SetPosition(newPosition);
                Cells[i, j] = null;
                Cells[newPosition.X, newPosition.Y] = simpleCreature;
            }
        }
    }
}