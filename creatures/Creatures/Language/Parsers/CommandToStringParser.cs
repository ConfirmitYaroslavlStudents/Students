using System.Collections;
using System.Collections.Generic;
using System.Text;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Parsers
{
    public class CommandToStringParser : ICommandVisitor
    {
        private StringBuilder _builder;

        public CommandToStringParser()
        {
            _builder = new StringBuilder();
        }

        public string ParseCommands(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                command.AcceptVisitor(this);
            }
            return GetResult();
        }

        public string ParseCommand(ICommand command)
        {
            command.AcceptVisitor(this);
            return GetResult();
        }

        private string GetResult()
        {
            var res = _builder.ToString().Trim('\r', '\n', ' ');
            _builder.Clear();
            return res;
        }

        public void Accept(NewInt command)
        {
            _builder.AppendLine($"int {command.Name}");
        }

        public void Accept(SetValue command)
        {
            _builder.AppendLine($"{command.TargetName} = {command.Value}");
        }

        public void Accept(Plus command)
        {
            _builder.AppendLine($"{command.TargetName} = {command.FirstSource} + {command.SecondSource}");
        }

        public void Accept(Print command)
        {
            _builder.AppendLine($"print {command.Variable}");
        }

        public void Accept(Minus command)
        {
            _builder.AppendLine($"{command.TargetName} = {command.FirstSource} - {command.SecondSource}");
        }

        public void Accept(CloneValue command)
        {
            _builder.AppendLine($"{command.TargetName} = {command.SourceName}");
        }

        public void Accept(Condition command)
        {
            _builder.AppendLine($"if {command.ConditionName} then");
        }

        public void Accept(Stop command)
        {
            _builder.AppendLine("stop");
        }

        public void Accept(CloseCondition command)
        {
            _builder.AppendLine("endif");
        }

        public void Accept(GetState command)
        {
            _builder.AppendLine($"{command.TargetName} = getState {command.Direction}");
        }

        public void Accept(GetRandom command)
        {
            _builder.AppendLine($"{command.TargetName} = random {command.MaxValueName}");
        }
    }
}