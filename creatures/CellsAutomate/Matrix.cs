using System;
using System.Collections.Generic;
using System.Drawing;
using CellsAutomate.Creatures;
using CellsAutomate.Food;
using CellsAutomate.Tools;

namespace CellsAutomate
{
    public class Matrix
    {
        public int Length;
        public int Height;
        public FoodMatrix EatMatrix { get; set; }
        private readonly Creator _creator;

        public Membrane[,] Creatures { get; set; }

        public Matrix(int length, int height, Creator creator, IFoodDistributionStrategy strategy)
        {
            Length = length;
            Height = height;
            _creator = creator;
            EatMatrix = new FoodMatrix(length, height, strategy);
            Creatures = new Membrane[length, height];
        }

        public IEnumerable<Membrane> CreaturesAsEnumerable
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

        private void FillMatrixWithFood()
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

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Creatures[i, j] = random.Next(1000) % 1000 == 0 ? new Membrane(_creator.CreateAbstractCreature(), random, new Point(i, j), 1, _creator) : null;
                }
            }
            FillMatrixWithFood();
        }

        public void MakeTurn()
        {
            var creatures = new List<Membrane>();

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

        private void MakeTurn(Membrane currentCreature)
        {
            try
            {
                var turnResult = currentCreature.Turn(EatMatrix, Creatures);
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
                        currentCreature.Eat(EatMatrix); break;
                    default: throw new Exception();
                }
            }
            catch (Exception)
            {
                MakeTurnDie(currentCreature.Position);
                EXEPTIONS++;
            }
        }

        public static int EXEPTIONS = 0;
        private void MakeTurnDie(Point position)
        {
            Creatures[position.X, position.Y] = null;
        }

        private void MakeTurnMakeChild(DirectionEnum direction, Membrane creature)
        {
            if (direction == DirectionEnum.Stay)
                return;
            var childPoint = DirectionEx.PointByDirection(direction, creature.Position);
            if (CommonMethods.IsValidAndFree(childPoint, Creatures))
            {
                Creatures[childPoint.X, childPoint.Y] = creature.MakeChild(childPoint);
            }
        }

        private void MakeTurnGo(DirectionEnum direction, Membrane creature)
        {
            if (direction == DirectionEnum.Stay)
                return;
            var newPosition = DirectionEx.PointByDirection(direction, creature.Position);

            if (!CommonMethods.IsValidAndFree(newPosition, Creatures)) return;
            creature.Move(Creatures, newPosition);
            Stats.AddStats(direction);
        }
    }
}
