namespace Colors
{
    public interface IProcessor
    {
        void Process(IColor colorOne, IColor colorTwo);
        void Process(Red colorOne, Red colorTwo);
        void Process(Green colorOne, Green colorTwo);
        void Process(Red colorOne, Green colorTwo);
        void Process(Green colorOne, Red colorTwo);
    }
}
