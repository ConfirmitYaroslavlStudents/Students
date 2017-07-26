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
        private static FileManager fileManager;
        private ILogger logger;

        public Processor(Arguments arguments, ILogger logger)
        {
            if (arguments == null || logger == null)
                throw new ArgumentException("Arguments shouldn't be null!");

            this.arguments = arguments;
            this.logger = logger;
            fileManager = new FileManager();
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
            var files = fileManager.GetFiles(null, arguments.Mask, 
                arguments.IsRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                try
                {
                    logger.WriteMessage(file + " transformation processed", Status.Info);
                    action.Process(new MP3File(file));
                    logger.WriteMessage(file + " transformation complete", Status.Info);
                }
                catch (Exception e)
                {
                    logger.WriteMessage(e.Message, Status.Error);
                    throw;
                }
            }

            logger.WriteMessage("Process succesfully completed!", Status.Success);
        }
    }
}
