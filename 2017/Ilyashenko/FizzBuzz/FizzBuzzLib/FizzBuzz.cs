using System;
using System.Collections.Generic;

namespace FizzBuzzLib
{
    public class FizzBuzz
    {
        private SortedDictionary<int, string> _rules;

        public FizzBuzz()
        {
            Init();
        }

        public FizzBuzz(string[] rules)
        {
            Init();
            foreach (var r in rules)
            {
                var rule = r.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (rule.Length != 2)
                    throw new FormatException("Неверный формат вводимого правила! Правило должно иметь вид \"Число Значение \"");

                _rules.Add(int.Parse(rule[0]), rule[1]);
            }
        }

        private void Init()
        {
            _rules = new SortedDictionary<int, string>();
            _rules.Add(3, "Fizz");
            _rules.Add(5, "Buzz");
        }

        public string Process(int number)
        {
            var result = String.Empty;

            foreach (var rule in _rules)
            {
                if (number.IsDividableBy(rule.Key))
                    result += rule.Value;
            }

            return String.IsNullOrEmpty(result) ? number.ToString() : result;
        }

        public void AddRule(int number, string value)
        {
            if (_rules.ContainsKey(number))
                throw new ArgumentException("Такое правило уже существует!");
            _rules.Add(number, value);
        }

        public void RemoveRule(int number)
        {
            if (!_rules.ContainsKey(number))
                throw new ArgumentException("Такого правила не существует!");
            _rules.Remove(3);
        }

        public void ChangeRule(int number, string value)
        {
            if (!_rules.ContainsKey(number))
                throw new ArgumentException("Такого правила не существует!");
            _rules[number] = value;
        }
    }
}
