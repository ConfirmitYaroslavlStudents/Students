using Colors.Colors;

namespace Colors
{
    internal class ColorProcessor
    {
        public string Prosess(Red first, Red second)
        {
            return first.ColorRed + " "+ second.ColorRed;
        }

        public string Prosess(Red first, Blue second)
        {
            return first.ColorRed + " " + second.ColorBlue;
        }

        public string Prosess(Blue first, Green second)
        {
            return first.ColorBlue + " " + second.ColorGreen;
        }

        public string Prosess(Green first, Green second)
        {
            return first.ColorGreen + " " + second.ColorGreen;
        }

        public string ProcessDefault(IColor first, IColor second)
        {
            return "I don't know exact color";
        }
    }
}