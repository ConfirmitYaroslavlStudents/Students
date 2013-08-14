using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public class FizzBuzzer
    {
        public SortedDictionary<int, string> Rules { get; set; }

        public FizzBuzzer()
        {
           Rules = new SortedDictionary<int, string>();
           Rules.Add(3, "Fizz");
           Rules.Add(5, "Buzz");
        }

        public string ToString(int number)
        {
            var result = new List<string>();

            foreach (var rule in Rules)
                if (number % rule.Key == 0)
                    result.Add(rule.Value);

            if (result.Count > 0)
                return string.Join(" ", result);

            return number.ToString();
        }
    }
}
