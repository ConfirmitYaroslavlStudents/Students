using System;
using System.IO;
using Logger;
using RenamerLib.Actions;
using RenamerLib.Arguments;

namespace RenamerLib
{
    public class Processor
    {
        private readonly RenamerArguments _arguments;
        private readonly IFileManager _fileManager;
        private readonly ILogger _logger;

        public Processor(RenamerArguments renamerArguments)
        {
            if (renamerArguments == null)
                throw new ArgumentException("RenamerArguments shouldn't be null!");

            _arguments = renamerArguments;
            _logger = null;
            _fileManager = new FileSystemManager();
        }

        public Processor(RenamerArguments renamerArguments, IFileManager fileManager)
        {
            if (renamerArguments == null)
                throw new ArgumentException("RenamerArguments shouldn't be null!");

            _arguments = renamerArguments;
            _logger = null;
            _fileManager = fileManager;
        }

        public Processor(RenamerArguments renamerArguments, ILogger logger) : this(renamerArguments)
        {
            if (logger == null)
                throw new ArgumentException("RenamerArguments shouldn't be null!");

            _logger = logger;
        }

        public Processor(RenamerArguments renamerArguments, ILogger logger, IFileManager fileManager) : this(renamerArguments, logger)
        {
            _fileManager = fileManager;
        }

        private IAction GetAction(AllowedActions action)
        {
            switch (action)
            {
                case AllowedActions.ToFileName:
                    return new TagToFileNameAction();
                case AllowedActions.ToTag:
                    return new FileNameToTagAction();
                default:
                    return null;
            }
        }

        public void Process()
        {
            IAction action = new ActionWithLogger(GetAction(_arguments.Action), _logger);

            var files = _fileManager.GetFiles(_arguments.Mask, 
                _arguments.IsRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                action.Process(file);
            }

            _logger?.WriteMessage("Process succesfully completed!", Status.Success);
        }
    }
}
