using System.Collections.Generic;
using ColorLibrary;
using UnitTestsForColorLibrary;

namespace TestApp
{
    class Program
    {
        private static Processor IntelCorei7;
        static void Main(string[] args)
        {
            IntelCorei7 = new Processor();
            var colorPairs = new List<ColorPair>();
            colorPairs.Add(new ColorPair(new Red(), new Green(), "red_green"));
            colorPairs.Add(new ColorPair(new Green(), new Red(), "green_red"));
            colorPairs.Add(new ColorPair(new Red(), new Red(), "red_red"));
            colorPairs.Add(new ColorPair(new Green(), new Green(), "green_green"));
            foreach (var item in colorPairs)
            {
                var f = Shake(item.FirstColor, item.SecondColor).Equals(item.Answer);
            }
        }

        public static string Shake(IColored a, IColored b)
        {
            var typeOfFirstColor = a.GetType();
            var typeOfSecondColor = b.GetType();
            var names = typeOfFirstColor.Name.ToLower() + typeOfSecondColor.Name.ToLower();
            switch (names)
            {
                case "redred":
                    return IntelCorei7.Mix(new Red(a), new Red(b));

                case "greengreen":
                    return IntelCorei7.Mix(new Green(a), new Green(b));

                case "redgreen":
                    return IntelCorei7.Mix(new Red(a), new Green(b));

                case "greenred":
                    return IntelCorei7.Mix(new Green(a), new Red(b));
                default:
                    return default(string);
            }
        }
    }
}
