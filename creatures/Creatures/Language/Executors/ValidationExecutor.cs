using System.Collections.Generic;
using System.Text;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Executors
{
    public class ValidationExecutor:ICommandVisitor
    {
        private Dictionary<string, int?> _variables;
        private StringBuilder _console;
        private Stack<bool> _conditions;

        private IExecutorToolset _executorToolset;
        private bool _stop;
        private bool _isExecutable;

        public bool Execute(IEnumerable<ICommand> parsedCommands, IExecutorToolset executorToolset)
        {
            _variables = new Dictionary<string, int?>();
            _console = new StringBuilder();
            _conditions = new Stack<bool>();
            _conditions.Push(true);
            _stop = false;
            _isExecutable = true;
            _executorToolset = executorToolset;

            foreach (var parsedCommand in parsedCommands)
            {
                Execute(parsedCommand);
                if (!_isExecutable)
                    break;
            }
            if (_conditions.Count > 1) _isExecutable = false;

            return _isExecutable;
        }

        private void Execute(ICommand command)
        {
            command.AcceptVisitor(this);
        }

        public void Accept(NewInt command)
        {
            if (_variables.ContainsKey(command.Name))
            {
                _isExecutable = false;
                return;
            }
            _variables[command.Name] = null;
        }

        public void Accept(SetValue command)
        {
            if (!_variables.ContainsKey(command.TargetName))
            {
                _isExecutable = false;
                return;
            }
            _variables[command.TargetName] = command.Value;
        }

        public void Accept(Plus command)
        {
            if (!_variables.ContainsKey(command.FirstSource)  || 
                !_variables.ContainsKey(command.SecondSource) ||
                !_variables.ContainsKey(command.TargetName)   ||
                _variables[command.FirstSource]==null         ||
                _variables[command.SecondSource]==null)
            {
                _isExecutable = false;
                return;
            }      
            var firstValue = _variables[command.FirstSource];
            var secondValue = _variables[command.SecondSource];
            _variables[command.TargetName] = firstValue + secondValue;
        }

        public void Accept(Minus command)
        {
            if (!_variables.ContainsKey(command.FirstSource)  ||
                !_variables.ContainsKey(command.SecondSource) ||
                !_variables.ContainsKey(command.TargetName)   ||
                _variables[command.FirstSource]==null         ||
                _variables[command.SecondSource]==null)
            {
                _isExecutable = false;
                return;
            }

            var firstValue = _variables[command.FirstSource];
            var secondValue = _variables[command.SecondSource];
            _variables[command.TargetName] = firstValue - secondValue;
        }

        public void Accept(CloneValue command)
        {
            if (!_variables.ContainsKey(command.SourceName) ||
                !_variables.ContainsKey(command.TargetName) ||
                _variables[command.SourceName]==null)
            {
                _isExecutable = false;
                return;
            }
            var value = _variables[command.SourceName];
            _variables[command.TargetName] = value;
        }

        public void Accept(Condition command)
        {
            if (!_variables.ContainsKey(command.ConditionName) ||
                _variables[command.ConditionName] == null)
            {
                _isExecutable = false;
                return;
            }
      
            var value = _variables[command.ConditionName];
            _conditions.Push(value >= 0);
        }

        public void Accept(Print command)
        {
            if (!_variables.ContainsKey(command.Variable) ||
                _variables[command.Variable]==null)
            {
                _isExecutable = false;
                return;
            }

            var value = _variables[command.Variable];
            _console.AppendLine(value.ToString());
        }

        public void Accept(CloseCondition command)
        {
            if (_conditions.Count == 1)
            {
                _isExecutable = false;
                return;
            }

            _conditions.Pop();
        }

        public void Accept(GetState command)
        {
            if (!_variables.ContainsKey(command.TargetName))
            {
                _isExecutable = false;
                return;
            }
            //TODO
            _variables[command.TargetName] = _executorToolset.GetState(command.Direction);
        }

        public void Accept(GetRandom command)
        {
            if (!_variables.ContainsKey(command.TargetName) ||
                !_variables.ContainsKey(command.MaxValueName) ||
                _variables[command.MaxValueName] == null)
            {
                _isExecutable = false;
                return;
            }

            _variables[command.TargetName] = _executorToolset.GetRandom(_variables[command.MaxValueName].Value);
        }

        public void Accept(Stop command)
        {       
            _stop = true;
        }

    }
}
