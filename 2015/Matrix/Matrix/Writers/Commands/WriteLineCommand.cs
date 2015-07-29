namespace Matrix.Writers.Commands
{
    public class WriteLineCommand : IWriteCommand
    {
        public void Write(IWriter writer)
        {
            writer.WriteLine();
        }
    }
}