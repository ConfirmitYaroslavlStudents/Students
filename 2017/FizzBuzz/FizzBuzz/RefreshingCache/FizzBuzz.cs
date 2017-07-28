namespace RefreshingCache
{
    public class FizzBuzz
    {
        private const string Fizz = "Fizz";
        private const string Buzz = "Buzz";

        public string Process(int number)
        {
            if (number.IsDividableBy(15))
            {
                return Fizz + Buzz;
            }

            if (number.IsDividableBy(5))
            {
                return Buzz;
            }

            if (number.IsDividableBy(3))
            {
                return Fizz;
            }

            return number.ToString();
        }
    }

    public static class Int32Extensions
    {
        public static  bool IsDividableBy(this int number, int divider)
        {
            return number % divider == 0;
        }
    }
}
