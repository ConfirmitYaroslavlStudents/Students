using System;
using Colors.Colors;

namespace Colors.Visitors
{
    internal class SecondVisitor : IColorVisitor<string>
    {
        public Func<Red, string> TakeRed;
        public Func<Green, string> TakeGreen;
        public Func<Blue, string> TakeBlue;

        public string Visit(Red first)
        {
            return TakeRed(first);
        }

        public string Visit(Green color)
        {
            return TakeGreen(color);
        }

        public string Visit(Blue color)
        {
            return TakeBlue(color);
        }
    }
}