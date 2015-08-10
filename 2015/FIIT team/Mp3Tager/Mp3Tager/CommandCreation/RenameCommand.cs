using Mp3Lib;
using TagLib;

namespace CommandCreation
{
    internal class RenameCommand : Command
    {
        private readonly string _path;
        private readonly string _pattern;

        public override int[] GetNumberOfArguments()
        {
            return new[] { 3 };            
        }
        public override string GetCommandName()
        {
            return CommandNames.Rename;
        }

        public RenameCommand(string[] args)
        {
            CheckIfCanBeExecuted(args);
            _path = args[1];
            _pattern = args[2];
        }

        public override void Execute()
        {
            var mp3File = new Mp3File(File.Create(_path));
            var mp3Manipulations = new Mp3Manipulations(mp3File);
            mp3Manipulations.Rename(_pattern);
        }
    }
}
