using System;
using System.Collections.Generic;
using System.Text;
using Creatures.Language.Commands;
using Creatures.Language.Commands.Interfaces;

namespace Creatures.Language.Executors
{
    
    public interface IExecutorToolset
    {
        int GetState(int direction);
        int GetRandom(int maxValue);
    }

    public class ExecutorToolset : IExecutorToolset
    {
        Random _random;

        public ExecutorToolset(Random random)
        {
            _random = random;
        }

        public int GetState(int direction)
        {
            return 4;    
        }

        public int GetRandom(int maxValue)
        {
            return 1;
        }
    }

    public class MyExecutorToolset : IExecutorToolset
    {
        private readonly Random _random;
        private readonly IDictionary<int, int> _state;

        public MyExecutorToolset(Random random, IDictionary<int, int> state)
        {
            _random = random;
            _state = state;
        }

        public int GetState(int condition)
        {
            return _state[condition];
        }

        public int GetRandom(int maxValue)
        {
            var result = _random.Next(maxValue);
            return result + 1;
        }
    }

    public class Executor : ICommandVisitor
    {
        private Dictionary<string, int?> _variables;
        private StringBuilder _console;
        private Stack<bool> _conditions;

        private IExecutorToolset _executorToolset;
        private bool _stop;

        public string Execute(IEnumerable<ICommand> parsedCommands, IExecutorToolset executorToolset)
        {
            _variables = new Dictionary<string, int?>();
            _console = new StringBuilder();
            _conditions = new Stack<bool>();
            _conditions.Push(true);
            _stop = false;

            _executorToolset = executorToolset;

            foreach (var parsedCommand in parsedCommands)
            {
                if (_stop) break;

                Execute(parsedCommand);
            }
            
            return _console.ToString();
        }

        private void Execute(ICommand command)
        {
            command.AcceptVisitor(this);
        }

        public void Accept(NewInt command)
        {
            if (!_conditions.Peek()) return;
             
            _variables[command.Name] = null;
        }

        public void Accept(SetValue command)
        {
            if (!_conditions.Peek()) return;
             
            _variables[command.TargetName] = command.Value;
        }

        public void Accept(Plus command)
        {
            if (!_conditions.Peek()) return;

            var firstValue = _variables[command.FirstSource];
            var secondValue = _variables[command.SecondSource];
            _variables[command.TargetName] = firstValue + secondValue;
        }

        public void Accept(Minus command)
        {
            if (!_conditions.Peek()) return;

            var firstValue = _variables[command.FirstSource];
            var secondValue = _variables[command.SecondSource];
            _variables[command.TargetName] = firstValue - secondValue;
        }

        public void Accept(CloneValue command)
        {
            if (!_conditions.Peek()) return;

            var value = _variables[command.SourceName];
            _variables[command.TargetName] = value;
        }

        public void Accept(Condition command)
        {
            if (!_conditions.Peek())
            {
                _conditions.Push(false);
                return;
            }

            var value = _variables[command.ConditionName];
            _conditions.Push(value >= 0);
        }

        public void Accept(Print command)
        {
            if (!_conditions.Peek()) return;

            var value = _variables[command.Variable];
            _console.AppendLine(value.ToString());
        }

        public void Accept(CloseCondition command)
        {
            _conditions.Pop();
        }

        public void Accept(GetState command)
        {
            if (!_conditions.Peek()) return;

            _variables[command.TargetName] = _executorToolset.GetState(command.Direction);
        }

        public void Accept(GetRandom command)
        {
            if (!_conditions.Peek()) return;

            _variables[command.TargetName] = _executorToolset.GetRandom(_variables[command.MaxValueName].Value);
        }

        public void Accept(Stop command)
        {
            if (!_conditions.Peek()) return;

            _stop = true;
        }
    }
}