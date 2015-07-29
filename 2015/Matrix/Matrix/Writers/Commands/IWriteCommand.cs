namespace Matrix.Writers.Commands
{
    public interface IWriteCommand
    {
        void Write(IWriter writer);
    }
}