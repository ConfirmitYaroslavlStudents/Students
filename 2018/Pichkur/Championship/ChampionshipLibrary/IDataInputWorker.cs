namespace Championship
{
    public interface IDataInputWorker
    {
        void WriteLineMessage(string message);
        void WriteMessage(string message);
        string InputString();
    }
}
