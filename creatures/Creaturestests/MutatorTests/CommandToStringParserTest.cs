using System;
using System.Linq;
using System.Text;
using Creatures.Language.Commands;
using Creatures.Language.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Creaturestests.MutatorTests
{
    [TestClass]
    public class CommandToStringParserTest
    {

        [TestMethod]
        public void IntParse()
        {
            const string name = "commandName";
            var command = new NewInt(name);
            var commandParser=new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim('\n', ' ','\r');
            var desiredResult = $"int {name}";

            Assert.AreEqual(parsedCommand, desiredResult);
        }

        [TestMethod]
        public void PlusParse()
        {
            const string targetName = "targetName";
            const string firstSourceName = "firstSourseName";
            const string secondSourceName = "secondSourceName";
            var command = new Plus(targetName, firstSourceName, secondSourceName);
            var commandParser = new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = $"{targetName} = {firstSourceName} + {secondSourceName}";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void MinusParse()
        {
            const string targetName = "targetName";
            const string firstSourceName = "firstSourseName";
            const string secondSourceName = "secondSourceName";
            var command = new Minus(targetName, firstSourceName, secondSourceName);
            var commandParser = new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = $"{targetName} = {firstSourceName} - {secondSourceName}";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void CloneValueParse()
        {
            const string targetName = "target";
            const string sourceName = "source";
            var command = new CloneValue(targetName, sourceName);
            var commandParser = new CommandToStringParser();

            var parsedCommand=commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = $"{targetName} = {sourceName}";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void CloseConditionParse()
        {
            var command=new CloseCondition();
            var commandParser=new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = "endif";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void ConditionParse()
        {
            const string conditionName = "condition";
            var command = new Condition(conditionName);
            var commandParser = new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = $"if {conditionName} then";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void GetRandomParse()
        {
            const string targetName = "target";
            const string maxValueName = "cat";
            var command = new GetRandom(targetName, maxValueName);
            var commandParser = new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim('\n', ' ', '\r');
            var result = $"{targetName} = random {maxValueName}";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void GetStateParse()
        {
            const string targetName = "target";
            const int direction = 2;
            var command = new GetState(targetName, direction);
            var commandParser = new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = $"{targetName} = getState {direction}";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void PrintParse()
        {
            const string variableName = "somekindofvariable";
            var command = new Print(variableName);
            var commandParser = new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = $"print {variableName}";

            Assert.AreEqual(parsedCommand, result);
        }

        [TestMethod]
        public void StopParse()
        {
            var command = new Stop();
            var commandParser = new CommandToStringParser();

            var parsedCommand = commandParser.ParseCommand(command).Trim(' ', '\n', '\r');
            var result = "stop";

            Assert.AreEqual(result, parsedCommand);
        }

        [TestMethod]
        public void ReverseParseShouldBeSame()
        {
            var comparer=new CommandsEqualityComparer();
            var somecode =
                new StringBuilder(@"
                    int a
                    int b
                    int c
                    a = 1
                    b = getState 2
                    c = b
                    int d
                    d = random c
                    d = a + c
                    d = b - a
                    if d then
                    if a then
                    stop
                    endif
                    print d
                    endif
                        ");
            var parsedCommands = new Parser().ProcessCommands(somecode.ToString()).ToArray();

            var parsedCommandInString = new CommandToStringParser().ParseCommands(parsedCommands);
            var reverse = new Parser().ProcessCommands(parsedCommandInString).ToArray();

            Assert.AreEqual(reverse.Length, parsedCommands.Length);
            for (int i = 0; i < parsedCommands.Length; i++)
            {
                Assert.IsTrue(comparer.IsEqual(parsedCommands[i], reverse[i]));
            }
        }

    }
}