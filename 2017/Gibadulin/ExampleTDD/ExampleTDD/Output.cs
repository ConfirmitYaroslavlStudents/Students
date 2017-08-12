using System.Text;

namespace ExampleTDD
{
    public class Output
    {
        public string GetOutput()
        {
            var result = new StringBuilder();
            var bananaFizzBuzz = new BananaFizzBuzz();
            for (var i = 1; i < 100; i++)
            {
                result.Append(bananaFizzBuzz.Process(i));
                result.Append(" ");
            }
            result.Append(bananaFizzBuzz.Process(100));

            return result.ToString();
        }
    }
}