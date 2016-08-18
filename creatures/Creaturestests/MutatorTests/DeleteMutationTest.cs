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
    public class DeleteMutationTest
    {
        [TestMethod]
        public void DeleteDeclaration()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  print ione
                                  print itwo
                                  ";
            const int indexToDelete = 0;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"
                                  int itwo
                                  itwo = 2
                                  print itwo
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteIf()
        {
            const string code = @"int ione
                                  ione = 1
                                  if ione then
                                  endif
                                  ";
            const int indexToDelete = 2;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteEndIf()
        {
            const string code = @"int ione
                                  ione = 1
                                  if ione then
                                  endif
                                  ";
            const int indexToDelete = 3;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                       ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteCloneValueInitializeSetter()
        {
            const string code = @"int ione
                                  int itwo
                                  ione = 1
                                  itwo = ione
                                  print itwo
                                  ";
            const int indexToDelete = 3;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                        ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteInitializeSetterMustNotDeleteDeclaration()
        {
            const string code = @"int ione
                                  int itwo
                                  ione = 1
                                  ";
            const int indexToDelete = 2;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteNotInitializeSetter()
        {
            const string code = @"int ione
                                  int itwo
                                  ione = 1
                                  ione = 2
                                  ";
            const int indexToDelete = 3;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                        ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteInitializeGetRandom()
        {
            const string code = @"int ione
                                  int itwo
                                  ione = 1
                                  itwo = random ione
                                  print itwo
                                  ";
            const int indexToDelete = 3;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                        ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteInitializeGetState()
        {
            const string code = @"int ione
                                  int itwo
                                  ione = 1
                                  itwo = getState 1
                                  print itwo
                                  ";
            const int indexToDelete = 3;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                        ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteStop()
        {
            const string code = @"int ione
                                  int itwo
                                  stop
                                  ione = 1
                                  itwo = getState 1
                                  print itwo
                                  ";
            const int indexToDelete = 2;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                        ione = 1
                                        itwo = getState 1
                                        print itwo
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeletePrint()
        {
            const string code = @"int ione
                                  int itwo                                  
                                  ione = 1
                                  print ione
                                  itwo = getState 1
                                  print itwo
                                  ";
            const int indexToDelete = 3;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                        ione = 1
                                        itwo = getState 1
                                        print itwo
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteTargetOfPlusMustDeletePlus()
        {
            const string code = @"int ione
                                  int itwo                                  
                                  int ithree
                                  ione = 1
                                  itwo = 2
                                  ithree = ione + itwo
                                  ";
            const int indexToDelete = 2;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo                                  
                                        ione = 1
                                        itwo = 2
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteSourceOfPlusMustDeletePlus()
        {
            const string code = @"int ione
                                  int itwo                                  
                                  int ithree
                                  ione = 1
                                  itwo = 2
                                  ithree = ione + itwo
                                  ";
            const int indexToDelete = 1;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione  
                                        int ithree                             
                                        ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteTargetOfMinusMustDeleteMinus()
        {
            const string code = @"int ione
                                  int itwo                                  
                                  int ithree
                                  ione = 1
                                  itwo = 2
                                  ithree = ione - itwo
                                  ";
            const int indexToDelete = 2;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @" int ione
                                         int itwo                                  
                                         ione = 1
                                         itwo = 2
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteSourceOfMinusMustDeleteMinus()
        {
            const string code = @"int ione
                                  int itwo                                  
                                  int ithree
                                  ione = 1
                                  itwo = 2
                                  ithree = ione - itwo
                                  ";
            const int indexToDelete = 1;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();

            const string resultCode = @"int ione     
                                        int ithree                          
                                        ione = 1
                                  ";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void DeleteUndo()
        {
            const string code = @"int ione
                                  int itwo                                  
                                  int ithree
                                  ione = 1
                                  itwo = 2
                                  ithree = ione - itwo
                                  ";
            const int indexToDelete = 1;
            var commands = GenerateCommands(code);
            var mutation = GetDeleteMutation(indexToDelete, commands);

            mutation.Transform();
            mutation.Undo();

            var resultCommands = GenerateCommands(code);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        private bool AreCollectionsEquals(ICommandsList x, ICommandsList y)
        {
            if (x.Count != y.Count) return false;
            var comparer = new CommandsEqualityComparer();
            return !x.Where((t, i) => !comparer.IsEqual(t, y[i])).Any();
        }

        private DeleteCommandMutation GetDeleteMutation(int indexToDelete, ICommandsList commands)
        {
            var random = new DeleteRandom();
            random.Invoke(indexToDelete);
            return new DeleteCommandMutation(random, commands);
        }

        private ICommandsList GenerateCommands(string code)
        {
            return new CommandsList(new Parser().ProcessCommands(code));
        }

        private class DeleteRandom : Random
        {
            private Queue<int> _returnable;

            public DeleteRandom()
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
                return base.Next();
            }

            public override int Next(int minValue, int maxValue)
            {
                if (_returnable.Count > 0) return _returnable.Dequeue();
                return base.Next();
            }

            public void Invoke(int index)
            {
                _returnable.Enqueue(index);
            }
        }
    }
}
