using Colors.Visitors;

namespace Colors.Colors
{
    class Red : IColor
    {
        public string ColorRed { get { return "Red"; } }

        public T AcceptVisitor<T>(IColorVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}