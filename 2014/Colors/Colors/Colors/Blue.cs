using Colors.Visitors;

namespace Colors.Colors
{
    class Blue : IColor
    {
        public string ColorBlue { get { return "Blue"; } }

        public T AcceptVisitor<T>(IColorVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}