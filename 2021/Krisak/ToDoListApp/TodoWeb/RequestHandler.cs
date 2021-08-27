using System;
using System.Collections.Generic;
using ToDoLibrary;
using ToDoLibrary.Loggers;
using ToDoLibrary.SaveAndLoad;
using System.Web.Mvc;
using ToDoLibrary.CommandHandler;
using ToDoLibrary.Commands;

namespace TodoWeb
{
    public class RequestHandler
    {
        private readonly WebLogger _logger;
        private readonly ToDoApp _toDoApp;

        public RequestHandler(WebLogger logger, ISaveTasks saver, ILoadTasks loader)
        {
            _logger = logger;
            _toDoApp = new ToDoApp(_logger, saver, loader);
        }

        public List<Task> ShowTasks()
            => _toDoApp.ShowTasks();

        public (int, string) Handle(HttpVerbs typeRequest, ICommand command)
        {
            var commandHandler = new WebCommandHandler(command);
            _toDoApp.HandleCommand(commandHandler);

            if (_logger.Exception != null)
                return GetResultWhenException(_logger.Exception);

            return GetResultWhenSuccess(typeRequest);
        }

        private (int, string) GetResultWhenException(Exception e)
        {
            return e switch
            {
                WrongEnteredCommandException _ => (400, e.Message),
                _ => (500, e.Message)
            };
        }

        private (int, string) GetResultWhenSuccess(HttpVerbs typeRequest)
        {
            return typeRequest switch
            {
                HttpVerbs.Post => (201, "New task has been successfully added."),
                HttpVerbs.Delete => (200, "Task has been successfully deleted."),
                HttpVerbs.Patch => (200, "Task has been successfully changed.")
            };
        }
    }
}