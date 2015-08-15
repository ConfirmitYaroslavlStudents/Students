namespace CommandCreation
{
    public interface IWriter
    {
        void Write(string value);

        void WriteLine();

        void WriteLine(string value);
    }
}
