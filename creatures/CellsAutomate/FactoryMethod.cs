using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creatures.Language.Executors;

namespace CellsAutomate
{
    public abstract class AbstractCreature
    {
        public Point Position { get; set; }

        public int Generation { get; set; }

        public int FoodSupply = Constants.FoodLevel;

        public bool CanMakeChild => FoodSupply >= Constants.ChildPrice + Constants.MinFoodToSurvive;

        public abstract Tuple<ActionEnum, DirectionEnum> MyTurn(FoodMatrix eatMatrix, AbstractCreature[,] cells);

        public abstract AbstractCreature MakeChild(Point position);

        public void SetPosition(Point newPosition)
        {
            Position = newPosition;
        }

        public void EatFood()
        {
            FoodSupply += Constants.OneBite;
        }
    }

    public abstract class Creator
    {
        public abstract AbstractCreature CreateAbstractCreature(Point position, Random random, int generation);
    }

    public class CreatorCreature : Creator
    {
        public override AbstractCreature CreateAbstractCreature(Point position, Random random, int generation)
        {
            var executor = new Executor();
            var commands = new SeedGenerator().StartAlgorithm;
            return new Creature(position, executor, commands, random, generation);
        }
    }

    public class CreatorSimpleCreature : Creator
    {
        public override AbstractCreature CreateAbstractCreature(Point position, Random random, int generation)
        {
            return new SimpleCreature(position, random, generation);
        }
    }
}
