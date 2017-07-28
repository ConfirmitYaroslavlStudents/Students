using System;
using System.Collections.Generic;

namespace FizzBuzz
{
    public class FizzBuzz
    {
        private readonly SortedDictionary<int, string> _rules;

        public FizzBuzz()
        {
            _rules = new SortedDictionary<int, string>(new DescendingComparer<int>())
            {
                {3, "Fizz"},
                {5, "Buzz" },
                {15, "FizzBuzz" }
            };
        }

        public string GetStringRepresentationFor(int number)
        {
            foreach (var rule in _rules)
            {
                if (number.IsDividableBy(rule.Key))
                {
                    return rule.Value;
                }
            }

            return number.ToString();
        }

        public bool IsRuleExistsFor(int number)
        {
            return _rules.ContainsKey(number);
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