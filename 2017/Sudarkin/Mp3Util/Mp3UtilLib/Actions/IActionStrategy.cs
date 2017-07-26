namespace Mp3UtilLib.Actions
{
    public interface IActionStrategy
    {
        void Process(AudioFile audioFile);
    }
}