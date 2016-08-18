using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class SwapMutationTest
    {
        [TestMethod]
        public void SwapTest()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            const int swapfirst = 3;
            const int swapsecond = 5;
            var commands = GenerateCommands(code);
            var mutation = GetSwapMutation(commands, swapfirst, swapsecond);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        int four
                                        int three
                                        itwo = 2
                                        int five";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void SwapUndo()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            var commands = GenerateCommands(code);
            var mutation = new SwapCommandMutation(new Random(), commands);

            mutation.Transform();
            mutation.Undo();

            var assertCommands = GenerateCommands(code);
            Assert.IsTrue(AreCollectionsEquals(commands, assertCommands));
        }

        private SwapCommandMutation GetSwapMutation(ICommandsList commands, int swapfirst, int swapsecond)
        {
            var random = new SwapRandom();
            random.Invoke(swapfirst, swapsecond);
            return new SwapCommandMutation(random, commands);
        }

        private bool AreCollectionsEquals(ICommandsList x, ICommandsList y)
        {
            if (x.Count != y.Count) return false;
            var comparer = new CommandsEqualityComparer();
            return !x.Where((t, i) => !comparer.IsEqual(t, y[i])).Any();
        }

        private ICommandsList GenerateCommands(string code)
        {
            return new CommandsList(new Parser().ProcessCommands(code));
        }

        private class SwapRandom : Random
        {
            private readonly Queue<int> _returnable;

            public SwapRandom()
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

            public void Invoke(int value, params int[] other)
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
