using Mp3Lib;
using TagLib;

namespace CommandCreation
{
    internal class RenameCommand : Command
    {
        private IMp3File mp3File;
        private readonly string _pattern;

        public  static int[] GetNumberOfArguments()
        {
            return new[] { 3 };            
        }
        public override string GetCommandName()
        {
            return CommandNames.Rename;
        }

        public RenameCommand(IMp3File audioFile, string mask)
        {      
            mp3File = audioFile;
            _pattern = mask;
        }

        public override void Execute()
        {         
            var mp3Manipulations = new Mp3Manipulations(mp3File);
            mp3Manipulations.Rename(_pattern);
        }
    }
}
