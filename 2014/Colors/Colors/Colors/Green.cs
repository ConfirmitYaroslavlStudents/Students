using Colors.Visitors;

namespace Colors.Colors
{
    internal class Green : IColor
    {
        public string ColorGreen { get { return "Green"; } }

        public T AcceptVisitor<T>(IColorVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}