using System;
using Logger;

namespace RenamerLib.Actions
{
    public class ActionWithLogger : IAction
    {
        private readonly IAction _action;
        private readonly ILogger _logger;

        public ActionWithLogger(IAction action, ILogger logger)
        {
            _action = action;
            _logger = logger;
        }

        public void Process(IMP3File mp3File)
        {
            try
            {
                _logger?.WriteMessage(mp3File.FilePath + " transformation processed", Status.Info);
                _action.Process(mp3File);
                _logger?.WriteMessage(mp3File.FilePath + " transformation complete", Status.Info);
            }
            catch (Exception e)
            {
                _logger?.WriteMessage(e.Message, Status.Error);
                throw;
            }
        }
    }
}