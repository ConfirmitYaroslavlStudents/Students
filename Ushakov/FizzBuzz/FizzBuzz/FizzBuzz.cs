using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public class FizzBuzzer
    {
        public SortedDictionary<int, string> AdditionalReplaceRules { get; set; }

        public FizzBuzzer()
        {
            AdditionalReplaceRules = new SortedDictionary<int, string>();
        }

        public string ToString(int number)
        {
            var result = new List<string>();

            if (number % 3 == 0)
                result.Add("Fizz");

            if (number % 5 == 0)
                result.Add("Buzz");

            foreach (var rule in AdditionalReplaceRules)
                if (number % rule.Key == 0)
                    result.Add(rule.Value);

            if (result.Count > 0)
                return string.Join(" ", result);

            return number.ToString();
        }
    }
}
