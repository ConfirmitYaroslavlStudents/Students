using System;
using Mp3UtilLib.Actions;
using Mp3UtilLib.Arguments;
using Mp3UtilLib.FileSystem;
using Mp3UtilLib.Logger;

namespace Mp3UtilLib
{
    public class Processor
    {
        private readonly Args _arguments;
        private readonly IFileSystem _fileSystem;
        private readonly ConsoleLogger _logger;

        public Processor(Args arguments, IFileSystem fileSystem)
            : this(arguments, new ConsoleLogger(), fileSystem)
        {
            
        }

        public Processor(Args arguments, ConsoleLogger logger)
            : this(arguments, logger, new FileSystem.FileSystem())
        {
            
        }

        public Processor(Args arguments, ConsoleLogger logger, IFileSystem fileSystem)
        {
            _arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public void Execute()
        {
            IActionStrategy action = new ActionCreator().GetAction(_arguments.Action);

            foreach (AudioFile audioFile in 
                _fileSystem.GetAudioFilesFromCurrentDirectory(_arguments.Mask, _arguments.Recursive))
            {
                try
                {
                    action.Process(audioFile);
                    _logger.WriteSuccess($"{audioFile.FullName} - The transformation is complete");
                }
                catch (Exception ex)
                {
                    _logger.WriteError(ex.Message);
                }
            }

            _logger.WriteSuccess("Done!");
        }
    }
}