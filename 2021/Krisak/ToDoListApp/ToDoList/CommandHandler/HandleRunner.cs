using System;
using ToDoLibrary.Loggers;

namespace ToDoLibrary.CommandHandler
{
    public class HandleRunner
    {
        private readonly ILogger _logger;

        public HandleRunner(ILogger logger)
            => _logger = logger;

        public void Run(ICommandHandler commandHandler)
        {
            try
            {
                commandHandler.Run();
            }
            catch (Exception e)
            {
                _logger.Log(e);
            }
        }
    }
}