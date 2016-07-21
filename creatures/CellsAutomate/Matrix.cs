using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CellsAutomate
{
    public class Matrix
    {
        public int Length;
        public int Width;
        public FoodMatrix EatMatrix { get; set; }

        public SimpleCreature[,] Cells { get; set; }

        public Matrix(int n, int m)
        {
            Length = n;
            Width = m;
            EatMatrix = new FoodMatrix(n, m);
            Cells = new SimpleCreature[n, m];
        }

        public IEnumerable<SimpleCreature> CellsAsEnumerable
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (Cells[i, j] != null)
                            yield return Cells[i, j];
                    }
                }
            }
        }

        public int AliveCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (Cells[i, j] != null) count++;
                    }
                }

                return count;
            }
        }

        public void FillMatrixWithFood()
        {
            var placeHoldersMatrix = new bool[Length, Width];

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    placeHoldersMatrix[i, j] = Cells[i, j] != null;
                }
            }

            EatMatrix.Build(placeHoldersMatrix, 0, 0);
            EatMatrix.Build(placeHoldersMatrix, 0, Width - 1);
            EatMatrix.Build(placeHoldersMatrix, Length - 1, 0);
            EatMatrix.Build(placeHoldersMatrix, Length - 1, Width - 1);
        }

        public void FillStartMatrixRandomly()
        {
            var random = new Random();
            //var i = random.Next(N);
            //var j = random.Next(M);

            //Cells[20, 20] = new Creature(new Point(20, 20), executor, commands, random);

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Cells[i, j] = random.Next(100) % 4 == 0 ? new SimpleCreature(new Point(i, j), 1) : null;
                }
            }

            FillMatrixWithFood();
        }

        //public string PrintStartMatrix()
        //{
        //    var result = new bool[Length, Width];

        //    for (int i = 0; i < Length; i++)
        //    {
        //        for (int j = 0; j < Width; j++)
        //        {
        //            result[i, j] = Cells[i, j] != null;
        //        }
        //    }

        //    return PrintMatrix(result);
        //}

        //private string PrintMatrix(bool[,] matrix)
        //{
        //    var result = new StringBuilder();

        //    for (int i = 0; i < Length; i++)
        //    {
        //        for (int j = 0; j < Width; j++)
        //        {
        //            result.Append(matrix[i, j] ? "*" : " ");
        //        }
        //        result.AppendLine("|");
        //    }

        //    return result.ToString();
        //}

        public void MakeTurn()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if(Cells[i, j] != null && !Cells[i, j].HadMoved)
                        MakeTurn(Cells[i, j], EatMatrix, i, j);
                }
            }

            PrepareForNextTurn();

            FillMatrixWithFood();
        }

        private void PrepareForNextTurn()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Cells[i, j] != null)
                        Cells[i, j].HadMoved = false;
                }
            }
        }

        private void MakeTurn(SimpleCreature currentSimpleCreature, FoodMatrix eat, int i, int j)
        {
            var currentPoint = new Point(i, j);

            var turnResult = currentSimpleCreature.MyTurn();

            switch (turnResult)
            {
                case ActionEnum.Die:
                    MakeTurnDie(i, j);
                    break;
                case ActionEnum.MakeChild:
                    MakeTurnMakeChild(currentSimpleCreature, currentPoint);
                    break;
                case ActionEnum.Go:
                    MakeTurnGo(currentSimpleCreature, i, j, currentPoint);
                    break;
                default: throw new Exception();
            }
        }

        private void MakeTurnDie(int i, int j)
        {
            Cells[i, j] = null;
        }

        private void MakeTurnGo(SimpleCreature currentSimpleCreature, int i, int j, Point currentPoint)
        {
            var diretionResult = GetDirectionOrEat(currentSimpleCreature);

            if (diretionResult != DirectionEnum.Stay)
            {
                var newPosition = DirectionEx.PointByDirection(diretionResult, currentPoint);
                currentSimpleCreature.SetPosition(newPosition);
                Cells[i, j] = null;
                Cells[newPosition.X, newPosition.Y] = currentSimpleCreature;
            }
        }

        private void MakeTurnMakeChild(SimpleCreature currentSimpleCreature, Point currentPoint)
        {
            var resultDirection = ChooseDirectionForChild(currentPoint);
            if (resultDirection != DirectionEnum.Stay)
            {
                var newPosition = DirectionEx.PointByDirection(resultDirection, currentPoint);

                Cells[newPosition.X, newPosition.Y] = currentSimpleCreature.MakeChild(newPosition);
            }
        }

        private DirectionEnum ChooseDirectionForChild(Point parentPoint)
        {
            var directions = new List<DirectionEnum>();
            var points = DirectionEx.GetPoints(parentPoint.X, parentPoint.Y);

            foreach (var item in points)
            {
                if (DirectionEx.IsValid(item, Length, Width) && IsFree(item))
                {
                    directions.Add(DirectionEx.DirectionByPoint(parentPoint, item));
                }
            }

            if (directions.Count != 0)
            {
                return directions.ElementAt(0);
            }

            return DirectionEnum.Stay;
        }

        public bool IsFree(Point currentPoint)
        {
            if (Cells[currentPoint.X, currentPoint.Y] == null)
                return true;
            return false;
        }

        private DirectionEnum GetDirectionOrEat(SimpleCreature currentSimpleCreature)
        {

            if (EatMatrix.HasFood(currentSimpleCreature.Position))
            {
                currentSimpleCreature.EatFood(EatMatrix);
                return DirectionEnum.Stay;
            }

            var result = ChooseDirectionForMove(currentSimpleCreature.Position);

            switch (result)
            {
                case DirectionEnum.Up:
                    Stats.Up++;
                    break;
                case DirectionEnum.Right:
                    Stats.Right++;
                    break;
                case DirectionEnum.Down:
                    Stats.Down++;
                    break;
                case DirectionEnum.Left:
                    Stats.Left++;
                    break;
                case DirectionEnum.Stay:
                    break;
                default:
                    throw new Exception();
            }
            return result;
        }

        private DirectionEnum ChooseDirectionForMove(Point currentPoint)
        {
            var directions = new List<DirectionEnum>();
            var points = DirectionEx.GetPoints(currentPoint.X, currentPoint.Y);

            foreach (var item in points)
            {
                if (DirectionEx.IsValid(item, Length, Width) && IsFree(item) && EatMatrix.HasFood(item))
                {
                    directions.Add(DirectionEx.DirectionByPoint(currentPoint, item));
                }
            }

            if (directions.Count != 0)
            {
                return directions.ElementAt(0);
            }

            return DirectionEnum.Stay;
        }
    }
}
