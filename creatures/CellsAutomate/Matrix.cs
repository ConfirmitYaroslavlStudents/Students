using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CellsAutomate
{
    public class Matrix
    {
        public int Length;
        public int Height;
        public FoodMatrix EatMatrix { get; set; }

        public AbstractCreature[,] Cells { get; set; }

        public Matrix(int length, int height)
        {
            Length = length;
            Height = height;
            EatMatrix = new FoodMatrix(length, height);
            Cells = new AbstractCreature[length, height];
        }

        public IEnumerable<AbstractCreature> CellsAsEnumerable
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Height; j++)
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
                    for (int j = 0; j < Height; j++)
                    {
                        if (Cells[i, j] != null) count++;
                    }
                }

                return count;
            }
        }

        public void FillMatrixWithFood()
        {
            var placeHoldersMatrix = new bool[Length, Height];

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    placeHoldersMatrix[i, j] = Cells[i, j] != null;
                }
            }

            EatMatrix.Build(placeHoldersMatrix, 0, 0);
            EatMatrix.Build(placeHoldersMatrix, 0, Height - 1);
            EatMatrix.Build(placeHoldersMatrix, Length - 1, 0);
            EatMatrix.Build(placeHoldersMatrix, Length - 1, Height - 1);
        }

        public void FillStartMatrixRandomly()
        {
            var random = new Random();

            var creator = new CreatorSimpleCreature();

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Cells[i, j] = random.Next(100) % 4 == 0 ? creator.CreateAbstractCreature(new Point(i, j), random, 1) : null;
                }
            }

            FillMatrixWithFood();
        }

        public void MakeTurn()
        {
            var cells = new List<AbstractCreature>();

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Cells[i, j] != null)
                    {
                        cells.Add(Cells[i, j]);
                    }
                }
            }

            foreach (var item in cells)
            {
                MakeTurn(item);
            }

            FillMatrixWithFood();
        }

        private void MakeTurn(AbstractCreature currentCreature)
        {
            var turnResult = currentCreature.MyTurn(EatMatrix, Cells);
            var action = turnResult.Item1;
            var direction = turnResult.Item2;

            switch (action)
            {
                case ActionEnum.Die:
                    MakeTurnDie(currentCreature.Position); break;
                case ActionEnum.MakeChild:
                    MakeTurnMakeChild(direction, currentCreature); break;
                case ActionEnum.Go:
                    MakeTurnGo(direction, currentCreature); break;
                case ActionEnum.Eat:
                    MakeTurnEat(currentCreature); break;
                default: throw new Exception();
            }
        }

        private void MakeTurnDie(Point position)
        {
            Cells[position.X, position.Y] = null;
        }

        private void MakeTurnMakeChild(DirectionEnum direction, AbstractCreature simpleCreature)
        {
            if (!simpleCreature.CanMakeChild || direction == DirectionEnum.Stay)
                return;
            var childPoint = DirectionEx.PointByDirection(direction, simpleCreature.Position);
            if (DirectionEx.IsValid(childPoint, Length, Height) && DirectionEx.IsFree(childPoint, Cells))
            {
                Cells[childPoint.X, childPoint.Y] = simpleCreature.MakeChild(childPoint);
            }
        }

        private void MakeTurnGo(DirectionEnum direction, AbstractCreature simpleCreature)
        {
            if (direction == DirectionEnum.Stay)
                return;
            var newPosition = DirectionEx.PointByDirection(direction, simpleCreature.Position);

            if (DirectionEx.IsValid(newPosition, Length, Height) && DirectionEx.IsFree(newPosition, Cells))
            {
                Cells[simpleCreature.Position.X, simpleCreature.Position.Y] = null;
                simpleCreature.SetPosition(newPosition);
                Cells[simpleCreature.Position.X, simpleCreature.Position.Y] = simpleCreature;
                AddStats(direction);
            }
        }

        private void AddStats(DirectionEnum direction)
        {
            switch (direction)
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
                default: throw new Exception();
            }
        }

        private void MakeTurnEat(AbstractCreature simpleCreature)
        {
            if (EatMatrix.TakeFood(simpleCreature.Position, Constants.OneBite))
            {
                simpleCreature.EatFood();
            }
        }
    }
}
