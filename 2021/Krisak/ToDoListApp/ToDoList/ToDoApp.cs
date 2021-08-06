using System;
using System.Collections.Generic;
using System.IO;
using ToDoLibrary.CommandHandler;
using ToDoLibrary.HandlerСommand;
using ToDoLibrary.Loggers;

namespace ToDoLibrary
{
    public class ToDoApp
    {
        private List<Task> _tasks = new List<Task>();
        private readonly ILogger _logger;
        private readonly SaveAndLoadNotes _saveAndLoad = new SaveAndLoadNotes(Data.SaveAndLoadFileName);

        public ToDoApp(ILogger logger)
        {
            _logger = logger;
            LoadNotes();
        }

        public void WorkWithConsole(IUserInput userInput)
        {
            var handlerCommandFromConsole = new HandlerCommandsFromConsole(_tasks);

            while (!handlerCommandFromConsole.IsFinished)
            {
                ShowInformationToUser.ShowHelpInConsole();

                try
                {
                    handlerCommandFromConsole.Handling(userInput.GetCommand());

                    SaveNotes();
                }

                catch (WrongEnteredCommandException e)
                {
                    _logger.Log(e.Message);
                }

                catch (Exception e)
                {
                    var fileAndConsoleLogger = new Loggers.FileLoggerDecorator(_logger, Data.LogFileName);
                    fileAndConsoleLogger.Log(e.Message);
                }
            }

            ShowInformationToUser.ShowGoodbyeInConsole();
        }

        public void WorkWithCmd(IUserInput userInput)
        {
            var handlerCommandsFromCmd = new HandlerCommandFromCmd(_tasks);

            try
            {
                handlerCommandsFromCmd.Handling(userInput.GetCommand());

                SaveNotes();
            }

            catch (WrongEnteredCommandException e)
            {
                _logger.Log(e.Message);
            }

            catch (Exception e)
            {
                var fileAndConsoleLogger = new Loggers.FileLoggerDecorator(_logger, Data.LogFileName);
                fileAndConsoleLogger.Log(e.Message);
            }

            ShowInformationToUser.ShowGoodbyeInConsole();
        }

        private void SaveNotes()
        {
            _saveAndLoad.Save(_tasks);
        }

        private void LoadNotes()
        {
            try
            {
                _tasks = _saveAndLoad.Load();
            }

            catch (FileNotFoundException e)
            {
                _tasks = new List<Task>();

                _logger.Log(e.Message);
            }

            catch (Exception e)
            {
                var fileAndConsoleLogger = new Loggers.FileLoggerDecorator(_logger, Data.LogFileName);
                fileAndConsoleLogger.Log(e.Message);
            }
        }
    }
}