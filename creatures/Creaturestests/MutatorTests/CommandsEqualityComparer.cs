using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace Creaturestests.MutatorTests
{
    internal class CommandsEqualityComparer:ICommandVisitor
    {
        private ICommand _second;
        private bool _isEqual;
        public bool IsEqual(ICommand first, ICommand second)
        {
            if (first.GetType() != second.GetType()) return false;
            _second = second;
            Execute(first);
            return _isEqual;
        }

        private void Execute(ICommand command)
        {
            command.AcceptVisitor(this);
        }

        public void Accept(NewInt command)
        {
            _isEqual = command.Name == (_second as NewInt).Name;
        }

        public void Accept(SetValue command)
        {
            var comparableCommand = _second as SetValue;
            _isEqual = command.TargetName == comparableCommand.TargetName &&
                       command.Value == comparableCommand.Value;
        }

        public void Accept(Plus command)
        {
            var comparableCommand = _second as Plus;
            _isEqual = command.TargetName == comparableCommand.TargetName   &&
                       command.FirstSource == comparableCommand.FirstSource &&
                       command.SecondSource == comparableCommand.SecondSource;
        }

        public void Accept(Print command)
        {
            _isEqual = command.Variable == (_second as Print).Variable;
        }

        public void Accept(Minus command)
        {
            var comparableCommand = _second as Minus;
            _isEqual = command.TargetName == comparableCommand.TargetName   &&
                       command.FirstSource == comparableCommand.FirstSource &&
                       command.SecondSource == comparableCommand.SecondSource;
        }

        public void Accept(CloneValue command)
        {
            var comparableCommand = _second as CloneValue;
            _isEqual = command.TargetName == comparableCommand.TargetName &&
                       command.SourceName == comparableCommand.SourceName;
        }

        public void Accept(Condition command)
        {
            _isEqual = command.ConditionName == (_second as Condition).ConditionName;
        }

        public void Accept(Stop command)
        {
            _isEqual = true;
        }

        public void Accept(CloseCondition command)
        {
            _isEqual = true;
        }

        public void Accept(GetState command)
        {
            var comparableCommand = _second as GetState;
            _isEqual = command.TargetName == comparableCommand.TargetName &&
                       command.Direction == comparableCommand.Direction;
        }

        public void Accept(GetRandom command)
        {
            var comparableCommand = _second as GetRandom;
            _isEqual = command.TargetName == comparableCommand.TargetName &&
                       command.MaxValueName == comparableCommand.MaxValueName;
        }
    }
}
