using System;
using System.Collections.Generic;
using System.Drawing;
using CellsAutomate.Creatures;
using CellsAutomate.Factory;
using CellsAutomate.Food;

namespace CellsAutomate
{
    public class Matrix
    {
        public int Length;
        public int Height;
        public FoodMatrix EatMatrix { get; set; }

        public BaseCreature[,] Creatures { get; set; }

        public Matrix(int length, int height)
        {
            Length = length;
            Height = height;
            EatMatrix = new FoodMatrix(length, height, new FillingFromCornersByWavesStrategy());
            Creatures = new BaseCreature[length, height];
        }

        public IEnumerable<BaseCreature> CreaturesAsEnumerable
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        if (Creatures[i, j] != null)
                            yield return Creatures[i, j];
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
                        if (Creatures[i, j] != null) count++;
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
                    placeHoldersMatrix[i, j] = Creatures[i, j] != null;
                }
            }

            EatMatrix.Build(placeHoldersMatrix);
        }

        public void FillStartMatrixRandomly()
        {
            var random = new Random();

            var creator = new CreatorOfSimpleCreature();

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Creatures[i, j] = random.Next(100) % 4 == 0 ? creator.CreateAbstractCreature(new Point(i, j), random, 1) : null;
                }
            }

            FillMatrixWithFood();
        }

        public void MakeTurn()
        {
            var creatures = new List<BaseCreature>();

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Creatures[i, j] != null)
                    {
                        creatures.Add(Creatures[i, j]);
                    }
                }
            }

            foreach (var item in creatures)
            {
                MakeTurn(item);
            }

            FillMatrixWithFood();
        }

        private void MakeTurn(BaseCreature currentCreature)
        {
            var turnResult = currentCreature.MyTurn(EatMatrix, Creatures);
            var action = turnResult.Item1;
            var direction = turnResult.Item2;

            switch (action)
            {
                case ActionEnum.Die:
                    MakeTurnDie(currentCreature.GetPosition()); break;
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
            Creatures[position.X, position.Y] = null;
        }

        private void MakeTurnMakeChild(DirectionEnum direction, BaseCreature simpleCreature)
        {
            if (!simpleCreature.CanMakeChild || direction == DirectionEnum.Stay)
                return;
            var childPoint = DirectionEx.PointByDirection(direction, simpleCreature.GetPosition());
            if (DirectionEx.IsValid(childPoint, Length, Height) && DirectionEx.IsFree(childPoint, Creatures))
            {
                Creatures[childPoint.X, childPoint.Y] = simpleCreature.MakeChild(childPoint);
            }
        }

        private void MakeTurnGo(DirectionEnum direction, BaseCreature simpleCreature)
        {
            if (direction == DirectionEnum.Stay)
                return;
            var newPosition = DirectionEx.PointByDirection(direction, simpleCreature.GetPosition());

            if (DirectionEx.IsValid(newPosition, Length, Height) && DirectionEx.IsFree(newPosition, Creatures))
            {
                var currentPoint = simpleCreature.GetPosition();
                Creatures[currentPoint.X, currentPoint.Y] = null;
                simpleCreature.SetPosition(newPosition);
                currentPoint = simpleCreature.GetPosition();
                Creatures[currentPoint.X, currentPoint.Y] = simpleCreature;
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

        private void MakeTurnEat(BaseCreature simpleCreature)
        {
            if (EatMatrix.TakeFood(simpleCreature.GetPosition()))
            {
                simpleCreature.EatFood();
            }
        }
    }
}
