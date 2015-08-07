namespace Mp3Lib
{
    public partial class Mp3Manipulations
    {
        private readonly IMp3File _mp3File;

        public Mp3Manipulations(IMp3File mp3File)
        {
            _mp3File = mp3File;
        }
    }
}
