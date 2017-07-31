using System;
using System.Collections.Generic;

namespace FizzBuzzLibrary
{
    public class FizzBuzz
    {
        private SortedDictionary<int, string> DivisionReactions;

        public FizzBuzz()
        {
            Init();
        }

        public FizzBuzz(KeyValuePair<int,string>[] divisors)
        {
            Init();

            foreach (var pair in divisors)
                AddDivisor(pair.Key, pair.Value);
        }

        private void Init()
        {
            DivisionReactions = new SortedDictionary<int, string>()
            {
                {3, "Fizz" },
                {5, "Buzz" }
            };
        }

        public void AddDivisor(int divisor, string reaction)
        {
            if (DivisionReactions.ContainsKey(divisor))
                throw new ArgumentException("Repeating the divisor is not allowed");

            DivisionReactions.Add(divisor, reaction);
        }

        public string GetReaction(int number)
        {
            string reaction = "";

            foreach (var currentDivisor in DivisionReactions)
            {
                if (number.IsDividableBy(currentDivisor.Key))
                    reaction += currentDivisor.Value;
            }

            if (reaction == "")
                reaction = number.ToString();

            return reaction;
        }

        public bool RemoveDivisor(int divisor)
        {
            return DivisionReactions.Remove(divisor);
        }
        
        public bool ChangeReactionForDivisor(int divisor, string newReaction)
        {
            if (!DivisionReactions.ContainsKey(divisor))
                return false;

            DivisionReactions[divisor] = newReaction;

            return true;
        }
    }
}
