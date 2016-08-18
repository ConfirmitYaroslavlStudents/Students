using System;
using System.Collections.Generic;
using System.Linq;
using CellsAutomate.Mutator.CommandsList;
using CellsAutomate.Mutator.Mutations;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class AddMutationTest
    {
        [TestMethod]
        public void AddNewInt()
        {
            const string code = @"int ione
                                  int itwo";
            const int indexToAdd = 2;
            var commandToAdd = new NewInt("ithree");
            var commandsList = GenerateCommands(code);
            var mutation = GetMutationForNewInt(commandsList, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        int itwo
                                        int ithree";
            var resultCommandsList = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commandsList, resultCommandsList));
        }

        [TestMethod]
        public void AddSetValue()
        {
            const string code = @"int ione
                                  int itwo";
            const int value = 5;
            const int indexToAdd = 1;
            var commandToAdd = new SetValue("ione", value);
            var commandsList = GenerateCommands(code);
            var mutation = GetMutationForSetValue(commandsList, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commandsList, resultCommands));
        }

        [TestMethod]
        public void AddConditionBlock()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo";
            const int indexToIfAdd = 2;
            const int indexToEndIfAdd = 4;
            var commandToAdd = new Condition("ione");
            var commandsList = GenerateCommands(code);
            var mutation = GetMutationForConditionBlock(commandsList, commandToAdd, indexToIfAdd, indexToEndIfAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        if ione then
                                        int itwo
                                        endif";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commandsList, resultCommands));
        }

        [TestMethod]
        public void AddStop()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo";
            const int indexToAdd = 3;
            var commands = GenerateCommands(code);
            var mutation = GetMutationForStop(commands, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                       ione = 5
                                       int itwo
                                       stop";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void AddPrint()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2";
            const int indexToAdd = 4;
            var commandToAdd = new Print("itwo");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForPrint(commands, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                       ione = 5
                                       int itwo
                                       itwo = 2
                                       print itwo";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void AddMinus()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            const int indexToAdd = 6;
            var commandToAdd = new Minus("four", "ione", "itwo");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForMinus(commands, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int three
                                        int four
                                        four = ione - itwo
                                        int five";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void AddPlus()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            const int indexToAdd = 6;
            var commandToAdd = new Plus("four", "ione", "itwo");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForPlus(commands, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int three
                                        int four
                                        four = ione + itwo
                                        int five";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }


        [TestMethod]
        public void AddGetRandom()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            const int indexToAdd = 6;
            var commandToAdd = new GetRandom("three", "itwo");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForGetRandom(commands, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int three
                                        int four
                                        three = random itwo
                                        int five";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void AddGetState()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2
                                  int three
                                  int four
                                  int five";
            const int indexToAdd = 6;
            var commandToAdd = new GetState("three", 2);
            var commands = GenerateCommands(code);
            var mutation = GetMutationForGetState(commands, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = 2
                                        int three
                                        int four
                                        three = getState 2
                                        int five";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void AddCloneValue()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo";
            const int indexToAdd = 3;
            var commandToAdd = new CloneValue("itwo", "ione");
            var commands = GenerateCommands(code);
            var mutation = GetMutationForCloneValue(commands, commandToAdd, indexToAdd);

            mutation.Transform();

            const string resultCode = @"int ione
                                        ione = 5
                                        int itwo
                                        itwo = ione";
            var resultCommands = GenerateCommands(resultCode);
            Assert.IsTrue(AreCollectionsEquals(commands, resultCommands));
        }

        [TestMethod]
        public void AddUndo()
        {
            const string code = @"int ione
                                  ione = 5
                                  int itwo
                                  itwo = 2";
            var commands = GenerateCommands(code);
            var mutation = new AddCommandMutation(new Random(), commands);

            mutation.Transform();
            mutation.Undo();

            var assertCommands = GenerateCommands(code);
            Assert.IsTrue(AreCollectionsEquals(commands, assertCommands));
        }

        private AddCommandMutation GetMutationForStop(ICommandsList commands, int indexToAdd)
        {
            var random = new AddRandom();
            random.TuneStop(indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForPrint(ICommandsList commands, Print command, int indexToAdd)
        {
            var random = new AddRandom();
            var declarationIndex = GetDeclarationIndexOfVariable(command.Variable, commands);
            random.TunePrint(declarationIndex, indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForGetState(ICommandsList commands, GetState command, int indexToAdd)
        {
            var random = new AddRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var direction = command.Direction;
            random.TuneGetState(targetDeclarationIndex, direction, indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForMinus(ICommandsList commands, Minus command, int indexToAdd)
        {
            var random = new AddRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var firstSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.FirstSource, commands);
            var secondSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.SecondSource, commands);
            random.TuneMinus(targetDeclarationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex, indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForPlus(ICommandsList commands, Plus command, int indexToAdd)
        {
            var random = new AddRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var firstSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.FirstSource, commands);
            var secondSourceDeclarationIndex = GetDeclarationIndexOfVariable(command.SecondSource, commands);
            random.TunePlus(targetDeclarationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex, indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForCloneValue(ICommandsList commands, CloneValue command, int indexToAdd)
        {
            var random = new AddRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var sourceDeclarationIndex = GetDeclarationIndexOfVariable(command.SourceName, commands);
            random.TuneCloneValue(targetDeclarationIndex, sourceDeclarationIndex, indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForGetRandom(ICommandsList commands, GetRandom command, int indexToAdd)
        {
            var random = new AddRandom();
            var targetDeclarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            var sourceDeclarationIndex = GetDeclarationIndexOfVariable(command.MaxValueName, commands);
            random.TuneGetRandom(targetDeclarationIndex, sourceDeclarationIndex, indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForConditionBlock(ICommandsList commands, Condition command, int ifIndex, int endIfIndex)
        {
            var random = new AddRandom();
            var declarationIndex = GetDeclarationIndexOfVariable(command.ConditionName, commands);
            random.TuneConditionBlock(declarationIndex, ifIndex, endIfIndex);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForSetValue(ICommandsList commands, SetValue command, int indexToAdd)
        {
            var random = new AddRandom();
            var declarationIndex = GetDeclarationIndexOfVariable(command.TargetName, commands);
            random.TuneSetValue(declarationIndex, command.Value, indexToAdd);
            return new AddCommandMutation(random, commands);
        }

        private AddCommandMutation GetMutationForNewInt(ICommandsList commands, NewInt command, int indexToAdd)
        {
            var random = new AddRandom();
            random.TuneNewInt(command.Name, indexToAdd);
            return new AddCommandMutation(random, commands);
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

        private class AddRandom : Random
        {
            private Queue<int> _returnable;

            public AddRandom()
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

            public void TuneNewInt(string name, int index)
            {
                InvokeType(new NewInt(name));
                Invoke(index);
                InvokeName(name);
            }

            public void TuneSetValue(int declarationIndex, int value, int indexToAdd)
            {
                InvokeType(new SetValue("", 0));
                Invoke(indexToAdd, declarationIndex, value);
            }

            public void TuneConditionBlock(int declarationIndex, int ifIndex, int endIfIndex)
            {
                InvokeType(new Condition(""));
                Invoke(ifIndex, endIfIndex, declarationIndex);
            }

            public void TuneGetRandom(int targetDeclaraionIndex, int sourceDeclarationIndex, int indexToAdd)
            {
                InvokeType(new GetRandom("", ""));
                Invoke(indexToAdd, targetDeclaraionIndex, sourceDeclarationIndex);
            }

            public void TuneGetState(int declarationIndex, int direction, int indexToAdd)
            {
                InvokeType(new GetState("", 0));
                Invoke(indexToAdd, declarationIndex, direction);
            }

            public void TuneMinus(int targetDeclararationIndex, int firstSourceDeclarationIndex,
                int secondSourceDeclarationIndex, int indexToAdd)
            {
                InvokeType(new Minus("", "", ""));
                Invoke(indexToAdd, targetDeclararationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex);
            }

            public void TunePlus(int targetDeclararationIndex, int firstSourceDeclarationIndex,
                int secondSourceDeclarationIndex, int indexToAdd)
            {
                InvokeType(new Plus("", "", ""));
                Invoke(indexToAdd, targetDeclararationIndex, firstSourceDeclarationIndex, secondSourceDeclarationIndex);
            }

            public void TunePrint(int declarationIndex, int indexToAdd)
            {
                InvokeType(new Print(""));
                Invoke(indexToAdd, declarationIndex);
            }


            public void TuneCloneValue(int targetDeclarationIndex, int sourceDeclarationIndex, int indexToAdd)
            {
                InvokeType(new CloneValue("", ""));
                Invoke(indexToAdd, targetDeclarationIndex, sourceDeclarationIndex);
            }

            public void TuneStop(int indexToAdd)
            {
                InvokeType(new Stop());
                Invoke(indexToAdd);
            }

            private void Invoke(int value, params int[] other)
            {
                _returnable.Enqueue(value);
                for (int i = 0; i < other.Length; i++)
                {
                    _returnable.Enqueue(other[i]);
                }
            }


            private void InvokeType(ICommand type)
            {
                if (type is NewInt) _returnable.Enqueue(0);
                if (type is CloneValue) _returnable.Enqueue(1);
                if (type is Condition || type is CloseCondition) _returnable.Enqueue(2);
                if (type is GetRandom) _returnable.Enqueue(3);
                if (type is GetState) _returnable.Enqueue(4);
                if (type is Minus) _returnable.Enqueue(5);
                if (type is Plus) _returnable.Enqueue(6);
                if (type is Print) _returnable.Enqueue(7);
                if (type is SetValue) _returnable.Enqueue(8);
                if (type is Stop) _returnable.Enqueue(9);
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
