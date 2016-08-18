using System;
using System.Collections.Generic;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class DublicateMutationTest
    {
        [TestMethod]
        public void DublicateTest()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            const int dublicateFrom = 3;
            const int dublicateTo = 5;
            var commands = GenerateCommands(code);
            var mutation = GetDublicateMutation(commands, dublicateFrom, dublicateTo);
            
            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int three
                                        itwo = 2
                                        int four
                                        int five";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DublicateUndo()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            var commands = GenerateCommands(code);
            var mutation=new DublicateCommandMutation(new Random(),commands);

            mutation.Transform();
            mutation.Undo();

            var assertCommands = GenerateCommands(code);
            Assert.IsTrue(AreCollectionsEquals(commands, assertCommands));
        }

        private DublicateCommandMutation GetDublicateMutation(ICommandsList commands, int dublicateFrom, int dublicateTo)
        {
            var random=new DublicateRandom();
            random.Invoke(dublicateFrom, dublicateTo);
            return new DublicateCommandMutation(random, commands);
        }

        private bool AreCollectionsEquals(ICommandsList x, ICommandsList y)
        {
            var flag = x.Count == y.Count;
            var comparer = new CommandsEqualityComparer();
            for (int i = 0; i < x.Count; i++)
            {
                flag = comparer.IsEqual(x[i], y[i]);
                if (!flag) break;
            }
            return flag;
        }

        private ICommandsList GenerateCommands(string code)
        {
            return new CommandsList(new Parser().ProcessCommands(code));
        }

        private class DublicateRandom : Random
        {
            private readonly Queue<int> _returnable;

            public DublicateRandom()
            {
                _returnable = new Queue<int>();
            }
            public override int Next()
            {
                if (_returnable.Count > 0) return _returnable.Dequeue();
                return base.Next();
            }

            public override int Next(int maxValue)
            {
                if (_returnable.Count > 0) return _returnable.Dequeue();
                return base.Next(maxValue);
            }

            public override int Next(int minValue, int maxValue)
            {
                if (_returnable.Count > 0) return _returnable.Dequeue();
                return base.Next(minValue, maxValue);
            }

            public void Invoke(int value,params int[] other)
            {
                _returnable.Enqueue(value);
                foreach (var i in other)
                {
                    _returnable.Enqueue(i);
                }
            }
        }
    }
}
