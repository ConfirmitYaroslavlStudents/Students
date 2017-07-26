using System;
using Mp3UtilLib.Actions;
using Mp3UtilLib.Arguments;
using Mp3UtilLib.Logger;

namespace Mp3UtilLib
{
    public class Processor
    {
        private readonly Args _arguments;
        private readonly FileSystem.FileSystem _fileSystem;
        private readonly ConsoleLogger _logger;

        public Processor(Args arguments, ConsoleLogger logger)
        {
            _arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileSystem = new FileSystem.FileSystem();
        }

        public void Execute()
        {
            IActionStrategy action = GetActionStrategy(_arguments.Action);

            foreach (string file in 
                _fileSystem.GetFilesFromCurrentDirectory(_arguments.Mask, _arguments.Recursive))
            {
                try
                {
                    action.Process(new Mp3File(file));
                    _logger.WriteError($"{file} - The transformation is complete");
                }
                catch (Exception ex)
                {
                    _logger.WriteError(ex.Message);
                }
            }

            _logger.WriteSuccess("Done!");
        }

        private IActionStrategy GetActionStrategy(ProgramAction action)
        {
            switch (action)
            {
                case ProgramAction.ToFileName:
                    return new FileNameAction();
                case ProgramAction.ToTag:
                    return new TagAction();
                default:
                    return null;
            }
        }
    }
}