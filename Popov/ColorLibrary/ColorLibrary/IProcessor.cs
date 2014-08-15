
namespace ColorLibrary
{
    public interface IProcessor
    {
        void Work(Green first, Green second);
        void Work(Red first, Red second);
        void Work(Red first, Green second);
        void Work(Green first, Red second);
    }
}
