using Mp3Lib;
using TagLib;

namespace CommandCreation
{
    internal class ChangeTagsCommand : Command
    {
        private IMp3File mp3File;
        private string _mask;

        // todo: why per instance? static? 
        public static int[] GetNumberOfArguments()
        {
            return new[] { 3 };
        }
        public override string GetCommandName()
        {
            return CommandNames.ChangeTags;
        }

        public ChangeTagsCommand(IMp3File audioFile, string mask)
        {
            mp3File = audioFile;
            _mask = mask;
        }

        public override void Execute()
        {         
            var mp3Manipulations = new Mp3Manipulations(mp3File);
            mp3Manipulations.ChangeTags(_mask);
        }
    }
}
