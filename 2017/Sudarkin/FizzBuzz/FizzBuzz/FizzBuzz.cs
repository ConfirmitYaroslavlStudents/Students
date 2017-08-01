using System;
using System.Collections.Generic;

namespace FizzBuzz
{
    public class FizzBuzz
    {
        private readonly SortedDictionary<int, string> _rules;

        public FizzBuzz()
        {
            _rules = new SortedDictionary<int, string>
            {
                {3, "Fizz"},
                {5, "Buzz" },
            };
        }

        public FizzBuzz(IDictionary<int, string> rules) : this()
        {
            AddRuleRange(rules);
        }

        public string GetStringRepresentationFor(int number)
        {
            string resultString = string.Empty;
            foreach (var rule in _rules)
            {
                if (number.IsDividableBy(rule.Key))
                {
                    resultString += rule.Value;
                }
            }

            return !string.IsNullOrEmpty(resultString) ? resultString : number.ToString();
        }

        public bool IsRuleExistsFor(int number)
        {
            return _rules.ContainsKey(number);
        }

        public void AddRuleRange(IDictionary<int, string> rules)
        {
            foreach (var rule in rules)
            {
                AddRule(rule.Key, rule.Value);
            }
        }

        public void AddRule(int number, string output)
        {
            if (IsRuleExistsFor(number))
            {
                throw new ArgumentException("This rule is already exists!");
            }

            _rules.Add(number, output);
        }

        public void RemoveRuleFor(int number)
        {
            _rules.Remove(number);
        }

        public void ChangeRule(int number, string newValue)
        {
            if (!IsRuleExistsFor(number))
            {
                throw new ArgumentException("This rule is not exists!");
            }

            _rules[number] = newValue;
        }
    }
}