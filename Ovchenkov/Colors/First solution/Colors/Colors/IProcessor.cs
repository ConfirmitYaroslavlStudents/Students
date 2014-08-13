namespace Colors
{
    public interface IProcessor
    {
        void Process(Red color);
        void Process(Blue color);
        void Process(Green color);
        void Process(Red colorOne, Red colorTwo);
        void Process(Red colorOne, Blue colorTwo);
        void Process(Blue colorOne, Red colorTwo);
        void Process(Blue colorOne, Blue colorTwo);
        void Process(Green colorOne, Green colorTwo);
        void Process(Green colorOne, Red colorTwo);
        void Process(Red colorOne, Green colorTwo);
        void Process(Green colorOne, Blue colorTwo);
        void Process(Blue colorOne, Green colorTwo);
    }
}
