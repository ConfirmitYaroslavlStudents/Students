namespace CommandCreation
{
    public interface IWorker
    {
        void Write(string value);

        void WriteLine();

        void WriteLine(string value);

        string ReadLine();
    }
}
