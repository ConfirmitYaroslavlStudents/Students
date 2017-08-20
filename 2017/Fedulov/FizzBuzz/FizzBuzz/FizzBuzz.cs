using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public class FizzBuzz
    {
        private readonly SortedDictionary<int, string> rules;

        public FizzBuzz()
        {
            rules = new SortedDictionary<int, string>
            {
                { 3, "Fizz" },
                { 5, "Buzz" }
            };
        }

        public string Process(int number)
        {
            string result = "";
            foreach (var rule in rules)
            {
                if (number.IsDividableBy(rule.Key))
                    result += rule.Value;
            }

            return string.IsNullOrEmpty(result) ? number.ToString() : result;
        }

        public bool isRuleExist(int number)
        {
            return rules.ContainsKey(number);
        }

        public void AddRule(int number, string value)
        {
            if (isRuleExist(number))
                throw new ArgumentException("Rule for number " + number + " already exist!" );

            rules.Add(number, value);
        }

        public void AddRuleRange(IDictionary<int, string> newRules)
        {
            foreach (var rule in newRules)
            {
                AddRule(rule.Key, rule.Value);
            }
        }

        public void ChangeRule(int number, string value)
        {
            if (!isRuleExist(number))
                throw new ArgumentException("Rule for number " + number + " does not exist!");

            rules[number] = value;
        }

        public void RemoveRule(int number)
        {
            if (!isRuleExist(number))
                throw new ArgumentException("Rule for number " + number + " does not exist!");

            rules.Remove(number);
        }
    }

    public static class Int32Extensions
    {
        public static bool IsDividableBy(this int number, int divider)
        {
            return number % divider == 0;
        }
    }
}
