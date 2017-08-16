using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace RenamerLib
{
    public class Processor
    {
        private readonly Arguments arguments;
        private IFileManager fileManager;
        private ILogger logger;

        public Processor(Arguments arguments)
        {
            if (arguments == null)
                throw new ArgumentException("Arguments shouldn't be null!");

            this.arguments = arguments;
            this.logger = null;
            fileManager = new FileManager();
        }

        public Processor(Arguments arguments, IFileManager fileManager)
        {
            if (arguments == null)
                throw new ArgumentException("Arguments shouldn't be null!");

            this.arguments = arguments;
            this.logger = null;
            this.fileManager = fileManager;
        }

        public Processor(Arguments arguments, ILogger logger) : this(arguments)
        {
            if (logger == null)
                throw new ArgumentException("Arguments shouldn't be null!");

            this.logger = logger;
        }

        public Processor(Arguments arguments, ILogger logger, IFileManager fileManager) : this(arguments, logger)
        {
            this.fileManager = fileManager;
        }

        private IAction GetAction(AllowedActions action)
        {
            switch (action)
            {
                case AllowedActions.toFileName:
                    return new TagToFileNameAction();
                case AllowedActions.toTag:
                    return new FileNameToTagAction();
                default:
                    return null;
            }
        }

        public void Process()
        {
            IAction action = GetAction(arguments.Action);

            var files = fileManager.GetFiles(arguments.Mask, 
                arguments.IsRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                try
                {
                    logger?.WriteMessage(file.FilePath + " transformation processed", Status.Info);
                    action.Process(file);
                    logger?.WriteMessage(file.FilePath + " transformation complete", Status.Info);
                }
                catch (Exception e)
                {
                    logger?.WriteMessage(e.Message, Status.Error);
                    throw;
                }
            }

            logger?.WriteMessage("Process succesfully completed!", Status.Success);
        }
    }
}
