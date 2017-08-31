using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using AutomatedTasker.StepConfig;
using AutomatedTasker.Steps;
using static System.String;

namespace AutomatedTasker.UI
{
    public class UserInterface
    {
        public string Command { set; get; }

        private List<IStepConfig> Steps { set; get; }

        public UserInterface()
        {
            Command = "";
        }

        public UserInterface(string command)
        {
            Command = command;
        }

        public string HelpMessage() => "string";

        public List<IStepConfig> ParseArguments(string argumentCommand = null)
        {
            List<IStepConfig> steps = new List<IStepConfig>();

            if (IsNullOrEmpty(argumentCommand))
                argumentCommand = Command;
            var commands = SplitCommands(argumentCommand);

            foreach (var command in commands)
            {
                var trimmedCommand = command.Trim();
                var stepConfigType = getStepConfigType(trimmedCommand);
                var isAlways = CheckIfAlways(trimmedCommand);
                steps.Add(GetStepConfig(stepConfigType, steps, trimmedCommand, isAlways));
            }

            Steps = steps;
            return Steps;
        }

        private List<string> SplitCommands(string argumentCommand)
        {
            var splittedCommands = new List<string>();

            int commaPosition = -1;
            var nextCommaPosition = argumentCommand.IndexOf(",", StringComparison.Ordinal);
            while (commaPosition < argumentCommand.Length)
            {
                int questionMarkPosition = argumentCommand.IndexOf("?", commaPosition+1, StringComparison.Ordinal);
                if (questionMarkPosition != -1 && questionMarkPosition < nextCommaPosition)
                {
                    int closingBracePosition = argumentCommand.IndexOf(">", questionMarkPosition, StringComparison.Ordinal);
                    int conditionalEndPosition = closingBracePosition;
                    if (conditionalEndPosition == -1)
                    {
                        splittedCommands.Add(argumentCommand.Substring(commaPosition + 1));
                        commaPosition = argumentCommand.Length;
                    }
                    else
                    {
                        if (argumentCommand[closingBracePosition + 1] == ':')
                            conditionalEndPosition =
                                argumentCommand.IndexOf(">", closingBracePosition + 1, StringComparison.Ordinal);
                        splittedCommands.Add(argumentCommand.Substring(commaPosition + 1,
                            conditionalEndPosition - commaPosition - 1));

                        commaPosition = argumentCommand.IndexOf(",", conditionalEndPosition + 1, StringComparison.Ordinal);
                        if (commaPosition == -1)
                            commaPosition = argumentCommand.Length;
                    }
                }
                else
                {
                    splittedCommands.Add(
                        argumentCommand.Substring(commaPosition + 1, nextCommaPosition - commaPosition - 1));
                    commaPosition = nextCommaPosition;
                }

                nextCommaPosition = argumentCommand.IndexOf(",", commaPosition == argumentCommand.Length? commaPosition - 1 : commaPosition + 1, StringComparison.Ordinal);
                if (nextCommaPosition == -1)
                    nextCommaPosition = argumentCommand.Length;
            }

            return splittedCommands;
        }

        private IStepConfig GetStepConfig(StepConfigType stepConfigType, List<IStepConfig> steps, string command, bool isAlways)
        {
            switch (stepConfigType)
            {
                case StepConfigType.Ordinary:
                    return GetOrdinaryStepConfig(command, isAlways);
                case StepConfigType.Conditional:
                    return GetConditionalStepConfig(steps, command, isAlways);
                default:
                    throw new ArgumentException("Unknown StepConfig type");
            }
        }

        private IStepConfig GetOrdinaryStepConfig(string command, bool isAlways)
        {
            IStep step = GetStep(command);
            return new OrdinaryStepConfig(step, new ExecutionCondition { Always = isAlways });
        }

        private IStepConfig GetConditionalStepConfig(List<IStepConfig> steps, string command, bool isAlways)
        {
            int questionMarkPosition = command.IndexOf("?", StringComparison.Ordinal);
            int colonPosition = command.IndexOf(":", StringComparison.Ordinal);
            if (colonPosition == -1)
                colonPosition = command.Length;
            int conditionItemPosition = steps.Count + int.Parse(command.Substring(0, questionMarkPosition));

            if (command.Length <= questionMarkPosition + 2)
                throw new ArgumentOutOfRangeException("Command is corrupted!");

            var stepsIfTrue =
                ParseArguments(command.Substring(questionMarkPosition+2, colonPosition - questionMarkPosition - 3));
            if (stepsIfTrue == null) throw new ArgumentNullException(nameof(stepsIfTrue));

            var stepsIfFalse = ParseArguments(command.Substring(colonPosition + 2, command.Length - colonPosition - 3));
            return new ConditionalStepConfig(steps[conditionItemPosition], stepsIfTrue, stepsIfFalse)
                { Condition = new ExecutionCondition() { Always = isAlways }};
        }

        private IStep GetStep(string command)
        {
            var argumentsBeginPosition = command.IndexOf("(", StringComparison.Ordinal) + 1;
            var argumentsLength = command.IndexOf(")", StringComparison.Ordinal) - argumentsBeginPosition;
            string arguments = command.Substring(argumentsBeginPosition, argumentsLength);

            var commandEndPosition = argumentsBeginPosition - 1;
            var stepType = GetStepType(command.Substring(0, commandEndPosition));
            switch (stepType)
            {
                case StepType.CMDStep:
                    return GetCMDStep(arguments);
                case StepType.BATStep:
                    return GetBATStep(arguments);
                default:
                    throw new ArgumentException("Unknown Step type");
            }
        }

        private IStep GetCMDStep(string arguments) => new CMDStep(arguments);

        private IStep GetBATStep(string arguments)
        {
            var spacePosition = arguments.IndexOf(" ", StringComparison.Ordinal);
            var batFile = arguments.Substring(0, spacePosition - 1);
            var batArguments = arguments.Substring(spacePosition + 1);

            return new BatStep(batFile, batArguments);
        }

        private StepType GetStepType(string type)
        {
            switch (type)
            {
                case "cmd":
                    return StepType.CMDStep;
                case "bat":
                    return StepType.BATStep;
                default:
                    throw new ArgumentException("Unknown command type");
            }
        }

        private StepConfigType getStepConfigType(string command) => 
            command.IndexOf("?", StringComparison.Ordinal) == -1 ?
                StepConfigType.Ordinary : StepConfigType.Conditional;

        private bool CheckIfAlways(string command) => command.EndsWith("!");
    }

    public enum StepConfigType
    {
        Ordinary,
        Conditional
    }

    public enum StepType
    {
        CMDStep,
        BATStep
    }
}
