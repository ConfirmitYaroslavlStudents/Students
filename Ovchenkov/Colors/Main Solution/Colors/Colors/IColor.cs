using Colors.Helper;

namespace Colors
{
    public interface IColor
    {
        void Accept(IProcessor processor, ProcessHelper helper);
    }
}
