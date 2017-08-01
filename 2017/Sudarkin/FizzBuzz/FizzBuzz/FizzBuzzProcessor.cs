using System.Collections.Generic;

namespace FizzBuzz
{
    public class FizzBuzzProcessor
    {
        private readonly FizzBuzz _fizzBuzz;

        public FizzBuzzProcessor(FizzBuzz fizzBuzz)
        {
            _fizzBuzz = fizzBuzz;
        }

        public IEnumerable<string> Process(int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                yield return _fizzBuzz.GetStringRepresentationFor(i);
            }
        }
    }
}