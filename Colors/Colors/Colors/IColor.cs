using Colors.Visitors;

namespace Colors.Colors
{
    interface IColor
    {
        T AcceptVisitor<T>(IColorVisitor<T> visitor);
    }
}