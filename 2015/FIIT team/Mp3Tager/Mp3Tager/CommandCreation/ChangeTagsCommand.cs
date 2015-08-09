using Mp3Lib;
using TagLib;

namespace CommandCreation
{
    internal class ChangeTagsCommand : Command
    {
        private string _path;
        private string _mask;

        public override int[] GetNumberOfArguments()
        {
            return new[] { 3 };
        }
        public override string GetCommandName()
        {
            return CommandNames.ChangeTags;
        }

        public ChangeTagsCommand(string[] args)
        {
            CheckIfCanBeExecuted(args);
            _path = args[1];
            _mask = args[2];
        }

        public override void Execute()
        {
            var mp3File = new Mp3File(File.Create(_path));
            var mp3Manipulations = new Mp3Manipulations(mp3File);
            mp3Manipulations.ChangeTags(_mask);
        }
    }
}
