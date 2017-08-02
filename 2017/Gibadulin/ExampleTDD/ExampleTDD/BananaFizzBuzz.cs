using System.Text;

namespace ExampleTDD
{
    public class BananaFizzBuzz
    {
        private const string Banana = "Banana";
        private const string Fizz = "Fizz";
        private const string Buzz = "Buzz";

        public string Process(int number)
        {
            var result = new StringBuilder();

            if (number.IsDividableBy(2))
                result.Append(Banana);

            if (number.IsDividableBy(3))
                result.Append(Fizz);

            if (number.IsDividableBy(5))
                result.Append(Buzz);

            if (result.Length == 0)
                result.Append(number.ToString());

            return result.ToString();
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
