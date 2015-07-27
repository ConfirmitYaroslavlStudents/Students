namespace Matrix.Writers.Commands
{
    public class WriteValueCommand : IWriteCommand
    {
        private readonly string _value;

        public WriteValueCommand(string value)
        {
            _value = value;
        }

        public void Write(IWriter writer)
        {
            writer.Write(_value);
        }
    }
}