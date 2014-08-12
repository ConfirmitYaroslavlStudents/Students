namespace Colors
{
    public interface IColor
    {
        void DoFirst(IColor second);
        void DoSecond(Red first);
        void DoSecond(Green first);
    }
}
