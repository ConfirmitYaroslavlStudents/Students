namespace Mp3UtilConsole.Actions
{
    public interface IActionStrategy
    {
        void Process(Mp3File mp3File);
    }
}