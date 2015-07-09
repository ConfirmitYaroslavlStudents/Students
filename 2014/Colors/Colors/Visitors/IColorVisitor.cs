using Colors.Colors;

namespace Colors.Visitors
{
    internal interface IColorVisitor<out T>
    {
        T Visit(Red first);
        T Visit(Green color);
        T Visit(Blue color);
    }
}