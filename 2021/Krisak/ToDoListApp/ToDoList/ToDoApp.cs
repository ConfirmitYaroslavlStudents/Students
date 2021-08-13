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
        private TaskStorage _taskStorage = new TaskStorage();
        private readonly ILogger _logger;
        private readonly TasksSaverAndLoader _tasksSaverAndLoad = new TasksSaverAndLoader(Data.SaveAndLoadFileName);

        public ToDoApp(ILogger logger)
        {
            _logger = logger;
            LoadNotes();
        }

        public void WorkWithConsole(IUserInput userInput)
        {
            var result = ResultHandler.Working;
            var handlerCommandFromConsole = new CommandsFromConsoleHandler(_taskStorage);

            while (result!=ResultHandler.Completed)
            {
                ShowInformationInConsole.ShowHelpInConsole();
                result = RunHandler(userInput, handlerCommandFromConsole);
            }

            ShowInformationInConsole.ShowGoodbyeInConsole();
        }

        public void WorkWithCmd(IUserInput userInput)
        {
            var handlerCommandsFromCmd = new CommandFromCmdHandler(_taskStorage);
            RunHandler(userInput, handlerCommandsFromCmd);
        }

        public ResultHandler RunHandler(IUserInput userInput,IHandlerCommand handlerCommand)
        {
            var result = ResultHandler.Working;
            try
            {
                result = handlerCommand.CommandHandler(userInput.GetCommand());
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

            return result;
        }

        private void SaveNotes()
        {
            _tasksSaverAndLoad.Save(_taskStorage.Get());
        }

        private void LoadNotes()
        {
            try
            {
                _taskStorage.Set(_tasksSaverAndLoad.Load());
            }

            catch (FileNotFoundException e)
            {
                _taskStorage.Set(new List<Task>());

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