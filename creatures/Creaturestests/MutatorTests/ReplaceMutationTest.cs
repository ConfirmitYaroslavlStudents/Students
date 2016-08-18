using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class ReplaceMutationTest
    {
        [TestMethod]
        public void ReplaceNewInt()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2";
            const int indexToReplace = 2;
            var commandToReplace = new NewInt("noname");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForNewInt(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                       ione = 5
                                       int noname
                                       itwo = 2";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceCloseCondition()
        {
            const string code = @"int ione
                                  ione = 5
                                  if ione then
                                  stop
                                  endif
                                  int itwo
                                  itwo = 2";
            const int indexToReplace = 4;
            var commands = GenerateCommands(code);
            var mutation = GetMutationForCloseCondition(commands, indexToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        if ione then
                                        stop
                                        endif
                                        int itwo
                                        itwo = 2";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceStop()
        {
            const string code = @"int ione
                                  ione = 5
                                  if ione then
                                  stop
                                  endif
                                  int itwo
                                  itwo = 2";
            const int indexToReplace = 3;
            var commands = GenerateCommands(code);
            var mutation = GetMutationForStop(commands, indexToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        if ione then
                                        stop
                                        endif
                                        int itwo
                                        itwo = 2";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceCloneValue()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  int ithree
                                  ithree = 2
                                  int ifour
                                  itwo = ione
                                  stop";
            const int indexToReplace = 6;
            var commandToReplace = new CloneValue("ifour", "ithree");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForCloneValue(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        int ithree
                                        ithree = 2
                                        int ifour
                                        ifour = ithree
                                        stop";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceCondition()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  int ithree
                                  ithree = ione
                                  if ione then
                                  itwo = 2
                                  endif";
            const int indexToReplace = 5;
            var commandToReplace = new Condition("ithree");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForCondition(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        int ithree
                                        ithree = ione
                                        if ithree then
                                        itwo = 2
                                        endif";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceGetRandom()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int ithree
                                  ithree = ione
                                  itwo = random ione";
            const int indexToReplace = 6;
            var commandToReplace = new GetRandom("ithree", "ione");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForGetRandom(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int ithree
                                        ithree = ione
                                        ithree = random ione";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceGetState()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int ithree
                                  ithree = ione
                                  itwo = getState 3";
            const int indexToReplace = 6;
            var commandToReplace = new GetState("ithree", 0);
            var commands = GenerateCommands(code);
            var mutation = GetMutationForGetState(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int ithree
                                        ithree = ione
                                        ithree = getState 0";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceMinus()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int ithree
                                  ithree = ione
                                  itwo = ithree - ione";
            const int indexToReplace = 6;
            var commandToReplace = new Minus("ithree", "itwo", "ione");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForMinus(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int ithree
                                        ithree = ione
                                        ithree = itwo - ione";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplacePlus()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int ithree
                                  ithree = ione
                                  itwo = ithree + ione";
            const int indexToReplace = 6;
            var commandToReplace = new Plus("ithree", "itwo", "ione");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForPlus(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int ithree
                                        ithree = ione
                                        ithree = itwo + ione";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplacePrint()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  int ithree
                                  ithree = ione
                                  print ithree";
            const int indexToReplace = 5;
            var commandToReplace = new Print("ione");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForPrint(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        int ithree
                                        ithree = ione
                                        print ione";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceSetValue()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int ithree
                                  ithree = 6
                                  ";
            const int indexToReplace = 5;
            var commandToReplace = new SetValue("ione", 3);
            var commands = GenerateCommands(code);
            var mutation = GetMutationForSetValue(commands, indexToReplace, commandToReplace);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int ithree
                                        ione = 3";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void ReplaceUndo()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int ithree
                                  ithree = ione
                                  ";
            var commands = GenerateCommands(code);
            var mutation = new ReplaceCommandMutation(new Random(), commands);

            mutation.Transform();
            mutation.Undo();

            var assertCommands = GenerateCommands(code);
            Assert.IsTrue(AreCollectionsEquals(commands, assertCommands));
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

        private ReplaceCommandMutation GetMutationForCloseCondition(ICommandsList commands, int replaceIndex)
        {
            var random = new ReplaceRandom();
            random.TuneCloseCondition(replaceIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForCloneValue(ICommandsList commands, int replaceIndex, CloneValue command)
        {
            var random = new ReplaceRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var sourceDeclarationIndex = GetDeclarationIndexOfVariable(command.SourceName, commands);
            random.TuneCloneValue(replaceIndex, targetDeclarationIndex, sourceDeclarationIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForCondition(ICommandsList commands, int replaceIndex, Condition command)
        {
            var random = new ReplaceRandom();
            var declarationIndex = GetDeclarationIndexOfVariable(command.ConditionName, commands);
            random.TuneCondition(replaceIndex, declarationIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForGetRandom(ICommandsList commands, int replaceIndex, GetRandom command)
        {
            var random = new ReplaceRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var sourceDeclarationIndex = GetDeclarationIndexOfVariable(command.MaxValueName, commands);
            random.TuneGetRandom(replaceIndex, targetDeclarationIndex, sourceDeclarationIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForGetState(ICommandsList commands, int replaceIndex, GetState command)
        {
            var random = new ReplaceRandom();
            var declarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var direction = command.Direction;
            random.TuneGetState(replaceIndex, declarationIndex, direction);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForMinus(ICommandsList commands, int replaceIndex, Minus command)
        {
            var random = new ReplaceRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var firstSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.FirstSource, commands);
            var secondSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.SecondSource, commands);
            random.TuneMinus(replaceIndex, targetDeclarationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForPlus(ICommandsList commands, int replaceIndex, Plus command)
        {
            var random = new ReplaceRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var firstSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.FirstSource, commands);
            var secondSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.SecondSource, commands);
            random.TunePlus(replaceIndex, targetDeclarationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForNewInt(ICommandsList commands, int replaceIndex, NewInt command)
        {
            var random = new ReplaceRandom();
            random.TuneNewInt(replaceIndex, command.Name);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForPrint(ICommandsList commands, int replaceIndex, Print command)
        {
            var random = new ReplaceRandom();
            var declarationIndex = GetDeclarationIndexOfVariable(command.Variable, commands);
            random.TunePrint(replaceIndex, declarationIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForSetValue(ICommandsList commands, int replaceIndex, SetValue command)
        {
            var random = new ReplaceRandom();
            var declarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            random.TuneSetValue(replaceIndex, declarationIndex, declarationIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private ReplaceCommandMutation GetMutationForStop(ICommandsList commands, int replaceIndex)
        {
            var random = new ReplaceRandom();
            random.TuneStop(replaceIndex);
            return new ReplaceCommandMutation(random, commands);
        }

        private int GetDeclarationIndexOfVariable(string variable, ICommandsList commands)
        {
            int count = 0;
            foreach (NewInt command in commands.OfType<NewInt>())
            {
                if (command.Name == variable) return count;
                count++;
            }
            return -1;
        }

        private ICommandsList GenerateCommands(string code)
        {
            return new CommandsList(new Parser().ProcessCommands(code));
        }

        private class ReplaceRandom : Random
        {
            private readonly Queue<int> _returnable;

            public ReplaceRandom()
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

            public void TuneCloseCondition(int replaceIndex)
            {
                Invoke(replaceIndex);
            }

            public void TuneCloneValue(int replaceIndex, int targetDeclarationIndex, int sourceDeclarationIndex)
            {
                Invoke(replaceIndex);
                Invoke(targetDeclarationIndex, sourceDeclarationIndex);
            }

            public void TuneCondition(int replaceIndex, int declarationIndex)
            {
                Invoke(replaceIndex);
                Invoke(declarationIndex);
            }

            public void TuneGetRandom(int replaceIndex, int targetDeclarationIndex, int sourceDeclarationIndex)
            {
                Invoke(replaceIndex);
                Invoke(targetDeclarationIndex, sourceDeclarationIndex);
            }

            public void TuneGetState(int replaceIndex, int declarationIndex, int direction)
            {
                Invoke(replaceIndex);
                Invoke(declarationIndex, direction);
            }

            public void TuneMinus(int replaceIndex, int targetDeclarationIndex, int firstSourceDeclarationIndex, int secondSourceDeclarationIndex)
            {
                Invoke(replaceIndex);
                Invoke(targetDeclarationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex);
            }

            public void TunePlus(int replaceIndex, int targetDeclarationIndex, int firstSourceDeclarationIndex, int secondSourceDeclarationIndex)
            {
                Invoke(replaceIndex);
                Invoke(targetDeclarationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex);
            }

            public void TuneNewInt(int replaceIndex, string name)
            {
                Invoke(replaceIndex);
                InvokeName(name);
            }

            public void TunePrint(int replaceIndex, int declarationIndex)
            {
                Invoke(replaceIndex);
                Invoke(declarationIndex);
            }

            public void TuneSetValue(int replaceIndex, int declarationIndex, int value)
            {
                Invoke(replaceIndex);
                Invoke(declarationIndex, value);
            }

            public void TuneStop(int replaceIndex)
            {
                Invoke(replaceIndex);
            }

            private void Invoke(int value, params int[] others)
            {
                _returnable.Enqueue(value);
                for (int i = 0; i < others.Length; i++)
                {
                    _returnable.Enqueue(others[i]);
                }
            }

            private void InvokeName(string name)
            {
                _returnable.Enqueue(name.Length);
                foreach (char n in name)
                {
                    _returnable.Enqueue(n - 'a');
                }
            }
        }

    }
}
